using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Agenda.Application.Exceptions;
using Agenda.Application.Interfaces;
using Agenda.Application.Services.Base;
using Agenda.Application.ViewModels;
using Agenda.Application.ViewModels.User;
using Agenda.Domain.Core;
using Agenda.Domain.Interfaces;
using Agenda.Domain.Models;
using Agenda.Domain.Models.Types;
using Agenda.Infrastructure;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace Agenda.Application.Services
{
    public class UserManagementService : ServiceBase, IUserManagementService
    {

        private readonly IUserRepository _userRepository;

        public UserManagementService(IUserRepository userRepository,
            IMapper mapper,
            IUnitOfWork unitOfWork,
            IUser appUser,
            IInteractionRepository interactionRepository) : base(interactionRepository, mapper, appUser, unitOfWork)
        {
            _userRepository = userRepository;
        }

        public async Task<UserResponse> CreateUser(UserRequest newUser)
        {
            var validate = await newUser.ValidateAsync();
            if (!validate.IsValid)
                throw new BadRequestException(validate.Errors);

            var conflictList = await GetConflictList(newUser.Username, newUser.Email);
            if (conflictList.Any())
                throw new ConflictException(conflictList);

            newUser.Password = StringCipher.Encrypt(newUser.Password);

            var user = _mapper.Map<User>(newUser);

            await _userRepository.Add(user);
            await AddInteraction(InteractionType.CreateUser.Id, $"User \"{newUser.Username}\" was created");
            await _unitOfWork.SaveAsync();

            return _mapper.Map<UserResponse>(user);
        }

        public async Task<UserResponse> GetUserResponse(int id)
        {
            var dbUser = await GetUserModel(id);

            if (dbUser == null)
                throw new NotFoundException($"Nenhum usuário com o ID = {id}");

            return _mapper.Map<UserResponse>(dbUser);
        }

        public async Task<UserResponse> UpdateUser(int userId, UserRequest updatedUser)
        {
            var validate = await updatedUser.ValidateAsync();
            if (!validate.IsValid)
                throw new BadRequestException(validate.Errors);

            var dbUser = await GetUserModel(userId);
            if (dbUser == null)
                throw new NotFoundException($"Nenhum usuário com o ID = {userId}");

            var conflictList = await GetConflictList(updatedUser.Username, updatedUser.Email, userId);
            if (conflictList.Any())
                throw new ConflictException(conflictList);

            updatedUser.Password = StringCipher.Encrypt(updatedUser.Password);

            _mapper.Map(updatedUser, dbUser);

            await _userRepository.Update(dbUser);
            await AddInteraction(InteractionType.UpdateUser.Id, $"User \"{updatedUser.Username}\" was updated");
            await _unitOfWork.SaveAsync();

            return _mapper.Map<UserResponse>(dbUser);
        }

        public async Task DeleteUser(int userId)
        {
            var dbUser = await GetUserModel(userId);

            if (dbUser == null)
                throw new NotFoundException($"Nenhum usuário com o ID = {userId}");

            await _userRepository.Remove(dbUser);
            await AddInteraction(InteractionType.RemoveUser.Id, $"User \"{dbUser.Username}\" was removed");
            await _unitOfWork.SaveAsync();
        }

        public async Task<Pagination<UserResponse>> Search(UserSearchParams sQ)
        {
            var usersPage = await _userRepository.Paginate(sQ);

            return _mapper.Map<Pagination<UserResponse>>(usersPage);
        }

        public IEnumerable<EnumerationResponse> GetAllUserTypes()
        {
            var types = Enumeration.GetAll<UserRole>();

            return _mapper.Map<IEnumerable<EnumerationResponse>>(types);
        }

        private async Task<User> GetUserModel(int id)
        {
            return await _userRepository.Query()
                .Include(u => u.Role)
                .FirstOrDefaultAsync(u => u.Id == id);
        }

        private async Task<List<string>> GetConflictList(string username, string email, int existingUserId = 0)
        {
            var conflictList = new List<string>();
            var isUsernameAlreadyRegisteredTask = IsUsernameAlreadyRegistered(username, existingUserId);

            var isUsernameAlreadyRegistered = await isUsernameAlreadyRegisteredTask;
            if (isUsernameAlreadyRegistered)
                conflictList.Add($"{username} já está cadastrado, escolha outro username");

            var isEmailAlreadyRegistered = await IsEmailAlreadyRegistered(email, existingUserId);
            if (isEmailAlreadyRegistered)
                conflictList.Add($"{email} já está cadastrado, escolha outro email");

            return conflictList;
        }

        public async Task<bool> IsUsernameAlreadyRegistered(string username, int existingUserId = 0)
        {
            return await _userRepository.Query()
                .Where(u => u.Id != existingUserId)
                .AnyAsync(u => u.Username.Equals(username));
        }

        public async Task<bool> IsEmailAlreadyRegistered(string email, int existingUserId = 0)
        {
            return await _userRepository.Query()
                .Where(u => u.Id != existingUserId)
                .AnyAsync(u => u.Email.Equals(email));
        }

    }
}

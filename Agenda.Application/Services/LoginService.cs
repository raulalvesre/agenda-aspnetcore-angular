using System.Threading.Tasks;
using Agenda.Application.Exceptions;
using Agenda.Application.Interfaces;
using Agenda.Application.Services.Base;
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
    public class LoginService : ServiceBase, ILoginService
    {

        private readonly IUserRepository _userRepository;

        public LoginService(IUserRepository userRepository,
            IInteractionRepository interactionRepository,
            IUser appUser,
            IUnitOfWork unitOfWork,
            IMapper mapper) : base(interactionRepository, mapper, appUser, unitOfWork)
        {
            _userRepository = userRepository;
        }

        public async Task<UserResponse> Login(UserLoginRequest user)
        {
            var validate = await user.ValidateAsync();
            if (!validate.IsValid)
                throw new BadRequestException(validate.Errors);

            var dbUser = await _userRepository.Query()
                .Include(u => u.Role)
                .FirstOrDefaultAsync(u => u.Username.Equals(user.Username));

            if (dbUser == null || !UserLoginRequestPasswordAndUserModelPasswordAreTheSame(user, dbUser))
                throw new UnauthorizedException("Usuário ou senha inválidos");

            await AddInteraction(InteractionType.UserLogin.Id, $"{dbUser.Username} logged in", dbUser.Id);
            await _unitOfWork.SaveAsync();

            return _mapper.Map<UserResponse>(dbUser);
        }

        private bool UserLoginRequestPasswordAndUserModelPasswordAreTheSame(UserLoginRequest uLR, User uM)
        {
            return StringCipher.Decrypt(uM.Password).Equals(uLR.Password);
        }

    }
}

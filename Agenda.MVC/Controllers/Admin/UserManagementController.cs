using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Agenda.MVC.ApiHttpServices;
using Agenda.MVC.ViewModels;
using Agenda.MVC.ViewModels.ApiResponse;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Agenda.MVC.Controllers
{
    [Authorize(Roles = "ADMIN")]
    [Route("gerenciamento-usuarios")]
    public class UserManagementController : Controller
    {

        private readonly UserManagementService _userService;
        private readonly IMapper _mapper;

        public UserManagementController(UserManagementService userService,
            IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index(int page = 1)
        {
            var userPagination = await _userService.GetUserPage(page);

            ViewBag.totalPages = Math.Ceiling((double)userPagination.Total / 6);
            ViewBag.currentPage = page;

            var userIndexVm = new UserIndexViewModel() {
                Users = userPagination.Data,
                ModalForm = null
            };

            return View(userIndexVm);
        }

        [HttpGet]
        [Route("form")]
        public async Task<ActionResult> GetUserForm(int? id)
        {
            UserFormViewModel userFormVm = null;

            if (id.HasValue)
            {
                var user = await _userService.GetUser((int)id);
                userFormVm = _mapper.Map<UserFormViewModel>(user);
            }

            TempData["Roles"] = await GetUserRolesSelectList();

            return PartialView("_UserForm", userFormVm);
        }

        [HttpPost]
        public async Task<ActionResult> Index(int page, UserFormViewModel model)
        {
            ApiResponseViewModel<UserViewModel> apiResponse;

            if (string.IsNullOrEmpty(model.Id))
            {
                apiResponse = await _userService.AddUser(model);
                TempData.Add("toast", "Usuário adicionado com sucesso");
            }
            else
            {
                apiResponse = await _userService.UpdateUser(model);
                TempData.Add("toast", "Usuário atualizado com sucesso");
            }

            if (!apiResponse.HasError)
            {
                return RedirectToAction("Index");
            }

            var userPagination = await _userService.GetUserPage(page);
            apiResponse.AddErrorsToModelState(ModelState);
            TempData.Remove("toast");

            var userIndexVm = new UserIndexViewModel()
            {
                Users = userPagination.Data,
                ModalForm = model
            };

            ViewBag.totalPages = Math.Ceiling((double)userPagination.Total / 6);
            ViewBag.currentPage = page;
            TempData["Roles"] = await GetUserRolesSelectList();

            return View(userIndexVm);
        }

        [HttpPost]
        [Route("remover")]
        public async Task<ActionResult> RemoveUser(int id)
        {
            await _userService.RemoveUser(id);
            TempData.Add("toast", "Usuário removido com sucesso");
            return Content(Url.Action("Index"));
        }

        private async Task<SelectList> GetUserRolesSelectList()
        {
            var roles = await _userService.GetUserRoles();
            return new SelectList(roles, "Id", "Name", 2);
        }

    }
}

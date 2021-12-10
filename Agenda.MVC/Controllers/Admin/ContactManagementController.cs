using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Agenda.MVC.ApiHttpServices;
using Agenda.MVC.ViewModels.ApiResponse;
using Agenda.MVC.ViewModels.Contact;
using Agenda.MVC.ViewModels.ContactTelephone;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Agenda.MVC.Controllers
{

    [Authorize(Roles = "ADMIN")]
    [Route("gerenciamento-contatos")]
    public class ContactManagementController : Controller
    {

        private readonly ContactManagementService _contactManagementService;
        private readonly UserManagementService _userManagemmentService;
        private readonly IMapper _mapper;

        public ContactManagementController(ContactManagementService contactManagementService, IMapper mapper, UserManagementService userManagemmentService)
        {
            _contactManagementService = contactManagementService;
            _mapper = mapper;
            _userManagemmentService = userManagemmentService;
        }

        public async Task<ActionResult> Index(int page = 1)
        {
            var contactPagination = await _contactManagementService.GetContactPage(page);

            ViewBag.totalPages = Math.Ceiling((double)contactPagination.Total / 6);
            ViewBag.currentPage = page;

            return View(contactPagination.Data);
        }

        [HttpGet]
        [Route("detalhes/{id:int}")]
        public async Task<ActionResult> Details(int id)
        {
            var contactVm = await _contactManagementService.GetContact(id);

            return View("_ContactDetails", contactVm);
        }

        [HttpGet]
        [Route("form/{id:int}")]
        public async Task<ActionResult> Form(int id)
        {
            ContactFormViewModel contactFormVm = new ContactFormViewModel();
            ViewBag.telephoneTypes = await GetTelephoneTypeSelectList();
            ViewBag.users = await GetUsersSelectList();

            if (id != 0)
            {
                var contactVm = await _contactManagementService.GetContact(id);
                contactFormVm = _mapper.Map<ContactFormViewModel>(contactVm);
            }

            return View("_ContactForm", contactFormVm);
        }

        [HttpPost]
        [Route("form/{id:int}")]
        public async Task<ActionResult> Form(ContactFormViewModel model, string option = "save")
        {
            ViewBag.telephoneTypes = await GetTelephoneTypeSelectList();
            ViewBag.users = await GetUsersSelectList();

            if (option.Equals("save"))
            {
                ApiResponseViewModel<ContactViewModel> apiResponse;

                if (model.Id == 0)
                {
                    apiResponse = await _contactManagementService.AddContact(model);
                    TempData.Add("toast", "Contato adicionado com sucesso");
                }
                else
                {
                    apiResponse = await _contactManagementService.UpdateContact(model);
                    TempData.Add("toast", "Contato atualizado com sucesso");
                }

                if (apiResponse.HasError)
                {
                    apiResponse.AddErrorsToModelState(ModelState);
                    TempData.Remove("toast");
                    return View("_ContactForm", model);
                }

                return RedirectToAction("Index");
            }

            if (option.Equals("addTelephone"))
            {
                ModelState.Clear();
                model.Telephones.Add(new ContactTelephoneFormViewModel());
            }

            if (option.Contains("removeTelephone"))
            {
                int indexRemovedTelephone = int.Parse(option.Split("|")[1]);
                ModelState.Clear();
                model.Telephones.RemoveAt(indexRemovedTelephone);
            }

            return View("_ContactForm", model);
        }

        [HttpPost]
        [Route("remover")]
        public async Task<ActionResult> RemoveContact(int id)
        {
            await _contactManagementService.RemoveContact(id);
            TempData.Add("toast", "Contato removido com sucesso");
            return Content(Url.Action("Index"));
        }

        private async Task<SelectList> GetUsersSelectList()
        {
            var users = await _userManagemmentService.GetAllUsers();
            var items = users.Select(u => new { u.Id, Text = u.Username }).ToList();
            return new SelectList(items, "Id", "Text");
        }

        private async Task<SelectList> GetTelephoneTypeSelectList()
        {
            var telephoneTypes = await _contactManagementService.GetTelephoneTypes();
            var items = telephoneTypes.Select(tt => new { tt.Id, Text = tt.Name }).ToList();
            return new SelectList(items, "Id", "Text");
        }

    }
}

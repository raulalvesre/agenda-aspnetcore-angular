using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Agenda.MVC.ApiHttpServices;
using Agenda.MVC.ViewModels;
using Agenda.MVC.ViewModels.ApiResponse;
using Agenda.MVC.ViewModels.Contact;
using Agenda.MVC.ViewModels.ContactTelephone;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Agenda.MVC.Controllers
{
    [Authorize(Roles = "STANDARD USER")]
    [Route("agenda")]
    public class PhonebookController : Controller
    {
        private readonly PhonebookService _phonebookService;
        private readonly IMapper _mapper;

        public PhonebookController(PhonebookService phonebookService, IMapper mapper)
        {
            _phonebookService = phonebookService;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index([Bind(Prefix = nameof(PhonebookViewModel.SearchParameters))] PhonebookSearchViewModel searchParameters)
        {
            IEnumerable<ContactViewModel> contacts = new List<ContactViewModel>();

            var contactPagination = await _phonebookService.GetContactPage(searchParameters);

            if (contactPagination.Data != null)
            {
                contacts = contactPagination.Data;
                searchParameters.CountPages(7, contactPagination.Total);
                if (searchParameters.Page == 0)
                    searchParameters.Page = 1;
            }

            var model = new PhonebookViewModel()
            {
                SearchParameters = searchParameters,    
                Contacts = contacts
            };

            return View(model);
        }

        [HttpGet]
        [Route("detalhes/{id:int}")]
        public async Task<ActionResult> Details(int id)
        {
            var contactVm = await _phonebookService.GetContact(id);

            return View("_ContactDetails", contactVm);
        }

        [HttpGet]
        [Route("form/{id:int}")]
        public async Task<ActionResult> Form(int id)
        {
            ContactFormViewModel contactFormVm = new ContactFormViewModel();
            ViewBag.telephoneTypes = await GetTelephoneTypeSelectList();

            if (id != 0)
            {
                var contactVm = await _phonebookService.GetContact(id);
                contactFormVm = _mapper.Map<ContactFormViewModel>(contactVm);
            }

            return View("_ContactForm", contactFormVm);
        }

        [HttpPost]
        [Route("form/{id:int}")]
        public async Task<ActionResult> Form(ContactFormViewModel model, string option = "save")
        {
            ViewBag.telephoneTypes = await GetTelephoneTypeSelectList();

            if (option.Equals("save"))
            {
                ApiResponseViewModel<ContactViewModel> apiResponse;

                if (model.Id == 0)
                {
                    apiResponse = await _phonebookService.AddContact(model);
                    TempData.Add("toast", "Contato adicionado com sucesso");
                }
                else
                {
                    apiResponse = await _phonebookService.UpdateContact(model);
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
            await _phonebookService.RemoveContact(id);
            TempData.Add("toast", "Contato removido com sucesso");
            return Content(Url.Action("Index"));
        }

        private async Task<SelectList> GetTelephoneTypeSelectList()
        {
            var telephoneTypes = await _phonebookService.GetTelephoneTypes();
            var items = telephoneTypes.Select(tt => new { tt.Id, Text = tt.Name }).ToList();
            return new SelectList(items, "Id", "Text");
        }

    }
}

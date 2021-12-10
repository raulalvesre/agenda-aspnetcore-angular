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
    [Route("interacoes")]
    public class InteractionsController : Controller
    {

        private readonly InteractionsService _interactionService;

        public InteractionsController(InteractionsService interactionService)
        {
            _interactionService = interactionService;
        }

        public async Task<IActionResult> Index(int page = 1)
        {
            var interactionPagination = await _interactionService.GetInteractionPage(page);

            ViewBag.totalPages = Math.Ceiling((double)interactionPagination.Total / 6);
            ViewBag.currentPage = page;

            return View(interactionPagination.Data);
        }

    }
}

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Jobs.Common;
using Jobs.Common.Factories;
using Jobs.Data.WorkingPerson;
using Jobs.Data.WorkingPerson.Employee;
using Jobs.Data.WorkingPerson.Employer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MVC.Models;
using MVC.Models.ViewModels;
using MVC.Services;

namespace MVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly OfferHandler _offerHandler;
        private readonly OfferHandleredFactory _offerHandleredFactory;
        private readonly UserHandlerService _userHandlerService;


        public HomeController(ILogger<HomeController> logger, OfferHandler offerHandler, OfferHandleredFactory offerHandleredFactory, UserHandlerService userHandlerService)
        {
            _logger = logger;
            this._offerHandler = offerHandler;
            this._offerHandleredFactory = offerHandleredFactory;
            this._userHandlerService = userHandlerService;
        }

        [HttpPost]
        public IActionResult UseUser(HomeViewModel homeViewModel)
        {
            if(homeViewModel.SelectedUserName == null)
            {
                this._userHandlerService.ActiveUser = null;
            }
            else
            {
                bool valid = this.TryValidateModel(homeViewModel.SelectedUserName, nameof(homeViewModel.SelectedUserName));
                if (valid)
                { 
                    this._userHandlerService.ActiveUser = this._userHandlerService.WorkingPeople.First(ppl => ppl.Contact.Name == homeViewModel.SelectedUserName);
                }
            }
            return View(nameof(Index), homeViewModel);
        }

        [HttpPost]
        public IActionResult CreateEmployee(HomeViewModel homeViewModel)
        {
            homeViewModel.CreatedEmployer = null;
            bool valid = this.TryValidateModel(homeViewModel.CreatedEmployee, nameof(homeViewModel.CreatedEmployee));
            if (valid && !this._userHandlerService.WorkingPeople.Any( emp => emp.Contact.Name == homeViewModel.CreatedEmployee.Contact.Name))
            {
                this._offerHandleredFactory.CreateEmployee(homeViewModel.CreatedEmployee);
            }
            else if(this._userHandlerService.WorkingPeople.Any(emp => emp.Contact.Name == homeViewModel.CreatedEmployee.Contact.Name))
            {
                ModelState.AddModelError(string.Empty, String.Format("The given name: {0} already taken", homeViewModel.CreatedEmployee.Contact.Name));
            }
            return View(nameof(Index), homeViewModel);
        }

        [HttpPost]
        public IActionResult CreateEmployer(HomeViewModel homeViewModel)
        {
            homeViewModel.CreatedEmployee = null;
            bool valid = this.TryValidateModel(homeViewModel.CreatedEmployer, nameof(homeViewModel.CreatedEmployer));
            if (valid && !this._userHandlerService.WorkingPeople.Any(emp => emp.Contact.Name == homeViewModel.CreatedEmployer.Contact.Name))
            {
                this._offerHandleredFactory.CreateEmployer(homeViewModel.CreatedEmployer);
            }
            else if (this._userHandlerService.WorkingPeople.Any(emp => emp.Contact.Name == homeViewModel.CreatedEmployer.Contact.Name))
            {
                ModelState.AddModelError(string.Empty, String.Format("The given name: {0} already taken", homeViewModel.CreatedEmployer.Contact.Name));
            }
            return View(nameof(Index), homeViewModel);
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

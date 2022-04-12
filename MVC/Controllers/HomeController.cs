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

namespace MVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly OfferHandler _offerHandler;
        private readonly OfferHandleredFactory _offerHandleredFactory;


        public HomeController(ILogger<HomeController> logger, OfferHandler offerHandler, OfferHandleredFactory offerHandleredFactory)
        {
            _logger = logger;
            this._offerHandler = offerHandler;
            this._offerHandleredFactory = offerHandleredFactory;
        }

        [HttpPost]
        public IActionResult CreateEmployee(HomeViewModel homeViewModel)
        {
            homeViewModel.CreatedEmployer = null;
            bool valid = this.TryValidateModel(homeViewModel.CreatedEmployee, nameof(homeViewModel.CreatedEmployee));
            if (valid)
            {
                this._offerHandleredFactory.CreateEmployee(homeViewModel.CreatedEmployee);
            }
            return View(nameof(Index), homeViewModel);
        }

        [HttpPost]
        public IActionResult CreateEmployer(HomeViewModel homeViewModel)
        {
            homeViewModel.CreatedEmployee = null;
            bool valid = this.TryValidateModel(homeViewModel.CreatedEmployer, nameof(homeViewModel.CreatedEmployer));
            if (valid)
            {
                this._offerHandleredFactory.CreateEmployer(homeViewModel.CreatedEmployer);
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

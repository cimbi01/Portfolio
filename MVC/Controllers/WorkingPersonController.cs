using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Jobs.Common;
using Jobs.Common.Factories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MVC.Models.ViewModels;
using MVC.Services;

namespace MVC.Controllers
{
    public class WorkingPersonController : Controller
    {
        private readonly ILogger<WorkingPersonController> _logger;
        private readonly OfferHandler _offerHandler;
        private readonly OfferHandleredFactory _offerHandleredFactory;
        private readonly UserHandlerService _userHandlerService;

        public WorkingPersonController(ILogger<WorkingPersonController> logger, OfferHandler offerHandler, OfferHandleredFactory offerHandleredFactory, UserHandlerService userHandlerService)
        {
            _logger = logger;
            this._offerHandler = offerHandler;
            this._offerHandleredFactory = offerHandleredFactory;
            this._userHandlerService = userHandlerService;
        }

        public IActionResult MyContact()
        {
            WorkingPersonViewModel workingPersonViewModel = new WorkingPersonViewModel();
            workingPersonViewModel.WorkingPerson = this._userHandlerService.ActiveUser;
            return View(workingPersonViewModel);
        }

        public IActionResult UpdateMyContact(WorkingPersonViewModel workingPersonViewModel)
        {
            bool valid = this.TryValidateModel(workingPersonViewModel.WorkingPerson.Contact, nameof(workingPersonViewModel.WorkingPerson.Contact));
            if (valid)
            {
                this._userHandlerService.ActiveUser.Contact = workingPersonViewModel.WorkingPerson.Contact;
            }
            return View("MyContact", workingPersonViewModel);
        }


        /*
         * TODO: Read  -> Advertisements (Employers) (Filter, Sort)
         * TODO: Read -> Own JobOffers (Filter, Sort) -> (Own) ReceivedJobOffers Accept/Decline
         */
    }
}

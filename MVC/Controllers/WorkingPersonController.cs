using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Jobs.Common;
using Jobs.Common.Factories;
using Jobs.Data;
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

        private WorkingPersonViewModel InitializeWorkingPersonViewModel()
        {
            WorkingPersonViewModel model = new WorkingPersonViewModel();
            model = this.InitializeWorkingPersonViewModel(model);
            return model;
        }

        private WorkingPersonViewModel InitializeWorkingPersonViewModel(WorkingPersonViewModel model, bool ContactUpdate = true)
        {
            model.ManagedAdvertisements = this._offerHandler.Advertisements;
            model.ManagedJobOffers = this._offerHandler.GetJobOffers(this._userHandlerService.ActiveUser);
            if(ContactUpdate)
            { 
                model.Contact = this._userHandlerService.ActiveUser.Contact;
            }
            return model;
        }

        public IActionResult MyContact()
        {
            return View(this.InitializeWorkingPersonViewModel());
        }

        public IActionResult Advertisements()
        {
            return View(this.InitializeWorkingPersonViewModel());
        }

        public IActionResult MyJobOffers()
        {
            return View(this.InitializeWorkingPersonViewModel());
        }

        public IActionResult UpdateMyContact(WorkingPersonViewModel workingPersonViewModel)
        {
            workingPersonViewModel = this.InitializeWorkingPersonViewModel(workingPersonViewModel, false);
            bool valid = this.TryValidateModel(workingPersonViewModel.Contact, nameof(workingPersonViewModel.Contact));
            if (valid)
            {
                this._userHandlerService.ActiveUser.Contact = workingPersonViewModel.Contact;
            }
            return View("MyContact", workingPersonViewModel);
        }

        [HttpPost]
        public IActionResult OrderAdvertisements(WorkingPersonViewModel workingPersonViewModel)
        {
            workingPersonViewModel = this.InitializeWorkingPersonViewModel(workingPersonViewModel);
            bool valid = this.TryValidateModel(workingPersonViewModel.OrderString, nameof(workingPersonViewModel.OrderString));
            if (valid)
            {
                if(workingPersonViewModel.Ascending)
                {
                    switch(workingPersonViewModel.OrderString)
                    {
                        //TODO: enum
                        case "Name":
                            workingPersonViewModel.ManagedAdvertisements = this._offerHandler.Advertisements.OrderBy(offer => offer.JobData.Name).ToList();
                            break;
                        case "Details":
                            workingPersonViewModel.ManagedAdvertisements = this._offerHandler.Advertisements.OrderBy(offer => offer.JobData.Details).ToList();
                            break;
                        case "Offerer":
                            workingPersonViewModel.ManagedAdvertisements = this._offerHandler.Advertisements.OrderBy(offer => offer.Offerer.Contact.Name).ToList();
                            break;
                    }
                    workingPersonViewModel.Ascending = false;
                }
                else
                {
                    switch (workingPersonViewModel.OrderString)
                    {
                        //TODO: enum
                        case "Name":
                            workingPersonViewModel.ManagedAdvertisements = this._offerHandler.Advertisements.OrderByDescending(offer => offer.JobData.Name).ToList();
                            break;
                        case "Details":
                            workingPersonViewModel.ManagedAdvertisements = this._offerHandler.Advertisements.OrderByDescending(offer => offer.JobData.Details).ToList();
                            break;
                        case "Offerer":
                            workingPersonViewModel.ManagedAdvertisements = this._offerHandler.Advertisements.OrderByDescending(offer => offer.Offerer.Contact.Name).ToList();
                            break;
                    }
                    workingPersonViewModel.Ascending = true;
                }
            }
            return View("Advertisements", workingPersonViewModel);
        }

        /*
         * TODO: Read -> Own JobOffers (Filter, Sort) -> (Own) ReceivedJobOffers Accept/Decline
         */
    }
}

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
        protected readonly ILogger<WorkingPersonController> _logger;
        protected readonly OfferHandler _offerHandler;
        protected readonly OfferHandleredFactory _offerHandleredFactory;
        protected readonly UserHandlerService _userHandlerService;

        public WorkingPersonController(ILogger<WorkingPersonController> logger, OfferHandler offerHandler, OfferHandleredFactory offerHandleredFactory, UserHandlerService userHandlerService)
        {
            _logger = logger;
            this._offerHandler = offerHandler;
            this._offerHandleredFactory = offerHandleredFactory;
            this._userHandlerService = userHandlerService;
        }

        protected WorkingPersonViewModel InitializeWorkingPersonViewModel()
        {
            WorkingPersonViewModel model = new WorkingPersonViewModel();
            model = this.InitializeWorkingPersonViewModel(model);
            return model;
        }

        protected WorkingPersonViewModel InitializeWorkingPersonViewModel(WorkingPersonViewModel model, bool ContactUpdate = true)
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

        public IActionResult AcceptJobOffer(WorkingPersonViewModel workingPersonViewModel)
        {
            workingPersonViewModel = this.InitializeWorkingPersonViewModel(workingPersonViewModel, false);
            bool valid = this.TryValidateModel(workingPersonViewModel.SelectedJobDataName, nameof(workingPersonViewModel.SelectedJobDataName));
            if (valid)
            {
                JobOffer jobOffer = this._offerHandler.JobOffers.First(offer => offer.JobData.Name == workingPersonViewModel.SelectedJobDataName);
                this._offerHandler.AcceptJobOffer(jobOffer);
            }
            return View("MyJobOffers", workingPersonViewModel);
        }

        public IActionResult DeclineJobOffer(WorkingPersonViewModel workingPersonViewModel)
        {
            workingPersonViewModel = this.InitializeWorkingPersonViewModel(workingPersonViewModel, false);
            bool valid = this.TryValidateModel(workingPersonViewModel.SelectedJobDataName, nameof(workingPersonViewModel.SelectedJobDataName));
            if (valid)
            {
                JobOffer jobOffer = this._offerHandler.JobOffers.First(offer => offer.JobData.Name == workingPersonViewModel.SelectedJobDataName);
                this._offerHandler.DeclineJobOffer(jobOffer);
            }
            return View("MyJobOffers", workingPersonViewModel);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Jobs.Common;
using Jobs.Common.Factories;
using Jobs.Data;
using Jobs.Data.WorkingPerson.Employee;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MVC.Models.ViewModels;
using MVC.Services;

namespace MVC.Controllers
{
    public class EmployeeController : WorkingPersonController
    {

        public EmployeeController(ILogger<EmployeeController> logger, OfferHandler offerHandler, OfferHandleredFactory offerHandleredFactory, UserHandlerService userHandlerService) : base(logger, offerHandler, offerHandleredFactory, userHandlerService)
        {
        }

        private EmployeeViewModel InitializeEmployeeViewModel()
        {
            EmployeeViewModel employeeViewModel = new EmployeeViewModel(base.InitializeWorkingPersonViewModel());
            return this.InitializeEmployeeViewModel(employeeViewModel);
        }
        protected EmployeeViewModel InitializeEmployeeViewModel(EmployeeViewModel employeeViewModel)
        {
            employeeViewModel.ProfessionData = this._userHandlerService.ActiveEmployee.ProfessionData;
            return employeeViewModel;
        }

        public IActionResult MySkills()
        {
            return View(this.InitializeEmployeeViewModel());
        }

        public IActionResult EditMySkill(EmployeeViewModel employeeViewModel)
        {
            employeeViewModel.Skill = this._userHandlerService.ActiveEmployee.ProfessionData.Skills.First(skill => skill.Name == employeeViewModel.SelectedSkillName);
            employeeViewModel.EditSkill = true;
            return View("EditMySkill", employeeViewModel);
        }

        public IActionResult CreateMySkillEmpty()
        {
            EmployeeViewModel employeeViewModel = this.InitializeEmployeeViewModel();
            employeeViewModel.EditSkill = false;
            return View("EditMySkill", employeeViewModel);
        }

        public IActionResult CreateMySkill(EmployeeViewModel employeeViewModel)
        {
            bool valid = this.TryValidateModel(employeeViewModel.Skill, nameof(employeeViewModel.Skill));
            if (valid)
            {
                Skill skill = this._offerHandleredFactory.CreateSkill(employeeViewModel.Skill, this._userHandlerService.ActiveEmployee);
            }
            employeeViewModel = this.InitializeEmployeeViewModel(employeeViewModel);
            return View("MySkills", employeeViewModel);
        }

        public IActionResult UpdateMySkill(EmployeeViewModel employeeViewModel)
        {
            bool valid = this.TryValidateModel(employeeViewModel.Skill, nameof(employeeViewModel.Skill));
            if (valid)
            {
                int skillIndex = this._userHandlerService.ActiveEmployee.ProfessionData.Skills.FindIndex(skill => skill.Name == employeeViewModel.Skill.Name);
                this._userHandlerService.ActiveEmployee.ProfessionData.Skills[skillIndex] = employeeViewModel.Skill;
            }
            employeeViewModel = this.InitializeEmployeeViewModel(employeeViewModel);
            return View("MySkills", employeeViewModel);
        }

        //TODO: Refactor Code Clones Skill - Reference (Edit, Create, CreateEmpty)
        public IActionResult MyReferences()
        {
            return View(this.InitializeEmployeeViewModel());
        }

        public IActionResult EditMyReference(EmployeeViewModel employeeViewModel)
        {
            employeeViewModel.Reference = this._userHandlerService.ActiveEmployee.ProfessionData.References.First(reference => reference.Name == employeeViewModel.SelectedReferenceName);
            employeeViewModel.EditReference = true;
            return View("EditMyReference", employeeViewModel);
        }

        public IActionResult CreateMyReferenceEmpty()
        {
            EmployeeViewModel employeeViewModel = this.InitializeEmployeeViewModel();
            employeeViewModel.EditReference = false;
            return View("EditMyReference", employeeViewModel);
        }

        public IActionResult CreateMyReference(EmployeeViewModel employeeViewModel)
        {
            bool valid = this.TryValidateModel(employeeViewModel.Reference, nameof(employeeViewModel.Reference));
            if (valid)
            {
                Reference reference = this._offerHandleredFactory.CreateReference(employeeViewModel.Reference, this._userHandlerService.ActiveEmployee);
            }
            employeeViewModel = this.InitializeEmployeeViewModel(employeeViewModel);
            return View("MyReferences", employeeViewModel);
        }

        public IActionResult UpdateMyReference(EmployeeViewModel employeeViewModel)
        {
            bool valid = this.TryValidateModel(employeeViewModel.Reference, nameof(employeeViewModel.Reference));
            if (valid)
            {
                int referenceIndex = this._userHandlerService.ActiveEmployee.ProfessionData.References.FindIndex(reference => reference.Name == employeeViewModel.Reference.Name);
                this._userHandlerService.ActiveEmployee.ProfessionData.References[referenceIndex] = employeeViewModel.Reference;
            }
            employeeViewModel = this.InitializeEmployeeViewModel(employeeViewModel);
            return View("MyReferences", employeeViewModel);
        }

        public override IActionResult Advertisements()
        {
            EmployeeViewModel employeeViewModel = this.InitializeEmployeeViewModel();
            base.Advertisements();
            return View(employeeViewModel);
        }

        /*
         * TODO: Read -> (Own) ReceivedJobOffers Accept/Decline +-> (Own) ReceivedJobOffers CalculateSuitability()
         * TODO: Read  -> Advertisements (Employers) -> +-> CalculateSuitability, Apply -> ViewComponennt
         */
    }
}

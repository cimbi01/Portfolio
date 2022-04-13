using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Jobs.Common;
using Jobs.Common.Factories;
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
            return View(employeeViewModel);
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

        /*
         * TODO: Skills List -> Read-Update / Create
         * TODO: References List -> Read-Update / Create
         * TODO: Read -> (Own) ReceivedJobOffers Accept/Decline +-> (Own) ReceivedJobOffers CalculateSuitability()
         * TODO: Read  -> Advertisements (Employers) -> +-> CalculateSuitability, Apply        
         */
    }
}

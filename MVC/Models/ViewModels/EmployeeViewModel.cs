using Jobs.Data;
using Jobs.Data.WorkingPerson.Employee;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVC.Models.ViewModels
{
    public class EmployeeViewModel : WorkingPersonViewModel
    {
        public EmployeeViewModel(WorkingPersonViewModel workingPersonViewModel) : base(workingPersonViewModel)
        {

        }
        public EmployeeViewModel()
        {

        }

        //TODO: Enum -> EditSkillType
        public bool? EditReference{ get; set; }
        //TODO: Enum -> EditSkillType
        public bool? EditSkill { get; set; }
        public string? SelectedSkillName { get; set; }
        public Skill? Skill { get; set; }
        public string? SelectedReferenceName { get; set; }
        public Reference? Reference { get; set; }
        public ProfessionData? ProfessionData { get; set; }
    }
}

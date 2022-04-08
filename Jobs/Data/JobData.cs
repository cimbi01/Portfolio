using Jobs.Data.WorkingPerson.Employee;
using Jobs.Data.WorkingPerson.Employer;
using System;
using System.Collections.Generic;

namespace Jobs.Data
{
    public class JobData
    {
        public string Name { get; }
        public string Details { get; }

        // TODO: eventhandler -> set to false -> EndTime = DateTime.Now, null values needs to be initialized

        /// <summary>
        /// True if the job is active. False if the job is done.
        /// </summary>
        public bool OnGoing { get; }

        public DateTime StartTime { get; }

        public Employer Employer { get; }

        public List<Employee> Employees { get; } = new List<Employee>();

        /// <summary>
        /// List of <see cref="Skill"/>s that are needed for this job.
        /// </summary>
        public List<Skill> NeededSkills { get; set; }

        /// <summary>
        ///  Returns percentage of Employee.SkillDatas <-> JobData.NeededSkills
        /// </summary>
        /// <param name="employee"></param>
        /// <returns>Percentage</returns>
        public int CalculateEmployeeSuitability(Employee employee)
        {
            //TODO: implementation
            throw new NotImplementedException();
        }
    }
}
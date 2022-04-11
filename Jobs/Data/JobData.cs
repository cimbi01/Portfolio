using Jobs.Data.WorkingPerson.Employee;
using Jobs.Data.WorkingPerson.Employer;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Jobs.Data
{
    public class JobData
    {
        public JobData(string name, Employer employer)
        {
            this.Name = name;
            this.Employer = employer;
        }

        public string Name { get; }

        /// <summary>
        /// Details of a job. Can be null of Employer giving no details about the job.
        /// </summary>
        public string? Details { get; }

        //TODO: End()
        // TODO: eventhandler -> set to false -> EndTime = DateTime.Now, null values needs to be initialized
        /// <summary>
        /// Start time of the job. Can be null, if not started yet.
        /// </summary>
        public DateTime? StartTime { get; protected set; }

        public Employer Employer { get; }

        /// <summary>
        /// List of <see cref="Employee"/>s working on this job.
        /// </summary>
        public List<Employee> Employees { get; } = new List<Employee>();

        /// <summary>
        /// List of <see cref="Skill"/>s that are needed for this job.
        /// </summary>
        public List<Skill> NeededSkills { get; } = new List<Skill>();

        /// <summary>
        ///  Returned value will be 1 if all <see cref="Skill.RangeOfKnowledge"/> of <see cref="NeededSkills"/> are matching <paramref name="employee"/>s Skills.<br/>
        ///  Returned value will be higher than 1 if all <see cref="Skill.RangeOfKnowledge"/> of <see cref="NeededSkills"/> are matching <paramref name="employee"/>s Skills and some of the <paramref name="employee"/>s Skills are higher than needed.<br/>
        ///  Returned value will be lower than 1 if some <see cref="Skill.RangeOfKnowledge"/> of <see cref="NeededSkills"/> are higher than <paramref name="employee"/>s Skills.<br/>
        ///  </summary>
        /// <param name="employee"></param>
        /// <returns></returns>
        public double CalculateSuitability(Employee employee)
        {
            double suitability = 0;
            List<double> ranges = new List<double>();
            List<Skill> employeeSkills = employee.ProfessionData.Skills;
            bool anyLessThan1OrNotInEmployeesSkills = false;
            for (int i = 0; i < NeededSkills.Count; i++)
            {
                Skill neededSkill = this.NeededSkills[i];
                double range = 0;
                if (employeeSkills.Any(skill => skill.Name == neededSkill.Name))
                {
                    Skill employeeSkill = employeeSkills.First(skill => skill.Name == neededSkill.Name);
                    range = employeeSkill.RangeOfKnowledge / neededSkill.RangeOfKnowledge;
                    if(range < 1 && !anyLessThan1OrNotInEmployeesSkills)
                    {
                        anyLessThan1OrNotInEmployeesSkills = true;
                    }
                }
                else if(!anyLessThan1OrNotInEmployeesSkills)
                {
                    anyLessThan1OrNotInEmployeesSkills = true;
                }
                ranges.Add(range);
            }
            double rangesSum = 0;
            for (int i = 0; i < ranges.Count; i++)
            {
                if(anyLessThan1OrNotInEmployeesSkills && ranges[i] > 1)
                {
                    ranges[i] = 1;
                }
                rangesSum += ranges[i];
            }
            suitability = rangesSum / ranges.Count;
            return suitability;
        }

        public void Start()
        {
            this.StartTime = DateTime.Now;
        }
    }
}
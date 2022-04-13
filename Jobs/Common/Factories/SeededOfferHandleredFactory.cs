using Jobs.Data;
using Jobs.Data.WorkingPerson.Employee;
using Jobs.Data.WorkingPerson.Employer;
using System;
using System.Collections.Generic;
using System.Text;

namespace Jobs.Common.Factories
{
    public class SeededOfferHandleredFactory : OfferHandleredFactory
    {
        private const int NUMBEROFEMPLOYEES = 5;
        private const int NUMBEROFEMPLOYERS = 5;
        private const int NUMBEROFJOBDATAS= 15;
        private const int MAXNUMBEROFSKILLS = 10;
        private const int MAXNUMBEROFREFERENCES = 10;
        private Random random = new Random();
        private static bool seeded = false;
        private List<Employer> employers;
        private List<Employee> employees;

        public SeededOfferHandleredFactory(OfferHandler offerHandler) : base(offerHandler)
        {
            lock(this)
            {
                if(!seeded)
                {
                    this.employers = this.OfferHandler.Employers;
                    this.employees = this.OfferHandler.Employees;
                    this.SeedData();
                    seeded = true;
                }
            }
        }

        private List<Skill> GenerateSkills()
        {
            //TODO: NeededSkills, HadSkill -> List -> Dictionary
            List<Skill> skills = new List<Skill>();
            for (int j = 0; j < random.Next(MAXNUMBEROFSKILLS); j++)
            {
                string details = "details" + Convert.ToString(j);
                skills.Add(this.CreateSkill(Convert.ToString(random.Next()), random.Next(Skill.MinimumRangeOfKnowledge, Skill.MaximumRangeOfKnowledge), details));
            }
            return skills;
        }
        private List<Reference> GenerateReferences()
        {
            //TODO: NeededSkills, HadSkill -> List -> Dictionary
            List<Reference> references = new List<Reference>();
            for (int j = 0; j < random.Next(MAXNUMBEROFREFERENCES); j++)
            {
                string details = "details" + Convert.ToString(j);
                string url = "https://www.youtube.com/" + Convert.ToString(j); 
                references.Add(this.CreateReference(Convert.ToString(random.Next()), url, details));
            }
            return references;
        }
        
        private Employee GenerateEmployee(int i)
        {
            string name = "Employee" + Convert.ToString(i);
            Employee employee = this.CreateEmployee(name);
            employee.ProfessionData.References.AddRange(this.GenerateReferences());
            employee.ProfessionData.Skills.AddRange(this.GenerateSkills());
            return employee;
        }

        public void SeedData()
        {
            for (int i = 0; i < NUMBEROFEMPLOYEES; i++)
            {
                GenerateEmployee(i);
            }

            for (int i = 0; i < NUMBEROFEMPLOYERS; i++)
            {
                string name = "Employer" + Convert.ToString(i);
                this.CreateEmployer(name);
            }

            for (int i = 0; i < NUMBEROFJOBDATAS; i++)
            {
                int offerTypeNumber = random.Next(0, 3);
                int employerIndex = random.Next(0, NUMBEROFEMPLOYERS);
                Employer employer = employers[employerIndex];
                int employeeIndex = random.Next(0, NUMBEROFEMPLOYERS);
                Employee employee = employees[employerIndex];
                string name = "JobData" + Convert.ToString(i);
                JobData jobdata1 = this.CreateJobData(name, employer);
                jobdata1.Details = "Details" + Convert.ToString(i);
                jobdata1.NeededSkills.AddRange(this.GenerateSkills());
                JobOffer jobOffer = null;
                switch (offerTypeNumber)
                {
                    case 0:
                        jobOffer = this.OfferHandler.OfferJob(jobdata1, employer, employee);
                        break;
                    case 1:
                        jobOffer = this.OfferHandler.ApplyForJob(jobdata1, employee);
                        break;
                    case 2:
                        jobOffer = this.OfferHandler.AdvertiseJob(jobdata1, employer);
                        break;
                }
                int acceptTypeNumber = random.Next(0, 3);
                switch (acceptTypeNumber)
                {
                    case 0:
                        this.OfferHandler.AcceptJobOffer(jobOffer);
                        break;
                    case 1:
                        this.OfferHandler.DeclineJobOffer(jobOffer);
                        break;
                    case 2:
                        // (Not Accept, Not Decline)
                        break;
                }
            }


        }
    }
}

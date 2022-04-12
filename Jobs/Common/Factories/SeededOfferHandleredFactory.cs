using Jobs.Data;
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
        private const int NUMBEROFJOBDATAS= 5;
        private const int MAXNUMBEROFSKILLS = 10;
        private Random random = new Random();
        private static bool seeded = false;
        private List<Employer> employers;

        public SeededOfferHandleredFactory(OfferHandler offerHandler) : base(offerHandler)
        {
            lock(this)
            {
                if(!seeded)
                {
                    this.employers = this.OfferHandler.Employers;
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
                skills.Add(this.CreateSkill(Convert.ToString(random.Next()), random.Next(Skill.MinimumRangeOfKnowledge, Skill.MaximumRangeOfKnowledge)));
            }
            return skills;
        }
        public void SeedData()
        {
            for (int i = 0; i < NUMBEROFEMPLOYEES; i++)
            {
                string name = "Employee" + Convert.ToString(i);
                this.CreateEmployee(name).ProfessionData.Skills.AddRange(this.GenerateSkills());
            }

            for (int i = 0; i < NUMBEROFEMPLOYERS; i++)
            {
                string name = "Employer" + Convert.ToString(i);
                this.CreateEmployer(name);
            }

            for (int i = 0; i < NUMBEROFJOBDATAS; i++)
            {
                int employerIndex = random.Next(0, NUMBEROFEMPLOYERS - 1);
                Employer employer = employers[employerIndex];
                string name = "JobData" + Convert.ToString(i);
                JobData jobdata1 = this.CreateJobData(name, employer);
                jobdata1.Details = "Details" + Convert.ToString(i);
                jobdata1.NeededSkills.AddRange(this.GenerateSkills());
                this.OfferHandler.AdvertiseJob(jobdata1, employer);
            }


        }
    }
}

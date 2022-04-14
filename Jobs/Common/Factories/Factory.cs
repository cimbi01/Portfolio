using Jobs.Data;
using Jobs.Data.WorkingPerson;
using Jobs.Data.WorkingPerson.Employee;
using Jobs.Data.WorkingPerson.Employer;
using System;
using System.Collections.Generic;
using System.Text;

namespace Jobs.Common.Factories
{
    public class Factory
    {
        public Skill CreateSkill(string name, int rangeOfKnowledge, string? details = null)
        {
            Skill skill = new Skill(name, rangeOfKnowledge, details);
            return skill;
        }
        
        //TODO: test
        public Skill CreateSkill(Skill skill, Employee employee)
        {
            Skill createdSkill = new Skill(skill);
            employee.ProfessionData.Skills.Add(skill);
            return skill;
        }


        public Reference CreateReference(string name, string? url, string? details = null)
        {
            Reference reference = new Reference(name, url, details);
            return reference;
        }

        //TODO: test
        public Reference CreateReference(Reference reference, Employee employee)
        {
            Reference referenceCreated = new Reference(reference);
            employee.ProfessionData.References.Add(referenceCreated);
            return reference;
        }

        public Contact CreateContact()
        {
            return new Contact();
        }

        public Contact CreateContact(string name, string? emailAddress = null, string? phoneNumber = null)
        {
            Contact contact = new Contact(name, emailAddress, phoneNumber);
            return contact;
        }

        public ProfessionData CreateProfessionData()
        {
            ProfessionData professionData = new ProfessionData();
            return professionData;
        }

        public JobData CreateJobData(string name, Employer employer)
        {
            JobData jobData = new JobData(name, employer);
            return jobData;
        }
    }
}

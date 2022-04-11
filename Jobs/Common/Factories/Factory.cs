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

        public Reference CreateReference(string name, string url, string? details = null)
        {
            Uri urlGenerated = new Uri(url);
            Reference reference = new Reference(name, urlGenerated, details);
            return reference;
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
    }
}

using Jobs.Data.WorkingPerson;
using System;
using System.Collections.Generic;
using System.Text;

namespace Jobs.Common.Builders
{
    public class WorkingPersonBuilder : AbstractBuilder<WorkingPerson>
    {
        public WorkingPersonBuilder() : base()
        {

        }
        public void WithName(string name)
        {
            this.product.Contact.Name = name;
        }
        public void WithPhoneNumber(string phoneNumber)
        {
            this.product.Contact.PhoneNumber = phoneNumber;
        }
        public void WithEmailAddress(string emailAddress)
        {
            this.product.Contact.EmailAddress = emailAddress;
        }
        public override WorkingPerson Build()
        {
            WorkingPerson result = base.Build();
            //TODO: Add WorkingPerson to a CreatedWorkingPerson List
            return this.product;
        }
    }
}

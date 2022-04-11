using Jobs.Common.Factories;
using System;
using System.Collections.Generic;

namespace Jobs.Data.WorkingPerson.Employee
{
    public class Employee : WorkingPerson
    {
        internal Employee(string name) : base(name)
        {
            this.ProfessionData = new Factory().CreateProfessionData();
        }

        public ProfessionData ProfessionData { get; }
    }
}
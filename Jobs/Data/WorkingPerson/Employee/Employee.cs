using System;
using System.Collections.Generic;

namespace Jobs.Data.WorkingPerson.Employee
{
    public class Employee : WorkingPerson
    {
        public Employee(string name) : base(name)
        {}

        public ProfessionData ProfessionData { get; set; } = new ProfessionData();
    }
}
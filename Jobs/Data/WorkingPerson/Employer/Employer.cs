using System;

namespace Jobs.Data.WorkingPerson.Employer
{
    public class Employer : WorkingPerson
    {
        public Employer() : base()
        {

        }
        public Employer(Employer employer) : base(employer)
        {

        }
        public Employer(string name) : base(name)
        {
        }
    }
}
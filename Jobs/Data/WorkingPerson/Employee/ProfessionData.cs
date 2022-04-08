using System.Collections.Generic;

namespace Jobs.Data.WorkingPerson.Employee
{
    public class ProfessionData
    {
        public List<Skill> Skills { get; } = new List<Skill>();
        public List<Reference> References { get; } = new List<Reference>();
    }
}
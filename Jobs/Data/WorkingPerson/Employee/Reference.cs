using System;

namespace Jobs.Data.WorkingPerson.Employee
{
    public class Reference
    {
        public string Name { get; set; }

        /// <summary>
        /// Details of reference. Can be null if Employee giving no details.
        /// </summary>
        public string? Details { get; set; }


        /// <summary>
        /// URL of reference. Can be null if Employee giving no URL.
        /// </summary>
        public Uri Url { get; set; }
    }
}
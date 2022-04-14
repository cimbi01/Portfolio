using System;
using System.ComponentModel.DataAnnotations;

namespace Jobs.Data.WorkingPerson.Employee
{
    public class Reference
    {
        public Reference()
        {

        }

        //TODO: Test
        public Reference(Reference reference)
        {
            this.Name = reference.Name;
            this.Details = reference.Details;
            this.Url = reference.Url;
        }

        public Reference(string name, string? url, string? details = null)
        {
            this.Name = name;
            this.Details = details;
            this.Url = url;
        }

        public string Name { get; set; }

        /// <summary>
        /// Details of reference. Can be null if Employee giving no details.
        /// </summary>
        public string? Details { get; set; }


        [Url]
        /// <summary>
        /// URL of reference. Can be null if Employee giving no URL.
        /// </summary>
        public string? Url { get; set; }
    }
}
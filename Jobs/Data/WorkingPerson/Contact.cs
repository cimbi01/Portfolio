using System.ComponentModel.DataAnnotations;

namespace Jobs.Data.WorkingPerson
{
    public class Contact
    {
        public Contact()
        {

        }
        public Contact(string name, string? emailAddress = null, string? phoneNumber = null)
        {
            this.Name = name;
            this.EmailAddress = emailAddress;
            this.PhoneNumber = phoneNumber;
        }

        [Required(ErrorMessage ="Required")]
        public string Name { get; set; }

        /// <summary>
        /// Email address of <see cref="WorkingPerson"/>. Can be null if no email address is given by <see cref="WorkingPerson"/>
        /// </summary>
        [EmailAddress]
        public string? EmailAddress { get; set; }

        /// <summary>
        /// Phone number of <see cref="WorkingPerson"/>. Can be null if no phone number is given by <see cref="WorkingPerson"/>
        /// </summary>
        [Phone]
        public string? PhoneNumber { get; set; }
    }
}
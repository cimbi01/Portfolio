using Jobs.Data;
using Jobs.Data.WorkingPerson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVC.Models.ViewModels
{
    public class WorkingPersonViewModel
    {
        public bool Ascending { get; set; }
        public string? OrderString { get; set; }
        public Contact? Contact { get; set; }
        public List<JobOffer>? ManagedAdvertisements { get; set; }
        public List<JobOffer>? ManagedJobOffers{ get; set; }
        public string? JobDataName { get; set; }

        public WorkingPersonViewModel()
        {
        }
    }
}

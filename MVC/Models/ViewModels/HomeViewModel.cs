using Jobs.Common;
using Jobs.Data.WorkingPerson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVC.Models.ViewModels
{
    public class HomeViewModel
    {
        private OfferHandler offerHandler;
        public HomeViewModel(OfferHandler offerHandler)
        {
            this.offerHandler = offerHandler;
        }
        public WorkingPerson? CreatedUser { get; set; }
        public List<WorkingPerson> WorkingPeople
        {
            get
            {
                List<WorkingPerson> workingPeople = new List<WorkingPerson>();
                this.offerHandler.Employees.ForEach(emp => workingPeople.Add(emp));
                this.offerHandler.Employers.ForEach(emp => workingPeople.Add(emp));
                return workingPeople;
            }
        }
    }
}

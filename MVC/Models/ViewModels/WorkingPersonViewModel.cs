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
        private WorkingPerson workingPerson;

        public WorkingPerson WorkingPerson
        {
            get => workingPerson;
            set
            {
                workingPerson = value;
                this.workingPersonChangedEvent?.Invoke(this, EventArgs.Empty);
            }
        }

        private event EventHandler workingPersonChangedEvent;
        public event EventHandler OnWorkingPersonChangedEvent
        {
            add
            {
                this.workingPersonChangedEvent += value;
            }
            remove
            {
                this.workingPersonChangedEvent -= value;
            }
        }
        public WorkingPersonViewModel()
        {
            this.OnWorkingPersonChangedEvent += (triggerer, eventargs) =>
            {
                this.ReceivedJobOffers = this.WorkingPerson.ReceivedJobOffers;
                this.OfferedJobOffers = this.WorkingPerson.OfferedJobOffers;
            };
        }

        public WorkingPersonViewModel(WorkingPerson workingPerson) : base()
        {
            this.WorkingPerson = workingPerson;
        }

        public List<JobOffer>? Advertisements { get; set; }
        public List<JobOffer> JobOffers
        {
            get
            {
                List<JobOffer> jobOffers = ReceivedJobOffers;
                jobOffers.AddRange(this.OfferedJobOffers);
                return jobOffers;
            }
        }
        public List<JobOffer> ReceivedJobOffers { get; set; }
        public List<JobOffer> OfferedJobOffers { get; set; }
    }
}

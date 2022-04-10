using System;
using System.Collections.Generic;
using System.Text;

namespace Jobs.Common.Builders
{
    public abstract class AbstractBuilder<T> where T : new()
    {
        //TODO: Factory for WorkingPerson -> Employee, Employer
        //TODO: Builder for JobOffer
        //TODO: Builder for JobData
        //TODO: Builder for FinishedJobData
        protected T product;
        public AbstractBuilder()
        {
            this.product = new T();
        }

        public virtual T Build()
        {
            return product;
        }
    }
}

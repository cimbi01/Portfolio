using System;
using System.Collections.Generic;
using System.Text;

namespace Jobs.Common.Builders
{
    public abstract class AbstractBuilder<T> where T : new()
    {
        //TODO: Builder for JobData
        //TODO: Builder for FinishedJobData
        //TODO: Builder for JobOffer -> add to list
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

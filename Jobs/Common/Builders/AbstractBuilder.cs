using Jobs.Data.WorkingPerson.Employee;
using System;
using System.Collections.Generic;
using System.Text;

namespace Jobs.Common.Builders
{
    public abstract class AbstractBuilder<T>
    {
        protected bool Initialized { get; set; } = false;
        protected List<Action> SetActions { get; } = new List<Action>();
        // base fields

        //TODO: Builder for JobOffer -> add to list
        protected T product;

        /// <summary>
        /// Initializes product with all the essential fields and changed <see cref="Initialized"/> to true, if all essential field is not null
        /// If some basefields is null then throws exception.
        /// </summary>
        protected abstract void Initialize();

        /// <summary>
        /// Call <see cref="Initialize"/> if <see cref="Initialized"/> is true
        /// </summary>
        protected void TryInitialize()
        {
            if(!this.Initialized)
            {
                this.Initialize();
            }
        }

        //TODO: Test -> extends -> field: TrySetCalledTime++
        /// <summary>
        /// Trying to set properties.
        /// If trying is unsucceessfull, then adds the action to <see cref="SetActions"/> for later invoke.
        /// </summary>
        /// <param name="withPropertySet"></param>
        protected void TrySet(Action withPropertySet)
        {
            try
            {
                this.TryInitialize();
                withPropertySet();
            }
            catch(Exception except)
            {
                this.SetActions.Add(withPropertySet);
            }
        }

        /// <summary>
        /// Invokes all <see cref="Action"/> from <see cref="SetActions"/> then returns built product.
        /// </summary>
        /// <returns></returns>
        public virtual T Build()
        {
            this.TryInitialize();
            for (int i = 0; i < this.SetActions.Count; i++)
            {
                Action action = this.SetActions[i];
                action();
            }
            return product;
        }
    }
}

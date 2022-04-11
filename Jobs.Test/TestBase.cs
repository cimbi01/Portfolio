using Jobs.Common;
using Jobs.Common.Factories;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Jobs.Test
{
    class TestBase
    {
        protected OfferHandler offerHandler;
        protected Factory factory;

        [SetUp]
        public virtual void Init()
        {
            this.offerHandler = new OfferHandler();
            this.factory = new OfferHandleredFactory(this.offerHandler);
        }

        protected OfferHandleredFactory WorkingPersonFactory => (OfferHandleredFactory)this.factory;
    }
}

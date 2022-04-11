using System;
using System.Runtime.Serialization;

namespace Jobs.Data.Exceptions
{
    public class JobNotStartedException : Exception
    {
        public JobNotStartedException()
        {
        }
    }
}
using System;
using System.Runtime.Serialization;

namespace Jobs.Exceptions
{
    public class AlreadySetException : Exception
    {
        public AlreadySetException()
        {
        }
    }
}
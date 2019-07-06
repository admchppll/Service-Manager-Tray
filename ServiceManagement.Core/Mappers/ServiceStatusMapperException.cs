using System;
using System.Runtime.Serialization;

namespace ServiceManagement.Core.Mappers
{
    /// <summary>
    /// Exception to be thrown when conversion isn't possible
    /// </summary>
    /// <seealso cref="Exception" />
    [Serializable]
    public class ServiceStatusMapperException : Exception
    {
        public ServiceStatusMapperException()
        {
        }

        public ServiceStatusMapperException(string message)
            : base(message)
        {
        }

        public ServiceStatusMapperException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        protected ServiceStatusMapperException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
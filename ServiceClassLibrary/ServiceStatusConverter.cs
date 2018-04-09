using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace ServiceClassLibrary
{
    public static class ServiceStatusConverter
    {
        #region Enum Values
        /// <summary>
        /// Gets the <see cref="ServiceStatus"/> values from it's enum.
        /// </summary>
        /// <value>
        /// The service status values.
        /// </value>
        public static IEnumerable<byte> ServiceStatusValues => Enum.GetValues(typeof(ServiceStatus)).Cast<byte>();
        /// <summary>
        /// Gets the <see cref="ServiceControllerStatus"/> values.
        /// </summary>
        /// <value>
        /// The service controller status values.
        /// </value>
        public static IEnumerable<byte> ServiceControllerStatusValues => Enum.GetValues(typeof(ServiceControllerStatus)).Cast<byte>();
        #endregion
        #region To Service Status Methods
        /// <summary>
        /// Converts a <see cref="ServiceControllerStatus"/> to a <see cref="ServiceStatus"/>
        /// </summary>
        /// <param name="status">The <see cref="ServiceControllerStatus"/> to convert</param>
        /// <returns>The converted status</returns>
        /// <exception cref="ServiceClassLibrary.ServiceStatusConverter.ServiceStatusConversionException">Value out of bounds. The value selected does not exist in ServiceStatus</exception>
        public static ServiceStatus FromServiceControllerStatus(ServiceControllerStatus status)
        {
            byte originalValue = (byte)status;

            if (!ServiceStatusValues.Any(s => s.Equals(originalValue)))
                throw new ServiceStatusConversionException("Value out of bounds. The value selected does not exist in ServiceStatus");

            return (ServiceStatus)originalValue;
        }
        #endregion
        #region From Service Status Methods
        /// <summary>
        /// Converts a <see cref="ServiceStatus"/> to a <see cref="ServiceControllerStatus"/>
        /// </summary>
        /// <param name="status">The <see cref="ServiceStatus"/> to convert</param>
        /// <returns>The converted status</returns>
        /// <exception cref="ServiceClassLibrary.ServiceStatusConverter.ServiceStatusConversionException">Value out of bounds. The value selected does not exist in ServiceStatus</exception>
        public static ServiceControllerStatus ToServiceControllerStatus(ServiceStatus status)
        {
            byte originalValue = (byte)status;

            if (!ServiceControllerStatusValues.Any(s => s.Equals(originalValue)))
                throw new ServiceStatusConversionException("Value out of bounds. The value selected does not exist in ServiceControllerStatus");

            return (ServiceControllerStatus)originalValue;
        }
        #endregion
        /// <summary>
        /// Exception to be thrown when conversion isn't possible
        /// </summary>
        /// <seealso cref="System.Exception" />
        public class ServiceStatusConversionException : Exception
        {
            public ServiceStatusConversionException() { }
            public ServiceStatusConversionException(string message)
                : base(message)
            { }
            public ServiceStatusConversionException(string message, Exception innerException)
                : base(message, innerException)
            { }
        }
    }
}

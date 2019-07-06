using System;
using System.ServiceProcess;

namespace ServiceManagement.Core.Models
{
    public static class ServiceStatusConverter
    {
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
            ServiceStatus output;

            try
            {
                output = (ServiceStatus)originalValue;
            }
            catch (Exception)
            {
                throw new ServiceStatusConversionException("Value out of bounds. The value selected does not exist in ServiceStatus");
            }

            return output;
        }

        #endregion To Service Status Methods

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
            ServiceControllerStatus output;

            try
            {
                output = (ServiceControllerStatus)originalValue;
            }
            catch (Exception)
            {
                throw new ServiceStatusConversionException("Value out of bounds. The value selected does not exist in ServiceControllerStatus");
            }

            return output;
        }

        #endregion From Service Status Methods

        /// <summary>
        /// Exception to be thrown when conversion isn't possible
        /// </summary>
        /// <seealso cref="System.Exception" />
        public class ServiceStatusConversionException : Exception
        {
            public ServiceStatusConversionException()
            {
            }

            public ServiceStatusConversionException(string message)
                : base(message)
            {
            }

            public ServiceStatusConversionException(string message, Exception innerException)
                : base(message, innerException)
            {
            }
        }
    }
}
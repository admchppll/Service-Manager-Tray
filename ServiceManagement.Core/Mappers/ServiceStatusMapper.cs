using ServiceManagement.Core.Models;
using System;
using System.ServiceProcess;

namespace ServiceManagement.Core.Mappers
{
    internal static class ServiceStatusMapper
    {
        #region To Service Status Methods

        /// <summary>
        /// Converts a <see cref="ServiceControllerStatus"/> to a <see cref="ServiceStatus"/>
        /// </summary>
        /// <param name="status">The <see cref="ServiceControllerStatus"/> to convert</param>
        /// <returns>The converted status</returns>
        /// <exception cref="ServiceStatusMapperException">Value out of bounds. The value selected does not exist in ServiceStatus</exception>
        public static ServiceStatus FromServiceControllerStatus(ServiceControllerStatus status)
        {
            byte originalValue = (byte)status;
            ServiceStatus output;

            try
            {
                output = (ServiceStatus)originalValue;
            }
            catch (Exception ex)
            {
                throw new ServiceStatusMapperException("Value out of bounds. The value selected does not exist in ServiceStatus", ex);
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
        /// <exception cref="ServiceStatusMapperException">Value out of bounds. The value selected does not exist in ServiceStatus</exception>
        public static ServiceControllerStatus ToServiceControllerStatus(ServiceStatus status)
        {
            byte originalValue = (byte)status;
            ServiceControllerStatus output;

            try
            {
                output = (ServiceControllerStatus)originalValue;
            }
            catch (Exception ex)
            {
                throw new ServiceStatusMapperException("Value out of bounds. The value selected does not exist in ServiceControllerStatus", ex);
            }

            return output;
        }

        #endregion From Service Status Methods
    }
}
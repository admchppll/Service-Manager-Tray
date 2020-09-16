using ServiceManagement.Core.Clients;
using ServiceManagement.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ServiceManagement.Services.Interfaces
{
    public interface IStatusService : IServiceClient
    {
        /// <summary>
        /// Get the status of a service
        /// </summary>
        /// <param name="serviceName">Name of the service to get the status</param>
        /// <returns>The services status</returns>
        Task<ServiceStatus> GetStatus(string serviceName);

        /// <summary>
        /// Set Status on a Service
        /// </summary>
        /// <param name="service">The <see cref="Service">Service</see> to update the status on</param>
        /// <returns>The service with current status</returns>
        Task<Service> SetStatus(Service service);

        /// <summary>
        /// Set Status on a collection of <see cref="Service">services</see>
        /// </summary>
        /// <param name="service">The <see cref="Service">Service</see> to update the status on</param>
        /// <returns>The service with current status</returns>
        Task<IEnumerable<Service>> SetStatus(IEnumerable<Service> service);
    }
}
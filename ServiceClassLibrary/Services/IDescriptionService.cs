using System.Collections.Generic;
using System.Threading.Tasks;

namespace ServiceManagement.Core.Services
{
    public interface IDescriptionService
    {
        /// <summary>
        /// Get the description for a service
        /// </summary>
        /// <param name="serviceName">Name of the service to get the status</param>
        /// <returns>The services description</returns>
        Task<string> GetDescription(string serviceName);

        /// <summary>
        /// Set Description on a Service
        /// </summary>
        /// <param name="service">The <see cref="Service">Service</see> to update the description on</param>
        /// <returns>The service with current description</returns>
        Task<Service> SetDescription(Service service);

        /// <summary>
        /// Set Description on a collection of <see cref="Service">services</see>
        /// </summary>
        /// <param name="service">The <see cref="Service">Services</see> to update the description on</param>
        /// <returns>The service with description</returns>
        Task<IEnumerable<Service>> SetDescription(IEnumerable<Service> services);
    }
}
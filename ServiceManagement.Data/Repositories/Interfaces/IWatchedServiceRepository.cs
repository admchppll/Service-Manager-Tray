using ServiceManagement.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ServiceManagement.Data.Repositories.Interfaces
{
    public interface IWatchedServiceRepository
    {
        Task<IEnumerable<Service>> GetWatchedServices();

        bool SaveWatchedServices(IEnumerable<Service> services);
    }
}
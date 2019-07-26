using ServiceManagement.Core.Models;
using System.Collections.Generic;

namespace ServiceManagement.Data.Repositories
{
    public interface IWatchedServiceRepository
    {
        IEnumerable<Service> GetWatchedServices();

        bool SaveWatchedServices(IEnumerable<Service> services);
    }
}
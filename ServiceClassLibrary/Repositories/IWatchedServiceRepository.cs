using System.Collections.Generic;

namespace ServiceManagement.Core.Repositories
{
    public interface IWatchedServiceRepository
    {
        IEnumerable<Service> GetWatchedServices();

        bool SaveWatchedServices(IEnumerable<Service> services);
    }
}
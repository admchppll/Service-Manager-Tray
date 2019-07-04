using System.Collections.Generic;

namespace ServiceClassLibrary.Repositories
{
    public interface IWatchedServiceRepository
    {
        IEnumerable<Service> GetWatchedServices();
        bool SaveWatchedServices(IEnumerable<Service> services);
    }
}
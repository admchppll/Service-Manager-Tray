using System.Collections.Generic;

namespace ServiceManagement.Core.Repositories
{
    public interface IServiceRepository
    {
        ICollection<Service> AddWatchedService(Service service);

        Service FindService();

        ICollection<Service> GetAllServices();

        ICollection<Service> GetWatchedServices();
    }
}
using System.Collections.Generic;

namespace ServiceClassLibrary.Repositories
{
    public interface IServiceRepository
    {
        ICollection<Service> AddWatchedService(Service service);

        Service FindService();

        ICollection<Service> GetAllServices();

        ICollection<Service> GetWatchedServices();
    }
}
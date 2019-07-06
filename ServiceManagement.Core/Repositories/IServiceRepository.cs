using ServiceManagement.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ServiceManagement.Core.Repositories
{
    public interface IServiceRepository
    {
        ICollection<Service> AddWatchedService(Service service);

        Service FindService();

        Task<ICollection<Service>> GetAllServices();

        ICollection<Service> GetWatchedServices();
    }
}
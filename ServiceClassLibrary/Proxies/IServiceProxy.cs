using ServiceManagement.Core.Models;
using System.Threading.Tasks;

namespace ServiceManagement.Core.Proxies
{
    public interface IServiceProxy
    {
        Task<ServiceStatus> StartService(Service service);

        Task<ServiceStatus> StopService(Service service);

        Task<ServiceStatus> ToggleService(Service service);
    }
}
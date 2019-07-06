using System.Threading.Tasks;

namespace ServiceManagement.Core.Proxies
{
    public class ServiceProxy : IServiceProxy
    {
        public Task<ServiceStatus> StartService(Service service)
        {
            //Holding comment
            return Task.FromResult(ServiceStatus.Running);
        }

        public Task<ServiceStatus> StopService(Service service)
        {
            //Holding comment
            return Task.FromResult(ServiceStatus.Stopped);
        }

        public Task<ServiceStatus> ToggleService(Service service)
        {
            //Holding comment
            return Task.FromResult(ServiceStatus.Running);
        }
    }
}
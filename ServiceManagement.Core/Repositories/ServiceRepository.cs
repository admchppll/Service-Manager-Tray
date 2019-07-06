using ServiceManagement.Core.Models;
using ServiceManagement.Core.Services;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Threading.Tasks;

namespace ServiceManagement.Core.Repositories
{
    public class ServiceRepository : IServiceRepository
    {
        private readonly IStatusService _statusService;
        private readonly IDescriptionService _descriptionService;

        public ServiceRepository(
            IStatusService statusService,
            IDescriptionService descriptionService)
        {
            _statusService = statusService;
            _descriptionService = descriptionService;
        }

        public async Task<ICollection<Service>> GetAllServices()
        {
            var services = ServiceController.GetServices()
                .Select(service => new Service()
                {
                    Name = service.DisplayName,
                    MachineName = service.ServiceName
                });

            services = await _descriptionService.SetDescription(services);
            services = await _statusService.SetStatus(services);

            return services.ToList();
        }

        public ICollection<Service> GetWatchedServices()
        {
            return new List<Service>();
        }

        public Service FindService()
        {
            return new Service();
        }

        public ICollection<Service> AddWatchedService(Service service)
        {
            return new List<Service>();
        }
    }
}
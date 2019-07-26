using ServiceManagement.Core.Mappers;
using ServiceManagement.Core.Models;
using ServiceManagement.Core.Services;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Threading.Tasks;

namespace ServiceManagement.Data.Repositories
{
    public class ServiceRepository : IServiceRepository
    {
        private readonly IStatusService _statusService;
        private readonly IDescriptionService _descriptionService;
        private IEnumerable<Service> _cachedServices;

        public ServiceRepository(
            IStatusService statusService,
            IDescriptionService descriptionService)
        {
            _statusService = statusService;
            _descriptionService = descriptionService;

            _cachedServices = new List<Service>();
        }

        public async Task<ICollection<Service>> GetAllServices()
        {
            var services = _cachedServices.Any()
                ? await GetServicesFromCache()
                : await GetServicesFromSystem();

            return services.ToList();
        }

        private async Task<IEnumerable<Service>> GetServicesFromCache()
        {
            var services = await _statusService.SetStatus(_cachedServices);
            _cachedServices = services;
            return services;
        }

        private async Task<IEnumerable<Service>> GetServicesFromSystem()
        {
            var services = ServiceController.GetServices()
                .Select(service => new Service()
                {
                    Name = service.DisplayName,
                    MachineName = service.ServiceName,
                    Status = ServiceStatusMapper.FromServiceControllerStatus(service.Status)
                });

            services = await _descriptionService.SetDescription(services);

            _cachedServices = services;
            return services;
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
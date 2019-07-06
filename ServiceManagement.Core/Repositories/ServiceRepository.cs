using ServiceManagement.Core.Models;
using ServiceManagement.Core.Services;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;

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

        public ICollection<Service> GetAllServices()
        {
            var services = (ICollection<Service>)ServiceController.GetServices()
                .Select(service => new Service()
                {
                    Name = service.DisplayName,
                    MachineName = service.ServiceName
                });

            _descriptionService.SetDescription(services);
            _statusService.SetStatus(services);

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
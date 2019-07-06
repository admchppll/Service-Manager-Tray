using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Threading.Tasks;

namespace ServiceManagement.Core.Services
{
    public class DescriptionService : IDescriptionService
    {
        public async Task<string> GetDescription(string serviceName)
        {
            using (ManagementObject wmiService = new ManagementObject($"Win32_Service.Name='{serviceName}'"))
            {
                wmiService.Get();
                return await Task.FromResult(wmiService["Description"] == null ? "" : wmiService["Description"].ToString());
            }
        }

        public async Task<Service> SetDescription(Service service)
        {
            service.Description = await GetDescription(service.Name);
            return service;
        }

        public async Task<IEnumerable<Service>> SetDescription(IEnumerable<Service> services)
        {
            var tasks = services.Select(s => SetDescription(s));
            return await Task.WhenAll(tasks);
        }
    }
}
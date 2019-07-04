using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Threading.Tasks;

namespace ServiceClassLibrary.Services
{
    public class StatusService : IStatusService
    {
        public async Task<Service> SetStatus(Service service)
        {
            service.Status = await GetStatus(service.Name);
            return service;
        }

        public async Task<ServiceStatus> GetStatus(string serviceName)
        {
            using (ServiceController sc = new ServiceController(serviceName))
            {
                try
                {
                    return await Task.FromResult((ServiceStatus)(byte)sc.Status);
                }
                catch (InvalidOperationException)
                {
                    return ServiceStatus.NotExists;
                }
                catch (System.ComponentModel.Win32Exception)
                {
                    return ServiceStatus.NoAccess;
                }
                //If any other exceptions occur, we don't know what happened.
                catch (Exception)
                {
                    return ServiceStatus.Unknown;
                }
            }
        }

        public async Task<IEnumerable<Service>> SetStatus(IEnumerable<Service> services)
        {
            var tasks = services.Select(s => SetStatus(s));
            return await Task.WhenAll(tasks);
        }
    }
}
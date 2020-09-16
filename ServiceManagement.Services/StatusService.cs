using ServiceManagement.Core.Mappers;
using ServiceManagement.Core.Models;
using ServiceManagement.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Threading.Tasks;

namespace ServiceManagement.Services
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
                    var status = ServiceStatusMapper.FromServiceControllerStatus(sc.Status);
                    return await Task.FromResult(status);
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

        private const string AccessDeniedText = "Access is denied";

        private readonly ServiceStatus[] _canToggle = {
                ServiceStatus.Running,
                ServiceStatus.Stopped,
                ServiceStatus.Paused
            };

        private readonly ServiceStatus[] _pendingActionStatuses = {
                ServiceStatus.StartPending,
                ServiceStatus.StopPending,
                ServiceStatus.ContinuePending,
                ServiceStatus.PausePending
            };

        private readonly IStatusService _statusService;

        public async Task<ServiceStatus> StartService(Service service)
        {
            return await StartService(service.MachineName);
        }

        public async Task<ServiceStatus> StartService(string serviceName)
        {
            var currentStatus = await _statusService.GetStatus(serviceName);

            if (currentStatus == ServiceStatus.Running)
                return currentStatus;

            using (ServiceController sc = new ServiceController(serviceName))
            {
                try
                {
                    //TODO: Abstract this code to be more reusable. (Delegate?)
                    if (currentStatus == ServiceStatus.Paused)
                    {
                        sc.Continue();
                    }
                    else
                    {
                        sc.Start();
                    }
                    sc.WaitForStatus(ServiceControllerStatus.Running, new TimeSpan(0, 0, 30));
                }
                catch (InvalidOperationException ex)
                {
                    return ex.InnerException.Message == AccessDeniedText
                        ? ServiceStatus.NoAccess
                        : ServiceStatus.NotExists;
                }
                catch (System.ComponentModel.Win32Exception)
                {
                    return ServiceStatus.NoAccess;
                }
                //If any other exceptions occur, we dont know what happened.
                catch (Exception)
                {
                    return ServiceStatus.Unknown;
                }

                return ServiceStatusMapper.FromServiceControllerStatus(sc.Status);
            }
        }

        public async Task<ServiceStatus> StopService(Service service)
        {
            return await StopService(service.MachineName);
        }

        public async Task<ServiceStatus> StopService(string serviceName)
        {
            var currentStatus = await _statusService.GetStatus(serviceName);

            if (currentStatus == ServiceStatus.Stopped)
                return currentStatus;

            using (ServiceController sc = new ServiceController(serviceName))
            {
                try
                {
                    //TODO: Abstract this code to be more reusable. (Delegate?)
                    if (sc.CanStop)
                    {
                        sc.Stop();
                        sc.WaitForStatus(ServiceControllerStatus.Running, new TimeSpan(0, 0, 30));
                    }
                    else
                    {
                        return ServiceStatus.NotStop;
                    }
                }
                catch (InvalidOperationException ex)
                {
                    return ex.InnerException.Message == AccessDeniedText
                        ? ServiceStatus.NoAccess
                        : ServiceStatus.NotExists;
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
                return ServiceStatusMapper.FromServiceControllerStatus(sc.Status);
            }
        }

        public async Task<ServiceStatus> ToggleService(Service service)
        {
            return await ToggleService(service.MachineName);
        }

        public async Task<ServiceStatus> ToggleService(string serviceName)
        {
            var currentStatus = await _statusService.GetStatus(serviceName);

            if (_canToggle.Contains(currentStatus))
            {
                switch (currentStatus)
                {
                    case ServiceStatus.Running:
                        return await StopService(serviceName);

                    case ServiceStatus.Paused:
                    case ServiceStatus.Stopped:
                        return await StartService(serviceName);
                }
            }

            if (_pendingActionStatuses.Contains(currentStatus))
                throw new InvalidOperationException($"Cannot toggle {serviceName} whilst there is already a pending action ({currentStatus})");

            return currentStatus;
        }
    }
}
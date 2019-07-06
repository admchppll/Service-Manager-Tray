using ServiceManagement.Core.Mappers;
using ServiceManagement.Core.Models;
using ServiceManagement.Core.Services;
using System;
using System.Linq;
using System.ServiceProcess;
using System.Threading.Tasks;

namespace ServiceManagement.Core.Clients
{
    public class ServiceClient : IServiceClient
    {
        private const string AccessDeniedText = "Access is denied";

        private readonly IStatusService _statusService;

        public ServiceClient(IStatusService statusService)
        {
            _statusService = statusService;
        }

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
            ServiceStatus[] CanToggle = {
                ServiceStatus.Running,
                ServiceStatus.Stopped,
                ServiceStatus.Paused
            };

            ServiceStatus[] PendingActionStatus = {
                ServiceStatus.StartPending,
                ServiceStatus.StopPending,
                ServiceStatus.ContinuePending,
                ServiceStatus.PausePending
            };

            var currentStatus = await _statusService.GetStatus(serviceName);

            if (CanToggle.Contains(currentStatus))
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

            if (PendingActionStatus.Contains(currentStatus))
                throw new InvalidOperationException($"Cannot toggle {serviceName} whilst there is already a pending action ({currentStatus})");

            return currentStatus;
        }
    }
}
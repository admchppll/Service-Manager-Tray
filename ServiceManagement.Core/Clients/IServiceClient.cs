﻿using ServiceManagement.Core.Models;
using System.Threading.Tasks;

namespace ServiceManagement.Core.Clients
{
    public interface IServiceClient
    {
        Task<ServiceStatus> StartService(Service service);

        Task<ServiceStatus> StartService(string serviceName);

        Task<ServiceStatus> StopService(Service service);

        Task<ServiceStatus> StopService(string serviceName);

        Task<ServiceStatus> ToggleService(Service service);

        Task<ServiceStatus> ToggleService(string serviceName);
    }
}
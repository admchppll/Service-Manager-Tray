using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Management;
using System.ServiceProcess;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ServiceManagement.Core.Models
{
    public class Service
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// A user friendly name for the service
        /// </value>
        public string Name
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>
        /// The description of the service
        /// </value>
        public string Description
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets the name of the machine.
        /// </summary>
        /// <value>
        /// The actual name of the service on the current machine.
        /// </value>
        public string MachineName
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets the status.
        /// </summary>
        /// <value>
        /// The current status of the service.
        /// </value>
        /// <remarks>We don't store this in the configuration file.</remarks>
        [JsonIgnore]
        public ServiceStatus Status
        {
            get; set;
        }

        /// <summary>
        /// Gets the default configuration file location.
        /// </summary>
        /// <returns>The absolute path of the configuration file in the system</returns>
        public static string ConfigFileLocation()
        {
            return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "ServiceManager", "config.json").ToString();
        }

        /// <summary>
        /// Gets the status of a given service name.
        /// </summary>
        /// <param name="Name">The name of the service.</param>
        /// <returns>The current <see cref="ServiceStatus"/></returns>
        public static ServiceStatus GetStatus(string Name)
        {
            using (ServiceController sc = new ServiceController(Name))
            {
                try
                {
                    return (ServiceStatus)(byte)sc.Status;
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

        /// <summary>
        /// Gets the status of the current service.
        /// </summary>
        /// <returns>The current <see cref="ServiceStatus"/></returns>
        public ServiceStatus GetStatus()
        {
            return Status = GetStatus(this.MachineName);
        }

        /// <summary>
        /// Gets the status of the current service.
        /// </summary>
        /// <returns>The current <see cref="ServiceStatus"/></returns>
        public async Task<ServiceStatus> GetStatusAsync()
        {
            return GetStatus();
        }

        /// <summary>
        /// Gets the description of a given service from the system.
        /// </summary>
        /// <param name="serviceName">Name of the service.</param>
        /// <returns>The description and set the <see cref="Description"/> field.</returns>
        public static string GetDescription(string serviceName)
        {
            using (ManagementObject wmiService = new ManagementObject("Win32_Service.Name='" + serviceName + "'"))
            {
                wmiService.Get();
                return wmiService["Description"] == null ? "" : wmiService["Description"].ToString();
            }
        }

        /// <summary>
        /// Gets the description of the current service from the system.
        /// </summary>
        /// <returns>The description</returns>
        public string GetDescription()
        {
            return Description = Status != ServiceStatus.NotExists
                        ? GetDescription(MachineName)
                        : null;
        }

        /// <summary>
        /// Lists all services in the system
        /// </summary>
        /// <returns></returns>
        public static ObservableCollection<Service> GetAllServices()
        {
            List<Service> services = ServiceController.GetServices().Select(x => new Service()
            {
                Name = x.DisplayName,
                MachineName = x.ServiceName,
                Description = GetDescription(x.ServiceName),
                Status = GetStatus(x.ServiceName)
            }).ToList();

            return new ObservableCollection<Service>(services);
        }

        /// <summary>
        /// Updates the status of all services asynchronously
        /// </summary>
        /// <param name="serviceList">The <see cref="ObservableCollection{T}"/> of services to update</param>
        public static void UpdateStatus(ref ObservableCollection<Service> serviceList)
        {
            foreach (Service serv in serviceList)
            {
                serv.GetStatus();
            }
        }

        /// <summary>
        /// Deserializes Services from the default user file to an <see cref="ObservableCollection{T}"/>.
        /// </summary>
        /// <returns>A list of services. Or an empty list if no file exists.</returns>
        public static ObservableCollection<Service> DeserializeFromFile()
        {
            return DeserializeFromFile(ConfigFileLocation());
        }

        /// <summary>
        /// Deserializes Services from a file to an <see cref="ObservableCollection{T}"/>.
        /// </summary>
        /// <param name="servicesFile">The services configuration to import from file.</param>
        /// <returns>A list of services. Or an empty list if no file exists.</returns>
        public static ObservableCollection<Service> DeserializeFromFile(string servicesFile)
        {
            ObservableCollection<Service> result = new ObservableCollection<Service>();

            //If config file doesn't exist
            if (!File.Exists(servicesFile))
                return result;

            //Deserialize the configuration
            using (StreamReader file = File.OpenText(servicesFile))
            {
                JsonSerializer serializer = new JsonSerializer();
                result = (ObservableCollection<Service>)serializer.Deserialize(file, typeof(ObservableCollection<Service>));
            }

            return result;
        }

        /// <summary>
        /// Serializes list of services to file at default location.
        /// </summary>
        /// <param name="services">The services.</param>
        /// <returns>True, if the serialisation is successful. Otherwise, false.</returns>
        public static async Task<bool> SerializeToFile(ObservableCollection<Service> services)
        {
            return SerializeToFile(services, ConfigFileLocation());
        }

        /// <summary>
        /// Serializes list of services to file at given location.
        /// </summary>
        /// <param name="services">The services.</param>
        /// <param name="writeLocation">The file path to serialize the data to</param>
        /// <returns>True, if the serialisation is successful. Otherwise, false.</returns>
        public static bool SerializeToFile(ObservableCollection<Service> services, string writeLocation)
        {
            using (StreamWriter file = File.CreateText(writeLocation))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(file, services);
            }

            return true;
        }

        /// <summary>
        /// Set the Selected flag on the service objects in observable collection/>
        /// </summary>
        /// <param name="allServices">The collection of services to set the Selected flag on</param>
        /// <param name="selectedServices">The services to mark in the other collection</param>
        public static void SetSelectedFlags(ref ObservableCollection<Service> allServices, ref ObservableCollection<Service> selectedServices)
        {
        }

        #region TestCode

        //TODO: Remove when completing dev
        /// <summary>
        /// Return a list of sample the services for design use.
        /// </summary>
        /// <returns>List of 10 sample services</returns>
        public static ObservableCollection<Service> SampleServices()
        {
            ObservableCollection<Service> result = new ObservableCollection<Service>();

            Service item1;
            for (var i = 1; i < 10; i++)
            {
                item1 = new Service
                {
                    Name = "Service Name " + i,
                    Description = "Description " + i,
                    MachineName = "MachineName " + i
                };
                result.Add(item1);
            }

            return result;
        }

        #endregion TestCode

        #region Service Controls

        /// <summary>
        /// Starts this service.
        /// </summary>
        /// <returns>The current status</returns>
        public ServiceStatus Start()
        {
            ServiceStatus currentStatus = GetStatus();

            //Return if already in correct status
            if (currentStatus == ServiceStatus.Running)
                return currentStatus;

            using (ServiceController sc = new ServiceController(MachineName))
            {
                try
                {
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
                    return ex.InnerException.Message == "Access is denied"
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
                return Status = ServiceStatusConverter.FromServiceControllerStatus(sc.Status);
            }
        }

        /// <summary>
        /// Stops this service.
        /// </summary>
        /// <returns>The current status</returns>
        public ServiceStatus Stop()
        {
            ServiceStatus currentStatus = GetStatus();

            //Check if it has already been stopped
            if (currentStatus == ServiceStatus.Stopped)
                return currentStatus;

            using (ServiceController sc = new ServiceController(MachineName))
            {
                try
                {
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
                    return ex.InnerException.Message == "Access is denied"
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
                return Status = ServiceStatusConverter.FromServiceControllerStatus(sc.Status);
            }
        }

        /// <summary>
        /// Toggle the Service
        /// </summary>
        /// <returns>The current status</returns>
        /// <remarks>If the service is currently pending other actions, no action will be taken.</remarks>
        public ServiceStatus Toggle()
        {
            ServiceStatus[] CanToggle = {
                ServiceStatus.Running,
                ServiceStatus.Stopped,
                ServiceStatus.Paused
            };

            ServiceStatus[] PendingAction = {
                ServiceStatus.StartPending,
                ServiceStatus.StopPending,
                ServiceStatus.ContinuePending,
                ServiceStatus.PausePending
            };

            ServiceStatus currentStatus = GetStatus();
            MessageBox.Show($"Current Status:{currentStatus.ToString()}");

            if (CanToggle.Contains(currentStatus))
            {
                switch (currentStatus)
                {
                    case ServiceStatus.Running:
                        Stop();
                        break;

                    case ServiceStatus.Paused:
                    case ServiceStatus.Stopped:
                        Start();
                        break;
                }

                //Start/Stop operation will have updated the Service Status
                return Status;
            }
            else if (PendingAction.Contains(currentStatus))
            {
                MessageBox.Show($"This service already had pending action!\nCurrent Status: {currentStatus}");
                return currentStatus;
            }
            else
            {
                MessageBox.Show($"Cannot toggle this service!\nCurrent Status: {currentStatus}");
                return currentStatus;
            }
        }

        #endregion Service Controls
    }
}
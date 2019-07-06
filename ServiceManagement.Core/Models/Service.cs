using Newtonsoft.Json;
using System.Collections.ObjectModel;

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
        /// Set the Selected flag on the service objects in observable collection/>
        /// </summary>
        /// <param name="allServices">The collection of services to set the Selected flag on</param>
        /// <param name="selectedServices">The services to mark in the other collection</param>
        public static void SetSelectedFlags(ref ObservableCollection<Service> allServices, ref ObservableCollection<Service> selectedServices)
        {
            //Holding comment
        }

        /*

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

    */
    }
}
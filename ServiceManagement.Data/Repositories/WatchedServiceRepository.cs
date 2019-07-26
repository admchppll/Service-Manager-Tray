using Newtonsoft.Json;
using ServiceManagement.Core.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;

namespace ServiceManagement.Data.Repositories
{
    public class WatchedServiceRepository : IWatchedServiceRepository
    {
        /// <summary>
        /// Gets the default configuration file location.
        /// </summary>
        /// <returns>The absolute path of the configuration file in the system</returns>
        private string ConfigFileLocation()
        {
            return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "ServiceManager", "config.json").ToString();
        }

        /// <summary>
        /// Retrieves an <see cref="IEnumerable{T}"/> of watched Services.
        /// </summary>
        /// <returns>A list of services. Or an empty list if no file exists.</returns>
        public IEnumerable<Service> GetWatchedServices()
        {
            return DeserializeServicesFromFile(ConfigFileLocation());
        }

        /// <summary>
        /// De-serialize Services from a file to an <see cref="IEnumerable{T}"/>.
        /// </summary>
        /// <param name="servicesFile">The services configuration to import from file.</param>
        /// <returns>A list of services. Or an empty list if no file exists.</returns>
        private IEnumerable<Service> DeserializeServicesFromFile(string servicesFile)
        {
            IEnumerable<Service> result = new List<Service>();

            if (!File.Exists(servicesFile))
                return result;

            //De-serialize the configuration
            using (StreamReader file = File.OpenText(servicesFile))
            {
                JsonSerializer serializer = new JsonSerializer();
                result = (ObservableCollection<Service>)serializer.Deserialize(file, typeof(ObservableCollection<Service>));
            }

            return result;
        }

        /// <summary>
        /// Save list of watched services
        /// </summary>
        /// <param name="services">The services.</param>
        /// <returns>True, if the save is successful. Otherwise, false.</returns>
        public bool SaveWatchedServices(IEnumerable<Service> services)
        {
            return SerializeServicesToFile(services, ConfigFileLocation());
        }

        /// <summary>
        /// Serializes list of services to file at given location.
        /// </summary>
        /// <param name="services">The services.</param>
        /// <param name="writeLocation">The file path to serialize the data to</param>
        /// <returns>True, if the serialization is successful. Otherwise, false.</returns>
        private bool SerializeServicesToFile(IEnumerable<Service> services, string writeLocation)
        {
            using (StreamWriter file = File.CreateText(writeLocation))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(file, services);
            }

            return true;
        }
    }
}
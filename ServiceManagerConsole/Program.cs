using System;
using System.IO;

namespace ServiceManagerConsole
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            string config = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "ServiceManager", "config.json");

            /*
            //Loop operational periods and execute
            List<Service> Services = new List<Service>();

            //service 1
            Service service1 = new Service();
            service1.Name = "lfsvc";
            OperationalPeriod operationalPeriod1 = new OperationalPeriod();
            operationalPeriod1.Status = ServiceControllerStatus.Stopped;
            operationalPeriod1.Start = new LocalTime(23, 0);
            operationalPeriod1.End = new LocalTime(23, 1);

            OperationalPeriod operationalPeriod2 = new OperationalPeriod();
            operationalPeriod2.Status = ServiceControllerStatus.Running;
            operationalPeriod2.Start = new LocalTime(23, 1);
            operationalPeriod2.End = new LocalTime(23, 3);

            List<OperationalPeriod> Ops = new List<OperationalPeriod>();

            Ops.Add(operationalPeriod1);
            Ops.Add(operationalPeriod2);

            service1.OperationalPeriods = Ops;
            Services.Add(service1);*/

            /*ServiceController[] oc = ServiceController.GetServices();

            foreach (var ser in ServiceController.GetServices()) {
                try
                {
                    Console.WriteLine(ser.ServiceName + " - " + ser.DisplayName);
                }
                catch (System.ComponentModel.Win32Exception) { }
                //catch (System.InvalidOperationException) { }
            }*/

            //deserialize to file
            /*using (StreamWriter file = File.CreateText(config))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(file, Services);
            }*/

            //List<Service> serialized;

            //serialize from file
            /*using (StreamReader file = File.OpenText(config))
            {
                JsonSerializer serializer = new JsonSerializer();
                serialized = (List<Service>)serializer.Deserialize(file, typeof(List<Service>));
            }*/

            /*int count = 0;
            foreach (Service service in Services)
            {
                Console.WriteLine(service.Name);
                Console.WriteLine(service.GetStatus());
                foreach (OperationalPeriod op in service.OperationalPeriods){
                    Console.WriteLine($"{++count} - {op.Status}");
                    op.Execute(service.Name);
                    Console.WriteLine($"{count} - {op.Status}");
                }
            }
            */

            /*ServiceController[] scServices;
            scServices = ServiceController.GetServices();

            // Display the list of services currently running on this computer.

            Console.WriteLine("Services running on the local computer:");
            foreach (ServiceController scTemp in scServices)
            {
                if (scTemp.ServiceName == "lfsvc")
                {
                    // Write the service name and the display name
                    // for each running service.
                    Console.WriteLine();
                    Console.WriteLine("  Service :        {0}", scTemp.ServiceName);
                    Console.WriteLine("    Display name:    {0}", scTemp.DisplayName);

                    // Query WMI for additional information about this service.
                    // Display the start name (LocalSytem, etc) and the service
                    // description.
                    ManagementObject wmiService;
                    wmiService = new ManagementObject("Win32_Service.Name='" + scTemp.ServiceName + "'");
                    wmiService.Get();
                    Console.WriteLine("    Start name:      {0}", wmiService["StartName"]);
                    Console.WriteLine("    Description:     {0}", wmiService["Description"]);

                    Console.WriteLine(scTemp.Status);
                    scTemp.Start();
                    scTemp.WaitForStatus(ServiceControllerStatus.Running);
                    Console.WriteLine(scTemp.Status);
                    scTemp.Stop();
                }
            }*/

            //ObservableCollection<Service> serviceList = _.GetAllServices();
            //Service.UpdateStatus(ref serviceList);
            //Task serialize = Service.SerializeToFile(serviceList);
            //Console.WriteLine(serialize.Status);
            //serialize.Wait();
            Console.WriteLine("Finished Serializing");
            Console.ReadKey();
        }
    }
}
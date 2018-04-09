using NodaTime;
using System;
using System.ServiceProcess;

namespace ServiceClassLibrary
{
    public class OperationalPeriod
    {
        public ServiceControllerStatus DesiredStatus;
        public LocalTime Start;
        public LocalTime End;
        public LocalDate RuleStart;
        public LocalDate RuleEnd;
        public IsoDayOfWeek[] Days;

        public ServiceControllerStatus Execute(string serviceName) {
            using (ServiceController sc = new ServiceController(serviceName)) {
                if(sc.Status == this.DesiredStatus)
                    return sc.Status;

                switch (this.DesiredStatus)
                {
                    case ServiceControllerStatus.Running:
                        if(sc.Status == ServiceControllerStatus.Paused)
                        {
                            sc.Continue();
                        }
                        else
                        {
                            sc.Start();
                        }
                        sc.WaitForStatus(ServiceControllerStatus.Running, new TimeSpan(0, 0, 30));
                        break;
                    case ServiceControllerStatus.Paused:
                        if(sc.Status == ServiceControllerStatus.Running
                            && sc.CanPauseAndContinue) 
                        {
                            sc.Pause();
                            sc.WaitForStatus(ServiceControllerStatus.Paused, new TimeSpan(0, 0, 30));
                        }
                        break;
                    case ServiceControllerStatus.Stopped:
                        if(sc.Status == ServiceControllerStatus.Running 
                            && sc.CanStop){
                            sc.Stop();
                            sc.WaitForStatus(ServiceControllerStatus.Stopped, new TimeSpan(0, 0, 30));
                        }
                        break;
                    default:
                    break;
                }
                return sc.Status;
            }
        }
    }
}

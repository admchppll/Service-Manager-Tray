namespace ServiceClassLibrary
{
    /// <summary>
    /// Indicates the current state of the service.
    /// </summary>
    /// <remarks>This extends <see cref="System.ServiceProcess.ServiceControllerStatus"/></remarks>
    public enum ServiceStatus
    {
        /// <summary>
        /// The service status is currently unknown.
        /// </summary>
        Unknown = 0,

        /// <summary>
        /// The service is not running. This corresponds to the Win32 SERVICE_STOPPED constant, which is defined as 0x00000001.
        /// </summary>
        Stopped = 1,

        /// <summary>
        /// The service is starting. This corresponds to the Win32 SERVICE_START_PENDING constant, which is defined as 0x00000002.
        /// </summary>
        StartPending = 2,

        /// <summary>
        /// The service is stopping. This corresponds to the Win32 SERVICE_STOP_PENDING constant, which is defined as 0x00000003.
        /// </summary>
        StopPending = 3,

        /// <summary>
        /// The service is running. This corresponds to the Win32 SERVICE_RUNNING constant, which is defined as 0x00000004.
        /// </summary>
        Running = 4,

        /// <summary>
        /// The service continue is pending. This corresponds to the Win32 SERVICE_CONTINUE_PENDING constant, which is defined as 0x00000005.
        /// </summary>
        ContinuePending = 5,

        /// <summary>
        /// The service pause is pending. This corresponds to the Win32 SERVICE_PAUSE_PENDING constant, which is defined as 0x00000006.
        /// </summary>
        PausePending = 6,

        /// <summary>
        /// The service is paused. This corresponds to the Win32 SERVICE_PAUSED constant, which is defined as 0x00000007.
        /// </summary>
        Paused = 7,

        /// <summary>
        /// The service does not exist on the machine
        /// </summary>
        NotExists = 8,

        /// <summary>
        /// The program does not have permission to access status information
        /// </summary>
        NoAccess = 9,

        /// <summary>
        /// The service cannot be stopped
        /// </summary>
        NotStop = 10
    }
}
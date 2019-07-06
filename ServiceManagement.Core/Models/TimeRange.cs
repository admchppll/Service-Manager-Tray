using NodaTime;

namespace ServiceManagement.Core.Models
{
    internal class TimeRange
    {
        public LocalTime Start;
        public LocalTime End;

        /// <summary>
        /// Initializes a new instance of the <see cref="TimeRange"/> class.
        /// </summary>
        public TimeRange()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TimeRange"/> class.
        /// </summary>
        /// <param name="start">The start.</param>
        /// <param name="end">The end.</param>
        public TimeRange(LocalTime start, LocalTime end)
        {
            Start = start;
            End = end;
        }
    }
}
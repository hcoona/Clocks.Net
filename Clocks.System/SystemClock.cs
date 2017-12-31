using System;

namespace Clocks
{
    public class SystemClock : IPhysicalClock<DateTime>
    {
        // The Ticks property expresses date and time values in units of one ten-millionth of a second
        public bool IsHighResolution => false;

        // Using repeated calls to the DateTime.Now property to measure elapsed time is dependent on the system clock.
        public bool IsMonotonic => throw new NotImplementedException();

        public DateTime Now => DateTime.Now;

        public DateTime UtcNow => DateTime.UtcNow;

        public DateTime ParseTimePoint(DateTime timepoint)
        {
            return timepoint;
        }
    }
}

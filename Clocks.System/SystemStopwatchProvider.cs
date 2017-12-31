using System;
using System.Diagnostics;

namespace Clocks
{
    /// <summary>
    /// System built-in stopwatch provider implemented by <seealso cref="Stopwatch"/>
    /// </summary>
    /// <seealso cref="Clocks.IStopwatchProvider{System.Int64}" />
    public class SystemStopwatchProvider : IStopwatchProvider<long>
    {
        public bool IsHighResolution => Stopwatch.IsHighResolution;

        public IStopwatch Create()
        {
            return new SystemStopwatch(new Stopwatch());
        }

        public long GetTimestamp()
        {
            return Stopwatch.GetTimestamp();
        }

        public TimeSpan ParseDuration(long from, long to)
        {
            return TimeSpan.FromSeconds((to - from) / (double)Stopwatch.Frequency);
        }

        public IStopwatch StartNew()
        {
            return new SystemStopwatch(Stopwatch.StartNew());
        }
    }
}

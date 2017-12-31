using System;

namespace Clocks
{
    public interface IStopwatchProvider
    {
        bool IsHighResolution { get; }

        IStopwatch Create();

        IStopwatch StartNew();
    }

    public interface IStopwatchProvider<T> : IStopwatchProvider
    {
        T GetTimestamp();

        TimeSpan ParseDuration(T from, T to);
    }
}

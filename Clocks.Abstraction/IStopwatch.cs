using System;

namespace Clocks
{
    public interface IStopwatch
    {
        bool IsRunning { get; }

        TimeSpan Elapsed { get; }

        void Reset();

        void Start();

        void Stop();

        void Restart();
    }
}

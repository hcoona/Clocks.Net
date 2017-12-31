using System;

namespace Clocks
{
    public interface IPhysicalClock<T> : ILogicalClock<T>
        where T : IComparable, IComparable<T>, IEquatable<T>
    {
        bool IsHighResolution { get; }

        bool IsMonotonic { get; }

        DateTime ParseTimePoint(T timepoint);
    }
}

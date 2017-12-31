using System;

namespace Clocks
{
    public interface ILogicalClock<T>
        where T: IComparable, IComparable<T>, IEquatable<T>
    {
        T Now { get; }
    }
}

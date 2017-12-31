using System;

namespace Clocks
{
    /// <summary>
    /// Basic interface for a logical clock
    /// <para>Typical logical clocks are <strong>lamport scalar clock</strong> &amp; <strong>vector clock</strong></para>
    /// </summary>
    /// <typeparam name="T">The concrate type of time point</typeparam>
    public interface ILogicalClock<T>
        where T: IComparable, IComparable<T>, IEquatable<T>
    {
        /// <summary>
        /// Get current time point.
        /// </summary>
        /// <value>
        /// The current time point.
        /// </value>
        T Now { get; }
    }
}

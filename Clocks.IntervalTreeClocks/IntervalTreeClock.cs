using System;
using System.Threading;
using Itc4net;

namespace Clocks
{
    public class IntervalTreeClock : ILogicalClock<Stamp>
    {
        protected Stamp stamp;

        public IntervalTreeClock() : this(new Stamp()) { }

        public IntervalTreeClock(Stamp intialStamp)
        {
            stamp = intialStamp;
        }

        public Stamp Now => stamp.Peek();

        [Obsolete("Use ILogicalClock<long>.Increment instead")]
        public Stamp Increment()
        {
            while (true)
            {
                var originStamp = stamp;
                var newStamp = originStamp.Event();
                if (originStamp == Interlocked.CompareExchange(ref stamp, newStamp, originStamp))
                {
                    return originStamp;
                }
            }
        }

        public Tuple<IntervalTreeClock, IntervalTreeClock> Fork()
        {
            stamp.Fork(out var s1, out var s2);
            return Tuple.Create(new IntervalTreeClock(s1), new IntervalTreeClock(s2));
        }

        public Tuple<IntervalTreeClock, IntervalTreeClock, IntervalTreeClock> Fork3()
        {
            stamp.Fork(out var s1, out var s2, out var s3);
            return Tuple.Create(new IntervalTreeClock(s1), new IntervalTreeClock(s2), new IntervalTreeClock(s3));
        }

        public Tuple<IntervalTreeClock, IntervalTreeClock, IntervalTreeClock, IntervalTreeClock> Fork4()
        {
            stamp.Fork(out var s1, out var s2, out var s3, out var s4);
            return Tuple.Create(
                new IntervalTreeClock(s1),
                new IntervalTreeClock(s2),
                new IntervalTreeClock(s3),
                new IntervalTreeClock(s4));
        }
        
        public Stamp Join(IntervalTreeClock other)
        {
            while (true)
            {
                var originStamp = stamp;
                var newStamp = originStamp.Join(other.stamp);
                if (originStamp == Interlocked.CompareExchange(ref stamp, newStamp, originStamp))
                {
                    return newStamp;
                }
            }
        }

        public Stamp Join(Stamp other)
        {
            while (true)
            {
                var originStamp = stamp;
                var newStamp = originStamp.Join(other);
                if (originStamp == Interlocked.CompareExchange(ref stamp, newStamp, originStamp))
                {
                    return newStamp;
                }
            }
        }

        void ILogicalClock<Stamp>.Increment()
        {
#pragma warning disable CS0618 // 类型或成员已过时
            this.Increment();
#pragma warning restore CS0618 // 类型或成员已过时
        }

        /// <summary>
        /// Adjust internal counter because know about other logical time. It use <code>Receive</code> method of <seealso cref="Stamp"/>.
        /// </summary>
        /// <param name="other"></param>
        void ILogicalClock<Stamp>.Witness(Stamp other)
        {
            while (true)
            {
                var originStamp = stamp;
                var newStamp = originStamp.Receive(other);
                if (originStamp == Interlocked.CompareExchange(ref stamp, newStamp, originStamp))
                {
                    break;
                }
            }
        }
    }
}

using System;
using System.Threading;
using Itc4net;

namespace Clocks
{
    public class IntervalTreeClock : ILogicalClock<Stamp>
    {
        internal Stamp stamp;

        public IntervalTreeClock(Stamp intialStamp)
        {
            stamp = intialStamp;
        }

        public Stamp Now => stamp.Peek();

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
    }
}

using System.Threading;

namespace Clocks
{
    public class LamportClock : ILogicalClock<long>
    {
        private long counter;

        public LamportClock() : this(0) { }

        public LamportClock(long initial)
        {
            counter = initial;
        }

        public long Now => Interlocked.Read(ref counter);

        public long Increment()
        {
            return Interlocked.Increment(ref counter);
        }

        public long Witness(long timepoint)
        {
            while (true)
            {
                var current = Interlocked.Read(ref counter);
                if (timepoint < current)
                {
                    return current;
                }
                else
                {
                    var next = timepoint + 1;
                    if (current == Interlocked.CompareExchange(ref counter, current, next))
                    {
                        return next;
                    }
                }
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AmmoFinder.RateLimiter
{
    public static class Delayer
    {
        public static async Task LimitDelay(List<DateTimeOffset> callLog, int limitCount, TimeSpan limitTime)
        {
            var now = DateTimeOffset.UtcNow;

            lock (callLog)
            {
                callLog.Add(now);

                while (callLog.Count > limitCount)
                    callLog.RemoveAt(0);
            }

            if (callLog.Count < limitCount)
                return;

            var limit = now.Add(-limitTime);

            var lastCall = DateTimeOffset.MinValue;
            var shouldLock = false;

            lock (callLog)
            {
                lastCall = callLog.FirstOrDefault();
                shouldLock = callLog.Count(x => x >= limit) >= limitCount;
            }

            var delayTime = shouldLock && (lastCall > DateTimeOffset.MinValue)
                ? (lastCall - limit)
                : TimeSpan.Zero;

            if (delayTime > TimeSpan.Zero)
                await Task.Delay(delayTime);
        }
    }
}

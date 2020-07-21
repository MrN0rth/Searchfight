using System;

namespace Searchfight.WebSearchers
{
    public class RetryProvider
    {
        private readonly int _maxRunCount;

        public RetryProvider(int maxRunCount = 3)
        {
            _maxRunCount = maxRunCount;
        }

        public T RunWithRetry<T>(Func<T> @delegate)
        {
            int runCount = 0;
            Exception lastException = null;
            while (runCount < _maxRunCount)
            {
                try
                {
                    return @delegate();
                }
                catch (Exception e)
                {
                    lastException = e;
                    runCount++;
                }
            }

            throw new RetryException("Max number of retries exceeded, for more details see InnerException", lastException);
        }
    }
}
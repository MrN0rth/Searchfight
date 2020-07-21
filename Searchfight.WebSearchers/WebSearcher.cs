using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Searchfight.WebSearchers
{
    public abstract class WebSearcher
    {
        private readonly int _maxConcurrency;
        private readonly int _delayBetweenRequests;

        protected WebSearcher(int maxConcurrency, int delayBetweenRequests = 0)
        {
            _maxConcurrency = maxConcurrency;
            _delayBetweenRequests = delayBetweenRequests;
        }

        protected async Task<Dictionary<T, TRes>> RunConcurrently<T, TRes>(IEnumerable<T> searchedThemes, Func<T, Task<TRes>> funcToRun)
        {
            var results = new ConcurrentDictionary<T, TRes>();
            var queue = new ConcurrentQueue<T>(searchedThemes);
            var tasks = new List<Task>();
            for (int n = 0; n < _maxConcurrency; n++)
            {
                tasks.Add(Task.Run(async () =>
                {
                    while (queue.TryDequeue(out T theme))
                    {
                        TRes r = await funcToRun(theme);
                        results.TryAdd(theme, r);

                        if (_delayBetweenRequests > 0)
                        {
                            await Task.Delay(_delayBetweenRequests);
                        }
                    }
                }));
            }

            await Task.WhenAll(tasks);

            return results.ToDictionary(k => k.Key, v => v.Value);
        }
    }
}
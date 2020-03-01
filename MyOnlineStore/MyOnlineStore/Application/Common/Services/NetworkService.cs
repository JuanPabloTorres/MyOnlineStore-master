using MyOnlineStore.Application.Common.Interfaces.Services;
using Polly;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MyOnlineStore.Application.Common.Services
{
    public class NetworkService : INetworkService
    {
        public async Task<T> Retry<T>(Func<Task<T>> func, int retryCount, Func<Exception, int, Task> onRetry)
        {
            return await Policy
                            .Handle<Exception>()
                            .RetryAsync(retryCount, onRetry)
                            .ExecuteAsync(func);
        }

        public async Task<T> WaitAndRetry<T>(Func<Task<T>> func, Func<int, TimeSpan> sleepDurationProvider, int retryCount, Func<Exception, TimeSpan, Task> onRetryAsync)
        {
            return await Policy
                            .Handle<Exception>()
                            .WaitAndRetryAsync(retryCount, sleepDurationProvider, onRetryAsync)
                            .ExecuteAsync(func);
        }

        
    }
}

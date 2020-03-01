using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MyOnlineStore.Application.Common.Interfaces.Services
{
    public interface INetworkService
    {
        Task<T> Retry<T>(Func<Task<T>> func, int retryCount, Func<Exception, int, Task> onRetry);
        Task<T> WaitAndRetry<T>(Func<Task<T>> func, Func<int, TimeSpan> sleepDurationProvider, int retryCount,
                                Func<Exception, TimeSpan, Task> onRetryAsync);
    }
}

using MyOnlineStore.Shared.Models.Entries;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MyOnlineStore.Application.Common.Interfaces.DataStore
{
   public interface INotificationDataStore
    {

        public Task<IEnumerable<JobRequest>> GetJobRequest(Guid UserId);

    }
}

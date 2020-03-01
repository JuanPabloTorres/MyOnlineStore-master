using MyOnlineStore.Shared.Models.Entries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyOnlineStore.MobileAppService.Interfaces.Repositories
{
    public interface INotificationRepository:IRepository<JobRequest>
    {
        public IEnumerable<JobRequest> GetJobRequest(Guid UserId);

    }
}

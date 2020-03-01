using Microsoft.EntityFrameworkCore;
using MyOnlineStore.MobileAppService.Context;
using MyOnlineStore.MobileAppService.Interfaces.Repositories;
using MyOnlineStore.Shared.Models.Entries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyOnlineStore.MobileAppService.Repositories
{
    public class NotificationRepository : Repository<JobRequest>, INotificationRepository
    {
        public NotificationRepository(MyContext context) : base(context)
        {

        }

        public IEnumerable<JobRequest> GetJobRequest(Guid UserId)
        {

            
            var data = _Context.JobRequest
                .Where(r => r.UserReciverId == UserId);

            return data;
        }
    }
}

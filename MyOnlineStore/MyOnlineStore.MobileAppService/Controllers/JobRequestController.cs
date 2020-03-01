using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyOnlineStore.MobileAppService.Interfaces.Repositories;
using MyOnlineStore.Shared.Models.Entries;

namespace MyOnlineStore.MobileAppService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JobRequestController : ControllerBase
    {

        INotificationRepository NotificationRepository;
        public JobRequestController(INotificationRepository repository)
        {
            NotificationRepository = repository;
        }
        [HttpGet("[action]/{userid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IEnumerable<JobRequest> GetJobRequest(Guid userid)
        {
            var data = NotificationRepository.GetJobRequest(userid);

            
          

            return data;
        }

    }
}
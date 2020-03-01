using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyOnlineStore.Entities.Models.Stores;
using MyOnlineStore.MobileAppService.Interfaces.Repositories;

namespace MyOnlineStore.MobileAppService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StoresEmployeesController : ControllerBase
    {

        IStoresRepository storesRepository;

        public StoresEmployeesController(IStoresRepository storesRepository)
        {
            this.storesRepository = storesRepository;
        }

        [HttpGet("[action]/{storeId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IEnumerable<StoresEmployees> GetStoreEmployees(string storeId)
        {
            var data = storesRepository.GetStoreEmployees(storeId);

            return data;
        }

        [HttpGet("[action]/{employeeId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IEnumerable<StoresEmployees> GetStoresWorkSpaceFromEmployee(string employeeId)
        {
            var data = storesRepository.GetStoresWorkSpaceFromEmployee(employeeId);

            return data;
        }
    }
}
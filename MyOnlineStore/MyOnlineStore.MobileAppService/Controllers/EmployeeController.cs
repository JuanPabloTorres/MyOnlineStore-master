using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyOnlineStore.Entities.Models.Users;
using MyOnlineStore.MobileAppService.Interfaces.Repositories;

namespace MyOnlineStore.MobileAppService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {


        IEmployeeRepository employeeRepository;
        public EmployeeController(IEmployeeRepository employeeRepository)
        {
           this.employeeRepository = employeeRepository;
        }

       
       

        [HttpGet("[action]/{filter}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IEnumerable<User> GetUser(string filter)
        {

            List<User> data = new List<User>()
            {
                new User()
                {
                     FullName="Jose Torres",
                      Email="est.juanpablotorres@gmail.com",
                       PhoneNumber="9392403506"
                }
            };

          
                var datauser = data.Where(u => u.Email == filter || u.PhoneNumber == filter).ToList();

                return datauser;
           

           
           // var users = this.employeeRepository.GetUsers(filter);

            //return users;
        }
    }
}
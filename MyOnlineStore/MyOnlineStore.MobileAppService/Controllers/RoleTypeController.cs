using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using MyOnlineStore.MobileAppService.Interfaces.Repositories;
using MyOnlineStore.Entities.Models.Roles;

namespace MyOnlineStore.MobileAppService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleTypeController : ControllerBase
    {
        public readonly IRoleRefRepository _RoleRefRepo;
        public RoleTypeController(IRoleRefRepository roleRefRepository)
        {
            _RoleRefRepo = roleRefRepository;
        }
        // GET: api/RoleReference
        [HttpGet]
        public IEnumerable<RoleType> Get()
        {
            var list = _RoleRefRepo.GetAll().AsEnumerable();
            return list;
        }

        // GET: api/RoleReference/5
        [HttpGet("{roleName}", Name = "GetByRoleName")]
        public RoleType GetByRoleName(string roleName)
        {
            var role = _RoleRefRepo.GetByRoleName(roleName);
            return role;
        }

        // POST: api/RoleReference
        [HttpPost]
        public void Post([FromBody] string value)
        {

        }

        // PUT: api/RoleReference/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}

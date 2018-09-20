using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using wmg.BusinessLayer.IManager;
using wmg.Common.Resources;
using wmg.Common.Resources.User;

namespace wmg.API.Controllers
{
    [Produces("application/json")]
    [Route("api/roles")]
    public class RoleController : Controller
    {

        //all domaine in the same Interface
        private readonly IRoleManager _roleManager;

        public RoleController(IRoleManager roleManager)
        {
            _roleManager = roleManager;
        }


        [HttpGet]
        public async Task<IActionResult> GetRoles(FilterResource filtesQuery)
        {
            var roles = await _roleManager.GetAll(filtesQuery);
            if (roles == null)
                return NotFound();


            return Ok(roles);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetRolesById(int id)
        {
            var role = await _roleManager.GetItemById(id);
            if (role == null)
                return NotFound();

            return Ok(role);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRole(int id)
        {
            await _roleManager.Delete(id);

            return Ok(id);
        }


        [HttpPost]
        public async Task<IActionResult> CreateRole([FromBody] RoleResource roleResource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _roleManager.Add(roleResource);
            return Ok(result);
        }
    }
}

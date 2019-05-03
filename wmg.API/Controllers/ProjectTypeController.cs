using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using wmg.BusinessLayer.IManager;
using wmg.Common.Resources;
using wmg.Common.Resources.Project;
using wmg.Common.Resources.User;

namespace wmg.API.Controllers
{
    [Produces("application/json")]
    [Route("api/projectType")]
    public class ProjectTypeController : Controller
    {
        private readonly IProjectManager _projectManager;

        public ProjectTypeController(IProjectManager projectManager)
        {
            _projectManager = projectManager;
        }
        [HttpGet]
        public async Task<IActionResult> GetProjectTypes(ProjectTypeQueryResource filtesQuery)
        {
            var projectTypes = await _projectManager.GetAll(filtesQuery);
            if (projectTypes == null)
                return NotFound();


            return Ok(projectTypes);
        }
        [AllowAnonymous]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody]ProjectTypeResource projectTypeResource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(await _projectManager.Update(id, projectTypeResource));

        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProjectTypesById(int id)
        {
            var projectType = await _projectManager.GetItemById(id);
            if (projectType == null)
                return NotFound();

            return Ok(projectType);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProjectType(int id)
        {
            await _projectManager.Delete(id);

            return Ok(id);
        }


        [HttpPost]
        public async Task<IActionResult> CreateProjectType([FromBody] ProjectTypeResource ProjectTypeResource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _projectManager.Add(ProjectTypeResource);
            return Ok(result);
        }


    }
}

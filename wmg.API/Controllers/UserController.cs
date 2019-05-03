using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using wmg.BusinessLayer.IManager;
using wmg.Common.Entites;
using wmg.Common.Resources.User;

namespace wmg.API.Controllers
{
    [Produces("application/json")]
    [Route("api/users")]
    public class UserController : Controller
    {
        private readonly IUserManager _userManager;
        private readonly SignInManager<User> _signInManager;

        public UserController( SignInManager<User> signInManager, IUserManager userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        [HttpGet]

        public async Task<IActionResult> GetUser(UserQueryResource filterResource)
        {
            var queryResult = await _userManager.GetAll(filterResource);

            if (queryResult == null)
                return NotFound();

            return Ok(queryResult);
        }

        [HttpGet("{id}")]

        public async Task<IActionResult> GetUserById(int id)
        {
            var userProfileResource = await _userManager.GetItemById(id);

            if (userProfileResource == null)
                return NotFound();

            return Ok(userProfileResource);
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Register([FromBody]UserSaveResource saveUserResource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(await _userManager.Add(saveUserResource));

        }
        [AllowAnonymous]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody]UpdateUserResource updateUserResource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(await _userManager.Update(id, updateUserResource));

        }
        [HttpPost("authenticate")]
        [AllowAnonymous]
        public async Task<IActionResult> Authenticate([FromBody]AuthResource userAuthResource)
        {
            // Clear the existing external cookie to ensure a clean login process
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _signInManager.PasswordSignInAsync(userAuthResource.Email, userAuthResource.Password, userAuthResource.RememberMe, lockoutOnFailure: false);
           
            if (!result.Succeeded) return BadRequest("cannot login");
            return Ok(await _userManager.GetIUserByEmail(userAuthResource.Email));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            return Ok(  _userManager.Delete(id));
        }
    }
}

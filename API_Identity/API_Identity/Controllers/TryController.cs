using API_Identity.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace API_Identity.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TryController : ControllerBase
    {
        private readonly UserManager<Student> userManager;

        public TryController(UserManager<Student> userManager)
        {
            this.userManager = userManager;
        }
        [HttpGet]
        [Route("GetUserInfo")]
        [Authorize]
        public async Task<ActionResult> GetUserInfo()
        {
            var user = await userManager.GetUserAsync(User);  /// assume that id is in nameidentifier , based on token it will return the folloeing info
            return Ok(new
            {
                userName = user?.UserName,
                Email =user?.Email,
                Classlevel = user?.ClassLevel
            }); 
        }

        [HttpGet]
        [Route("GetinfoFormanager")]
        [Authorize(Policy ="adminsonly")]
        public ActionResult<string> GetinfoFormanager()
        {
            var name = "this end point can only access ny admins";
            return name;
        }



        [HttpGet]
        [Route("GetinfoForUser")]
        [Authorize (Policy ="AdminsAndUsers")]
        public ActionResult<string> GetinfoForUser()
        {
            var name = "this end point can only access ny admins or Users ";
            return name;
        }
    }
}

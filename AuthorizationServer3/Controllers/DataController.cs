using System;
using System.Linq;
using System.Security.Claims;
using System.Web.Http;

namespace AuthorizationServer3.Controllers
{
    public class DataController : ApiController
    {
        [AllowAnonymous]
        [HttpGet]
        [Route("api/data/forall")]
        public IHttpActionResult Get()
        {
            return Ok("__"+DateTime.Now.ToString());
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("api/data/authenticat")]
        public IHttpActionResult GetUser(string id)
        {
            return Ok(DateTime.Now.ToString()+"fg");
        }

        [Authorize]
        [HttpGet]
        [Route("api/data/authenticate")]
        public IHttpActionResult GetForAuthenticate()
        {
            var identity = (ClaimsIdentity)User.Identity;
            return Ok(identity.Name);
        }

        [Authorize]
        [HttpGet]
        [Route("api/data/authorize")]
        public IHttpActionResult GetForAdmin()
        {
            var identity = (ClaimsIdentity)User.Identity;
            var roles = identity.Claims.Where(c => c.Type == ClaimTypes.Role).Select(c => c.Value);
            return Ok("Name " + identity.Name + " Role" + string.Join(",", roles.ToList()));
        }
    }
}
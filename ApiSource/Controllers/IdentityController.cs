using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiSource.Controllers
{
    [ApiController]
    public class IdentityController : ControllerBase
    {
        [HttpGet]
        [Route("api/identity")]
        [PermissionAuthorize(Roles = "admin", Project = "测试项目")]
        public object GetUserClaims()
        {
            return User.Claims.Select(r => new { r.Type, r.Value });
        }
    }
}
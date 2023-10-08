using Extend;
using MemoryBll;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model;

namespace MemoryApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ChangePwdController : ControllerBase
    {
        [HttpPost]
        public Res ChangePwd(UserInfoplus res)
        {
            string token = Request.Headers["Authorization"];
            token = token.Remove(0, 7);
            string username = JwtHelper.GetUsername(token);
            RegisterBLL bLL = new RegisterBLL();
            return bLL.Changepwd(username, res.password, res.newpassword);
        }
    }
}

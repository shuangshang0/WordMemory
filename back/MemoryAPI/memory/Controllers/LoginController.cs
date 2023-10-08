using Extend;
using MemoryBll;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model;
using System.Reflection.Emit;

namespace MemoryApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        //option注入
        private readonly JwtHelper _jwtHelper;

        public LoginController(JwtHelper jwtHelper)
        {
            _jwtHelper = jwtHelper;
        }
        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns>username</returns>
        [HttpPost]
        public Res Login(UserInfo res)
        {
            LoginBLL bLL = new LoginBLL();
            var user = bLL.Login(res.username, res.password);
            if (user != null)
            {return new Res(true,_jwtHelper.CreateToken(user));}
            else
            { return new Res(false,""); }
        }
    }
}

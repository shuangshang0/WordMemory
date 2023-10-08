using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Extend;
using Model;

namespace MemoryApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class TokenTestController : ControllerBase
    {
        //option注入
        private readonly JwtHelper _jwtHelper;

        public TokenTestController(JwtHelper jwtHelper)
        {
            _jwtHelper = jwtHelper;
        }



        [HttpPost]
        public ActionResult<string> GetToken(UserInfo res)
        {
            return _jwtHelper.CreateToken(res.username);
        }

        [Authorize]
        [HttpGet]
        public ActionResult<string> GetTest()
        {

            return "True token";
        }

        [Authorize]
        [HttpGet]
        public string Getusernametest()
        {
            //提取请求头里Authorization的值
            string token = this.Request.Headers["Authorization"];
            //将Authorization中的Bearer 删除留下token
            token = token.Remove(0,7);
            return JwtHelper.GetUsername(token);
        }



    }
    
}

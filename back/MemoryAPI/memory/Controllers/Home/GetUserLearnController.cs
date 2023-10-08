using Extend;
using MemoryBll.Home;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model.Home;

namespace MemoryApi.Controllers.Home
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class GetUserLearnController : ControllerBase
    {
        [HttpGet]
        public List<UserLearn> GetUserLearn()
        {
            string token = Request.Headers["Authorization"];
            token = token.Remove(0, 7);
            string username = JwtHelper.GetUsername(token);
            GetUserLearnBLL bll = new GetUserLearnBLL();
            return bll.GetUserLearn(username);
        }
    }
}

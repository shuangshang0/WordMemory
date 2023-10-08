using Extend;
using MemoryBll.Home;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model;
using Model.Home;

namespace MemoryApi.Controllers.Home.community
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class MsgBoardController : ControllerBase
    {
        [HttpGet]
        public List<CommunityMsg> GetMsg()
        {
            MsgBoardBLL bll = new MsgBoardBLL();
            var res = bll.MsgBoard();
            return res;
        }
        [HttpPost]
        public Res PostMsg(CommunityMsg communityMsg)
        {
            string token = this.Request.Headers["Authorization"];
            token = token.Remove(0, 7);
            string username = JwtHelper.GetUsername(token);
            communityMsg.username= username;
            MsgBoardBLL bll =new MsgBoardBLL();
            return bll.AddMsg(communityMsg);
        }
    }
}

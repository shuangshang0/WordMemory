using Extend;
using MemoryBll.Home;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model;
using Model.Home;

namespace MemoryApi.Controllers.Home
{
    [Authorize]
    [Route("api/[controller]/[Action]")]
    [ApiController]
    public class CalenderController : ControllerBase
    {
        [HttpGet]
        public List<Calender> GetUserLearn()
        {
            string token = Request.Headers["Authorization"];
            token = token.Remove(0, 7);
            string username = JwtHelper.GetUsername(token);
            CalenderBLL bll = new CalenderBLL();
            return bll.GetCalenderByUser(username);
        }
        [HttpPost]
        public Res CalenderByLearnword(Calender res)
        {
            string token = Request.Headers["Authorization"];
            token = token.Remove(0, 7);
            string username = JwtHelper.GetUsername(token);
            CalenderBLL bll = new CalenderBLL();
            return bll.CalenderByLearnword(username, res.learnword, res.date);
        }
        [HttpPost]
        public Res CalenderByReviewword(Calender res)
        {
            string token = Request.Headers["Authorization"];
            token = token.Remove(0, 7);
            string username = JwtHelper.GetUsername(token);
            CalenderBLL bll = new CalenderBLL();
            return bll.CalenderByReviewword(username, res.reviewword, res.date);
        }
    }
}

using Extend;
using MemoryBll.MyWord;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model;
using Model.Home;

namespace MemoryApi.Controllers.MyWord
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class AddMyWordController : ControllerBase
    {
        [HttpPost]
        public Res AddMyWord(MyWordmodel res)
        {
            string token = Request.Headers["Authorization"];
            token = token.Remove(0, 7);
            string username = JwtHelper.GetUsername(token);
            MyWordBLL bll = new MyWordBLL();
            return bll.AddMyWord(username,res.englishWord,res.chineseWord);
        }
    }
}

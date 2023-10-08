using Extend;
using MemoryBll.MyWord;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model.Home;
using Model;

namespace MemoryApi.Controllers.MyWord
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class DeleteMyWordController : ControllerBase
    {
        [HttpPost]
        public Res DeleteMyWord(MyWordmodel res)
        {
            string token = Request.Headers["Authorization"];
            token = token.Remove(0, 7);
            string username = JwtHelper.GetUsername(token);
            MyWordBLL bll = new MyWordBLL();
            return bll.DeleteMyWord(username, res.englishWord, res.chineseWord);
        }
    }
}

using Extend;
using MemoryBll.Recite;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model;
using Model.Recite;

namespace MemoryApi.Controllers.Recite
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class EntryController : ControllerBase
    {
        [HttpPost]
        public Res Entry(UserLearnWord aff)
        {
            string token = this.Request.Headers["Authorization"];
            token = token.Remove(0, 7);
            string username = JwtHelper.GetUsername(token);
            EntryBLL bll = new EntryBLL();
            Res res1 = bll.EntryLearnWord(username, aff.englishword, aff.count, aff.isknow);
            if (res1.IsSuccess == false) { return res1; }
            Res res2 = bll.AddLearnWord1(username);
            return res2;
        }
    }
}

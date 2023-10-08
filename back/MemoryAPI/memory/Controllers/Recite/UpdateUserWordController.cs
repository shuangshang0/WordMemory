using Extend;
using MemoryBll.Recite;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model.Recite;
using Model;

namespace MemoryApi.Controllers.Recite
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UpdateUserWordController : ControllerBase
    {
        [HttpPost]
        public Res UpdateUserWord(UserLearnWord aff)
        {
            string token = this.Request.Headers["Authorization"];
            token = token.Remove(0, 7);
            string username = JwtHelper.GetUsername(token);
            EntryBLL bll = new EntryBLL();
          return bll.UpdateUserWord(username, aff.englishword, aff.count, aff.isknow);
           
        }
    }
}

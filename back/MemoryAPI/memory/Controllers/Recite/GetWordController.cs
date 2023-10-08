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
    public class GetWordController : ControllerBase
    {
        [HttpPost]
        public List<ENword> GetAnyWord(Numplus res)
        {
            string token = this.Request.Headers["Authorization"];
            token = token.Remove(0, 7);
            string username = JwtHelper.GetUsername(token);
            GetWordBLL bLL= new GetWordBLL();
            return bLL.GetAnyWord(username, res.num ,res.lexicon);
        }
    }
}

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
    public class GetReviewWordBy0Controller : ControllerBase
    {
        [HttpPost]
        public List<ENwordplus> GetAnyWord(Num num)
        {
            string token = this.Request.Headers["Authorization"];
            token = token.Remove(0, 7);
            string username = JwtHelper.GetUsername(token);
            GetWordBLL bLL = new GetWordBLL();
            return bLL.GetReviewWordBy0(username, num.num);
        }
    }
}

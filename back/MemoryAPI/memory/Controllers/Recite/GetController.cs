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
    [Route("api/[controller]/[Action]")]
    [ApiController]
    public class GetController : ControllerBase
    {
        [HttpPost]
        public List<ENword> Get3Random(ENword res)
        {
            GetWordBLL bll= new GetWordBLL();
            return bll.Get3Random(res.englishWord);
        }
    }
}

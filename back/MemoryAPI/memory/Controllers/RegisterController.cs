using MemoryBll;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model;

namespace MemoryApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegisterController : ControllerBase
    {
        [HttpPost]
        public Res Register(UserInfo res)
        {
            RegisterBLL bll = new();
            return bll.Register(res.username, res.password,res.phone,res.email);
        }
    }
}

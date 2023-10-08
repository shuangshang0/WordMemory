using MemoryDal.Home;
using Model.Home;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemoryBll.Home
{
    public class GetUserLearnBLL
    {
        public List<UserLearn> GetUserLearn(string username)
        {
            GetUserLearnDAL dal= new GetUserLearnDAL();
            return dal.GetUserLearn(username);
        }
    }
}

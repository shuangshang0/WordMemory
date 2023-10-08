using Extend;
using Model;
using Model.Home;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemoryDal.Home
{
    public class GetUserLearnDAL
    {
        public List<UserLearn> GetUserLearn(string username)
        {
            SqlParameter[] sp = new SqlParameter[]
            {
                new SqlParameter("username",username)
            };
            SqlDataReader result = SQLHelper.SPQueryMultiple("sp_getUserLearnByUser", sp);
            List<UserLearn> list= new List<UserLearn>();
            while(result.Read())
            {
                list.Add(new UserLearn()
                {
                    username = result["username"].ToString(),
                    learnword = (int)result["learnword"],
                    learnday = (int)result["learnday"]
                });
            }
            return list;
        }

    }
}

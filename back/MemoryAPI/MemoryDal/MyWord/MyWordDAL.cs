using Extend;
using Model;
using Model.Home;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemoryDal.MyWord
{
    public class MyWordDAL
    {
        public List<MyWordmodel> GetMyWord(string username)
        {
            SqlParameter[] sp = new SqlParameter[]
           {
                new SqlParameter("username",username)
           };
            SqlDataReader result = SQLHelper.SPQueryMultiple("sp_getMyWord", sp);
            List<MyWordmodel> list = new List<MyWordmodel>();
            while (result.Read())
            {
                list.Add(new MyWordmodel()
                {
                    username = result["username"].ToString(),
                    englishWord = result["englishWord"].ToString(),
                    chineseWord = result["chineseWord"].ToString()
                });
            }
            return list;
        }

        public Res AddMyWord(string username,string englishWord,string chineseWord)
        {

            SqlParameter[] sp = new SqlParameter[]{
                new SqlParameter("username",username),
                new SqlParameter("englishWord",englishWord),
                new SqlParameter("chineseWord",chineseWord),
            };
            int result = SQLHelper.SPInsertDeleteUpdata("sp_addMyWord", sp);
            if (result >= 1)
            {
                return new Res(true, "添加成功");
            }
            else
            {
                return new Res(false, "添加失败");
            }
        }

        public Res DeleteMyWord(string username, string englishWord, string chineseWord)
        {

            SqlParameter[] sp = new SqlParameter[]{
                new SqlParameter("username",username),
                new SqlParameter("englishWord",englishWord),
                new SqlParameter("chineseWord",chineseWord),
            };
            int result = SQLHelper.SPInsertDeleteUpdata("sp_deleteMyWord", sp);
            if (result >= 1)
            {
                return new Res(true, "删除成功");
            }
            else
            {
                return new Res(false, "删除失败");
            }
        }

    }
}

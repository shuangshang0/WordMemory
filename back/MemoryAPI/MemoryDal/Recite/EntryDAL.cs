using Extend;
using Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemoryDal.Recite
{
    public class EntryDAL
    {
        /// <summary>
        ///添加单词到userleranword表中 
        /// </summary>
        /// <param name="username"></param>
        /// <param name="englishword"></param>
        /// <param name="count"></param>
        /// <param name="isknow"></param>
        /// <returns></returns>
        public Res EntryLearnWord(string username,string englishword,int count ,int isknow)
        {
            SqlParameter[] sp = new SqlParameter[]{
                new SqlParameter("username",username),
                new SqlParameter("englishword",englishword),
                new SqlParameter("count",count),
                new SqlParameter("isknow",isknow)
            };
         int result = SQLHelper.SPInsertDeleteUpdata("sp_entryUserWord", sp);
            if (result >= 1) 
            { 
                return new Res(true, "添加成功");
            }
            else
            {
                return new Res(false, "添加失败");
            }
        }

        /// <summary>
        /// 给user的学习单词加1
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public Res AddLearnWord1(string username)
        {
            SqlParameter[] sp = new SqlParameter[]
            {
               new SqlParameter("username",username)
            };
            int Result = SQLHelper.SPInsertDeleteUpdata("sp_learnWordAdd1", sp);
            if (Result > 0)
            {
                return new Res(true, "增加成功");
            }
            else
            {
                return new Res(false, "增加learnword时失败");
            }
        }

        /// <summary>
        /// 修改learnword学习状况
        /// </summary>
        /// <param name="username"></param>
        /// <param name="englishword"></param>
        /// <param name="count"></param>
        /// <param name="isknow"></param>
        /// <returns></returns>
        public Res UpdateUserWord(string username, string englishword, int count, int isknow)
        {
            SqlParameter[] sp = new SqlParameter[]{
                new SqlParameter("username",username),
                new SqlParameter("englishword",englishword),
                new SqlParameter("count",count),
                new SqlParameter("isknow",isknow)
            };
            int result = SQLHelper.SPInsertDeleteUpdata("sp_updateUserWord", sp);
            if (result >= 1)
            {
                return new Res(true, "修改成功");
            }
            else
            {
                return new Res(false, "修改失败");
            }
        }
    }
}

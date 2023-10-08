using Extend;
using Model;
using Model.Recite;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemoryDal.Recite
{
    public class GetWordDAL
    {
        /// <summary>
        /// 从单词库中随机获取num个单词
        /// </summary>
        /// <param name="username">用户名</param>
        /// <param name="num">需要随机获取的单词数</param>
        /// <returns>ENword</returns>
        public List<ENword> GetAnyWord(string username,int num ,string lexicon)
        {
            SqlParameter[] pron = new SqlParameter[]
            {
                new SqlParameter("username",username),
                new SqlParameter("num",num),
                new SqlParameter("lexicon",lexicon)
            };
            List<ENword> res = new List<ENword>();
            SqlDataReader result = SQLHelper.SPQueryMultiple("sp_getNewWord", pron);
            while (result.Read())
            {
                res.Add(new ENword()
                {
                    englishWord = result["englishWord"].ToString(),
                    chineseWord = result["chineseWord"].ToString(),
                    englishInstance1 = result["englishInstance1"].ToString(),
                    chineseInstance1 = result["chineseInstance1"].ToString(),
                    englishInstance2 = result["englishInstance2"].ToString(),
                    chineseInstance2 = result["chineseInstance2"].ToString(),
                    pron = result["pron"].ToString(),
                });                
            }
            return res;
        }

        /// <summary>
        /// 获取复习次数为0的单词
        /// </summary>
        /// <param name="username"></param>
        /// <param name="num"></param>
        /// <returns>ENwordplus</returns>
        public List<ENwordplus> GetReviewWordBy0(string username, int num)
        {
            SqlParameter[] sp = new SqlParameter[]
               {
                new SqlParameter("username",username),
                new SqlParameter("num",num)
               };
            List<ENwordplus> res = new List<ENwordplus>();
            SqlDataReader result = SQLHelper.SPQueryMultiple("sp_getReviewWordBy0", sp);
            while (result.Read())
            {
                res.Add(new ENwordplus()
                {
                    englishWord = result["englishWord"].ToString(),
                    chineseWord = result["chineseWord"].ToString(),
                    englishInstance1 = result["englishInstance1"].ToString(),
                    chineseInstance1 = result["chineseInstance1"].ToString(),
                    englishInstance2 = result["englishInstance2"].ToString(),
                    chineseInstance2 = result["chineseInstance2"].ToString(),
                    pron = result["pron"].ToString(),
                    count= (int)result["count"],
                    isknow = (int)result["isknow"]
                });
            }
            return res;
        }
        public List<ENword> Get3Random(string englishWord)
        {
            SqlParameter[] sp = new SqlParameter[]
            {
                new SqlParameter("englishWord",englishWord)
            };
            List<ENword> res = new List<ENword>();
            SqlDataReader result = SQLHelper.SPQueryMultiple("sp_get3RandomWord", sp);
            while (result.Read())
            {
                res.Add(new ENword()
                {
                    englishWord = result["englishWord"].ToString(),
                    chineseWord = result["chineseWord"].ToString(),
                    englishInstance1 = result["englishInstance1"].ToString(),
                    chineseInstance1 = result["chineseInstance1"].ToString(),
                    englishInstance2 = result["englishInstance2"].ToString(),
                    chineseInstance2 = result["chineseInstance2"].ToString(),
                    pron = result["pron"].ToString(),
                });
            }
            return res;
        }

    }
}

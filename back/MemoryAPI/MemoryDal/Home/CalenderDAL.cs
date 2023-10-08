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
    public class CalenderDAL
    {
        /// <summary>
        /// 通过用户名查找UserCalender的值
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public List<Calender> GetCalenderByUser(string username)
        {
            SqlParameter[] sp = new SqlParameter[]
               {
                new SqlParameter("username",username)
               };
            SqlDataReader result = SQLHelper.SPQueryMultiple("sp_getCalenderByUser", sp);
            List<Calender> list = new List<Calender>();
            while (result.Read())
            {
                list.Add(new Calender()
                {
                    username = result["username"].ToString(),
                    learnword = (int)result["learnword"],
                    reviewword = (int)result["reviewword"],
                    date = result["date"].ToString(),
                });
            }
            return list;
        }


        /// <summary>
        /// 通过用户名和时间查找UserCalender的值
        /// </summary>
        /// <param name="username"></param>
        /// <param name="date"></param>
        /// <returns>存在返回true，不存在返回false</returns>
        public Res GetCalenderByDate(string username,string date)
        {
            SqlParameter[] sp = new SqlParameter[]
               {
                new SqlParameter("username",username),
                new SqlParameter("date",date)
               };
            object result = SQLHelper.SPQueryOnly1("sp_getCalenderByDate", sp);
            if (result == null)
            {
                return new Res(false, "不存在");
            }
            return new Res(true, "存在");
        }

        /// <summary>
        /// 当天已有学习数据的进行更新 新学的
        /// </summary>
        /// <param name="username"></param>
        /// <param name="learnword"></param>
        /// <param name="date"></param>
        /// <returns></returns>
        public Res Updatelearnword(string username, int learnword,string date)
        {
            SqlParameter[] sp = new SqlParameter[]{
                new SqlParameter("username",username),
                new SqlParameter("learnword",learnword),
                new SqlParameter("date",date),
            };
            int result = SQLHelper.SPInsertDeleteUpdata("sp_updateCalenderoflearnword", sp);
            if (result >= 1)
            {
                return new Res(true, "修改成功");
            }
            else
            {
                return new Res(false, "修改失败");
            }
        }

        /// <summary>
        /// 添加学习单词的数据 并给userlearn中的learnday+1
        /// </summary>
        /// <param name="username"></param>
        /// <param name="learnword"></param>
        /// <param name="date"></param>
        /// <returns></returns>
        public Res AddCalendarByLearnword(string username, int learnword, string date)
        {
            SqlParameter[] sp = new SqlParameter[]{
                new SqlParameter("username",username),
                new SqlParameter("learnword",learnword),
                new SqlParameter("date",date),
            };
            int result = SQLHelper.SPInsertDeleteUpdata("sp_addCalendarByLearnword", sp);
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
        /// 添加复习单词的数据（表中加1列） 并给userlearn中的learnday+1
        /// </summary>
        /// <param name="username"></param>
        /// <param name="learnword"></param>
        /// <param name="date"></param>
        /// <returns></returns>
        public Res AddCalendarByReviewword(string username, int reviewword, string date)
        {
            SqlParameter[] sp = new SqlParameter[]{
                new SqlParameter("username",username),
                new SqlParameter("reviewword",reviewword),
                new SqlParameter("date",date),
            };
            int result = SQLHelper.SPInsertDeleteUpdata("sp_addCalendarByReviewword", sp);
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
        /// 当天已有学习数据的进行更新 复习的
        /// </summary>
        /// <param name="username"></param>
        /// <param name="learnword"></param>
        /// <param name="date"></param>
        /// <returns></returns>
        public Res Updatereviewword(string username, int reviewword, string date)
        {
            SqlParameter[] sp = new SqlParameter[]{
                new SqlParameter("username",username),
                new SqlParameter("reviewword",reviewword),
                new SqlParameter("date",date),
            };
            int result = SQLHelper.SPInsertDeleteUpdata("sp_updateCalenderOfreviewword", sp);
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

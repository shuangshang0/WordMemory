using MemoryDal.Home;
using Model;
using Model.Home;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemoryBll.Home
{
    public class CalenderBLL
    {
        /// <summary>
        /// 通过用户名查找UserCalender的值
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public List<Calender> GetCalenderByUser(string username)
        {
           CalenderDAL dal = new CalenderDAL();
            return dal.GetCalenderByUser(username);
        }
        /// <summary>
        /// 对每天学习单词的数量统计更新
        /// </summary>
        /// <param name="username"></param>
        /// <param name="learnword"></param>
        /// <param name="date"></param>
        /// <returns></returns>
        public Res CalenderByLearnword(string username, int learnword, string date)
        {
            CalenderDAL dal = new CalenderDAL();
            var res = dal.GetCalenderByDate(username, date);
            if (res.IsSuccess == true)
            {
               return dal.Updatelearnword(username, learnword, date);
            }
            else
            {
                return dal.AddCalendarByLearnword(username, learnword, date);
            }
        }

        /// <summary>
        /// 对每天复习单词的数量统计更新
        /// </summary>
        /// <param name="username"></param>
        /// <param name="learnword"></param>
        /// <param name="date"></param>
        /// <returns></returns>
        public Res CalenderByReviewword(string username, int reviewword, string date)
        {
            CalenderDAL dal = new CalenderDAL();
            var res = dal.GetCalenderByDate(username, date);
            if (res.IsSuccess == true)
            {
                return dal.Updatereviewword(username, reviewword, date);
            }
            else
            {
                return dal.AddCalendarByReviewword(username, reviewword, date);
            }
        }
    }
}

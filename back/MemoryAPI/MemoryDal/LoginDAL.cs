using Extend;
using System.Data.SqlClient;

namespace MemoryDal
{
    public class LoginDAL
    {
        /// <summary>
        /// 登录账号密码检测
        /// </summary>
        /// <param name="username">账号</param>
        /// <param name="password">密码</param>
        /// <returns>传回username</returns>
        public string Login(string username, string password)
        {
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@username",username),
                new SqlParameter("@password",password)
            };
            //2.调用sqlhelper
            //object result = SQLHelper.GetSingleObjiect(sql, param);
            object result = SQLHelper.SPQueryOnly1("sp_userLogin", param);
            //调用存储过程
            return (string)result;
        }
    }
}
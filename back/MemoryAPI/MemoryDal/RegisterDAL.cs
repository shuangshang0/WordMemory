using Extend;
using Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Http.Headers;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace MemoryDal
{
    public class RegisterDAL
    {
        /// <summary>
        /// 用户注册
        /// </summary>
        /// <param name="username">用户名</param>
        /// <param name="password">密码</param>
        /// <returns></returns>
        public Res Register(string username, string password , string phone ,string email)
        {
            SqlParameter[] param = new SqlParameter[]
        {
                new SqlParameter("@username",username),
                new SqlParameter("@password",password),
                new SqlParameter("@phone",phone),
                new SqlParameter("@email",email)
        };
            int result_1 = SQLHelper.SPInsertDeleteUpdata("sp_register", param);
             //如果用户注册成功 为用户添加userlearn
            if (result_1 >= 0)
            {              
                SqlParameter[] param2 = new SqlParameter[] { new SqlParameter("@username", username) };
                int Result3 = SQLHelper.SPInsertDeleteUpdata("sp_addUserLearn", param2);
                if (Result3 == 0)
                {
                    return new Res(false, "用户已注册，但userlearn添加失败");
                }
                return new Res(true, "用户注册成功");
            }
            return new Res(false, "用户注册失败");
        }

        public Res finduesr(string username, string newpassword)
        {
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@username",username)
            };
            object result = SQLHelper.SPQueryOnly1("sp_findAllByusername", param);
            if (result == null)
            { return new Res(true, "不存在该用户名"); }
            else
            { return new Res(false, "存在该用户名"); }
        }

        public Res Changepwd(string username, string newpassword)
        {
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@username",username),
                new SqlParameter("@newpassword",newpassword),
            };
            int result_2 = SQLHelper.SPInsertDeleteUpdata("sp_changePwd", param);
            if (result_2 >= 0)
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
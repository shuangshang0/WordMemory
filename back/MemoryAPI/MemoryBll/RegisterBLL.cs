using MemoryDal;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemoryBll
{
    public class RegisterBLL
    {
        public Res Register(string username ,string password,string phone ,string email)
        {
            RegisterDAL registerDAL= new RegisterDAL();
            var result = registerDAL.finduesr(username ,password);
            if (result.IsSuccess == true)
            {
                return registerDAL.Register(username ,password, phone,email);
            }
            return result;
        }

        public Res Changepwd(string username, string password, string newpassword)
        {
            RegisterDAL registerDAL = new RegisterDAL();
            LoginDAL loginDAL = new LoginDAL();
            var user = loginDAL.Login(username, password);
            if (username == user)
            {
                return registerDAL.Changepwd(username, newpassword);
            }
            else
            {
                return new Res(false, "密码不正确");
            }
        }
    }
}

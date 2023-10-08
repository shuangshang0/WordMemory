using MemoryDal;

namespace MemoryBll
{
    public class LoginBLL
    {
        public string Login(string username ,string password)
        {
            LoginDAL loginDAL = new LoginDAL();
           return loginDAL.Login(username, password);
        }
    }
}
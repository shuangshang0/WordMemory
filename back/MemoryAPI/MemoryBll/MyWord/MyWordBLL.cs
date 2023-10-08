using MemoryDal.MyWord;
using Model;
using Model.Home;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemoryBll.MyWord
{
    public class MyWordBLL
    {
        public List<MyWordmodel> GetMyWord(string username) 
        { 
            MyWordDAL dal= new MyWordDAL();
            return dal.GetMyWord(username);
        }
        public Res AddMyWord(string username, string englishWord, string chineseWord)
        {
            MyWordDAL dal = new MyWordDAL();
            return dal.AddMyWord(username, englishWord, chineseWord);
        }
        public Res DeleteMyWord(string username, string englishWord, string chineseWord)
        {
            MyWordDAL dal = new MyWordDAL();
            return dal.DeleteMyWord(username, englishWord, chineseWord);
        }
    }
}

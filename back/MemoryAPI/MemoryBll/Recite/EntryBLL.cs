using MemoryDal.Recite;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemoryBll.Recite
{
    public class EntryBLL
    {
        public Res EntryLearnWord(string username, string englishword, int count, int isknow)
        {
            EntryDAL dal= new EntryDAL();
            return dal.EntryLearnWord(username,englishword,count,isknow);
        }

        public Res AddLearnWord1(string username)
        {
            EntryDAL dal = new EntryDAL();
            return dal.AddLearnWord1(username);
        }

        public Res UpdateUserWord(string username, string englishword, int count, int isknow)
        {
            EntryDAL dal = new EntryDAL();
            return dal.UpdateUserWord(username, englishword, count, isknow);
        }
    }
}

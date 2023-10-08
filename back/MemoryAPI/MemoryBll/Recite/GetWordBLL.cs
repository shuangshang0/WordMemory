using MemoryDal.Recite;
using Model.Recite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemoryBll.Recite
{
    public class GetWordBLL
    {
        public List<ENword> GetAnyWord(string username,int num,string lexicon) 
        {
            GetWordDAL dal = new GetWordDAL();
            return dal.GetAnyWord(username, num, lexicon);
        }
        public List<ENwordplus> GetReviewWordBy0(string username, int num)
        {
            GetWordDAL dal = new GetWordDAL();
            return dal.GetReviewWordBy0(username, num);
        }
        public List<ENword> Get3Random(string englishWord)
        {
            GetWordDAL dal = new GetWordDAL();
            return dal.Get3Random(englishWord);
        }
    }
}

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
    public class MsgBoardBLL
    {
       public List<CommunityMsg> MsgBoard()
        {
            MsgBoardDAL dal = new MsgBoardDAL();
            return dal.MsgBoard();
        }
        public Res AddMsg(CommunityMsg aff)
        {
            MsgBoardDAL dal = new MsgBoardDAL();
            return dal.AddMsg(aff);
        }
    }
}

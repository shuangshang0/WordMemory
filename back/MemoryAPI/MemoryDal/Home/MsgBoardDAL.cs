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
    public class MsgBoardDAL
    {
        public List<CommunityMsg> MsgBoard()
        {

            SqlDataReader result = SQLHelper.SPQueryMultiple("sp_GetAllMsg");
            List<CommunityMsg> msg = new List<CommunityMsg>();
            while (result.Read())
            {
                msg.Add(new CommunityMsg()
                {
                    username = result["username"].ToString(),
                    msg = result["msg"].ToString(),
                    date = result["date"].ToString()
                });
            }
            return msg;
        }

        public Res AddMsg(CommunityMsg aff)
        {
            SqlParameter[] param = new SqlParameter[]
           {
                new SqlParameter("@username",aff.username),
                new SqlParameter("@msg",aff.msg),
                new SqlParameter("@date",aff.date)
           };
            int res = SQLHelper.SPInsertDeleteUpdata("sp_addMsg", param);
            if (res == 0) {
                return new Res(false,"添加失败！");
            }
            return new Res(true, "添加成功！");

        }
    }
}

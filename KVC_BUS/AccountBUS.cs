using KVC_DAO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KVC_BUS
{
    public class AccountBUS
    {
        private static AccountBUS call;
        public static AccountBUS Call
        {
            get { if (call == null) call = new AccountBUS(); return call; }
            set => call = value;
        }
        private AccountBUS() { }
        public DataTable GetAllorOne(string id = "")
        {
            return AccountDAO.Call.GetAllorOne(id);
        }
    }
}

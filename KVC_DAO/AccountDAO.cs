using KVC_DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KVC_DAO
{
    public class AccountDAO
    {
        private static AccountDAO call;
        public static AccountDAO Call
        {
            get { if (call == null) call = new AccountDAO(); return call; }
            set => call = value;
        }
        private AccountDAO() { }
        public DataTable GetAllorOne(string id = "")
        {
            using (QL_KVCEntities db = new QL_KVCEntities())
            {
                List<ACCOUNT> lst = new List<ACCOUNT>();
                if (id == "")//getall
                {
                    lst = (from u in db.ACCOUNTs select u).ToList();
                }
                else lst = (from u in db.ACCOUNTs where u.ID == id select u).ToList();//getone
                return Support.ToDataTable<ACCOUNT>(lst);
            }
        }
    }
}

using KVC_DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KVC_DAO
{
    public class LoaiDBHDAO
    {
        private static LoaiDBHDAO call;
        public static LoaiDBHDAO Call
        {
            get { if (call == null) call = new LoaiDBHDAO(); return call; }
            set => call = value;
        }
        private LoaiDBHDAO() { }
        public DataTable GetAllorOne(string MALOAIDBH = "", string TENLOAIDBH = "")
        {
            using (QL_KVCEntities db = new QL_KVCEntities())
            {
                List<LOAIDOBAOHO> lst = new List<LOAIDOBAOHO>();
                if (MALOAIDBH == "" && TENLOAIDBH == "")//getall
                {
                    lst = (from u in db.LOAIDOBAOHOes select u).ToList();
                }
                else if (TENLOAIDBH == "")
                    lst = (from u in db.LOAIDOBAOHOes where u.MALOAIDBH == MALOAIDBH select u).ToList();//getone
                else if (MALOAIDBH == "")
                    lst = (from u in db.LOAIDOBAOHOes where u.TENLOAIDBH == TENLOAIDBH select u).ToList();//getone
                return Support.ToDataTable<LOAIDOBAOHO>(lst);
            }
        }
    }
}

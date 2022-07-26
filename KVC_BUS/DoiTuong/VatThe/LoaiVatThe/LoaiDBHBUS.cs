using KVC_DAO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KVC_BUS
{
    public class LoaiDBHBUS
    {
        private static LoaiDBHBUS call;
        public static LoaiDBHBUS Call
        {
            get { if (call == null) call = new LoaiDBHBUS(); return call; }
            set => call = value;
        }
        private LoaiDBHBUS() { }
        public DataTable GetAllorOne(string MALOAIDBH = "", string TENLOAIDBH = "")
        {
            return LoaiDBHDAO.Call.GetAllorOne(MALOAIDBH, TENLOAIDBH);
        }
    }
}

using KVC_DAO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KVC_BUS
{
    public class LoaiDVBUS
    {
        private static LoaiDVBUS call;
        public static LoaiDVBUS Call
        {
            get { if (call == null) call = new LoaiDVBUS(); return call; }
            set => call = value;
        }
        private LoaiDVBUS() { }
        public DataTable GetAllorOne(string MADV = "", string TENDV = "")
        {
            return LoaiDVDAO.Call.GetAllorOne(MADV, TENDV);
        }
    }
}

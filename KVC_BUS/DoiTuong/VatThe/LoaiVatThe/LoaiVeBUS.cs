using KVC_DAO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KVC_BUS
{
    public class LoaiVeBUS
    {
        private static LoaiVeBUS call;
        public static LoaiVeBUS Call
        {
            get { if (call == null) call = new LoaiVeBUS(); return call; }
            set => call = value;
        }
        private LoaiVeBUS() { }
        public DataTable GetAllorOne(string MALOAIVE = "", string TENLOAIVE = "")
        {
            return LoaiVeDAO.Call.GetAllorOne(MALOAIVE, TENLOAIVE);
        }
    }
}

using KVC_DAO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KVC_BUS
{
    public class XeBUS
    {
        private static XeBUS call;
        public static XeBUS Call
        {
            get { if (call == null) call = new XeBUS(); return call; }
            set => call = value;
        }
        private XeBUS() { }
        public DataTable GetAllorOne(string MAXE = "", string TENXE = "", string TRANGTHAI = "", string MANCC = "")
        {
            return XeDAO.Call.GetAllorOne(MAXE, TENXE, TRANGTHAI, MANCC);
        }
        public void Add(string MAXE, string TENXE, string MANCC)
        {
            XeDAO.Call.Add(MAXE, TENXE, MANCC);
        }
        public void Update(string MAXE, bool TRANGTHAI, string TENXE = "", string NGAYNHAP = "", string MANCC = "")
        {
            XeDAO.Call.Update(MAXE, TRANGTHAI, TENXE, NGAYNHAP, MANCC);
        }
    }
}

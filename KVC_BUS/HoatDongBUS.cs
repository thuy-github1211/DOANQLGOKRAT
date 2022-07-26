using KVC_DAO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KVC_BUS
{
    public class HoatDongBUS
    {
        private static HoatDongBUS call;
        public static HoatDongBUS Call
        {
            get { if (call == null) call = new HoatDongBUS(); return call; }
            set => call = value;
        }
        private HoatDongBUS() { }
        public DataTable GetAllorOne(string MAXE = "")
        {
            return HoatDongDAO.Call.GetAllorOne(MAXE);
        }
        public void Add(string MAXE, string MAKH, TimeSpan THOIGIAN, bool TRANGTHAI, string MAHLV = "", string MAHD = "")
        {
            HoatDongDAO.Call.Add(MAXE, MAKH, THOIGIAN, TRANGTHAI, MAHLV, MAHD);
        }
        public void Update(string MAXE, bool TRANGTHAI = false, string MAKH = "", string MAHLV = "")
        {
            HoatDongDAO.Call.Update(MAXE, TRANGTHAI, MAKH, MAHLV);
        }
        public void Remove(string MAXE)
        {
            HoatDongDAO.Call.Remove(MAXE);
        }
    }
}

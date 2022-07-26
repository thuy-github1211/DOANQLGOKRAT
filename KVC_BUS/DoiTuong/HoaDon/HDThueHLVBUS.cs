using KVC_DAO;
using KVC_DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KVC_BUS
{
    public class HDThueHLVBUS
    {
        private static HDThueHLVBUS call;
        public static HDThueHLVBUS Call
        {
            get { if (call == null) call = new HDThueHLVBUS(); return call; }
            set => call = value;
        }
        private HDThueHLVBUS() { }
        public DataTable GetAllorOne(string MAHD = "", string MANV = "", string MAKH = "")
        {
            return HDThueHLVDAO.Call.GetAllorOne(MAHD, MANV, MAKH);
        }
        public void Add(string MAHD, DateTime NGAYLAP, string MANV, string MAKH, string MAHLV, string MADV)
        {
            HDThueHLVDAO.Call.Add(MAHD, NGAYLAP, MANV, MAKH, MAHLV, MADV);
        }
        public void Remove(string MAHD)
        {
            HDThueHLVDAO.Call.Remove(MAHD);
        }
        //public void Update(string MAHD, string MAKH = "", string MANV = "", double TONGTIEN = 0)
        //{
        //    HDThueHLVDAO.Call.Update(MAHD, MAKH, MANV, TONGTIEN);
        //}
    }
}

using KVC_DAO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KVC_BUS
{
    public class NhanVienBUS
    {
        private static NhanVienBUS call;
        public static NhanVienBUS Call
        {
            get { if (call == null) call = new NhanVienBUS(); return call; }
            set => call = value;
        }
        private NhanVienBUS() { }
        public DataTable GetAllorOne(string MANV = "")
        {
            return NhanVienDAO.Call.GetAllorOne(MANV);
        }
        public void Add(string MANV, string TENNV, string GIOITINH, string MALOAI, string DIACHI)
        {
            NhanVienDAO.Call.Add(MANV, TENNV, GIOITINH, MALOAI, DIACHI);
        }
        public void Update(string MANV, string TENNV, string GIOITINH, string MALOAI, string DIACHI)
        {
            NhanVienDAO.Call.Update(MANV, TENNV, GIOITINH, MALOAI, DIACHI);
        }
    }
}
using KVC_DAO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KVC_BUS
{
    public class NhaCungCapBUS
    {
        private static NhaCungCapBUS call;
        public static NhaCungCapBUS Call
        {
            get { if (call == null) call = new NhaCungCapBUS(); return call; }
            set => call = value;
        }
        private NhaCungCapBUS() { }
        public DataTable GetAllorOne(string MANCC = "", string TENNCC = "")
        {
            return NhaCungCapDAO.Call.GetAllorOne(MANCC, TENNCC);
        }
        //public voMANCC AddKH(string ma, string ten, string gt, string SDT)
        //{
        //    NhaCungCapDAO.Call.AddKH(ma, ten, gt, SDT);
        //}
        //public voMANCC RemoveAccount(string MANCC)
        //{
        //    AccountsDAO.Call.RemoveAccount(MANCC);
        //}
        //public voMANCC UpdateAccount(string MANCC, string hinhanh, string password, string hoten, string diachi, string sdt, string gioitinh, DateTime ngaysinh, string email, string gioithieu, bool admin)
        //{
        //    AccountsDAO.Call.UpdateAccount(MANCC, hinhanh, password, hoten, diachi, sdt, gioitinh, ngaysinh, email, gioithieu, admin);
        //}
    }
}

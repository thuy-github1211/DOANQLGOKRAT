using KVC_DAO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KVC_BUS
{
    public class KhachHangBUS
    {
        private static KhachHangBUS call;
        public static KhachHangBUS Call
        {
            get { if (call == null) call = new KhachHangBUS(); return call; }
            set => call = value;
        }
        private KhachHangBUS() { }
        public DataTable GetAllorOne(string id = "")
        {
            return KhachHangDAO.Call.GetAllorOne(id);
        }
        public void AddKH(string ma, string ten, string gt, string SDT)
        {
            KhachHangDAO.Call.AddKH(ma, ten, gt, SDT);
        }
        //public void RemoveAccount(string id)
        //{
        //    AccountsDAO.Call.RemoveAccount(id);
        //}
        //public void UpdateAccount(string id, string hinhanh, string password, string hoten, string diachi, string sdt, string gioitinh, DateTime ngaysinh, string email, string gioithieu, bool admin)
        //{
        //    AccountsDAO.Call.UpdateAccount(id, hinhanh, password, hoten, diachi, sdt, gioitinh, ngaysinh, email, gioithieu, admin);
        //}
    }
}

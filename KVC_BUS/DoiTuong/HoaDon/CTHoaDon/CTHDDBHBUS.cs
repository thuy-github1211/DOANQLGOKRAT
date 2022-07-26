using KVC_DAO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KVC_BUS
{
    public class CTHDDBHBUS
    {
        private static CTHDDBHBUS call;
        public static CTHDDBHBUS Call
        {
            get { if (call == null) call = new CTHDDBHBUS(); return call; }
            set => call = value;
        }
        private CTHDDBHBUS() { }
        public DataTable GetAllorOne(string MAHD = "", string MADBH = "")
        {
            return CTHDDBHDAO.Call.GetAllorOne(MAHD, MADBH);
        }
        public void Add(string MAHD, string MADBH, string MALOAIDBH, double DONGIA)
        {
            CTHDDBHDAO.Call.Add(MAHD, MADBH, MALOAIDBH, DONGIA);
        }
        public void Remove(string MAHD, string MADBH)
        {
            CTHDDBHDAO.Call.Remove(MAHD, MADBH);
        }
        //public void UpdateAccount(string id, string hinhanh, string password, string hoten, string diachi, string sdt, string gioitinh, DateTime ngaysinh, string email, string gioithieu, bool admin)
        //{
        //    AccountsDAO.Call.UpdateAccount(id, hinhanh, password, hoten, diachi, sdt, gioitinh, ngaysinh, email, gioithieu, admin);
        //}
    }
}

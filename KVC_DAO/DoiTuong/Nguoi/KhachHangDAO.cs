using KVC_DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KVC_DAO
{
    public class KhachHangDAO
    {
        private static KhachHangDAO call;
        public static KhachHangDAO Call
        {
            get { if (call == null) call = new KhachHangDAO(); return call; }
            set => call = value;
        }
        private KhachHangDAO() { }
        public DataTable GetAllorOne(string id = "")
        {
            using (QL_KVCEntities db = new QL_KVCEntities())
            {
                List<KHACHHANG> lst = new List<KHACHHANG>();
                if (id == "")//getall
                {
                   lst = (from u in db.KHACHHANGs select u).ToList();
                }
                else lst = (from u in db.KHACHHANGs where u.MAKH == id select u).ToList();//getone
                return Support.ToDataTable<KHACHHANG>(lst);
            }
        }
        public void AddKH(string MAKH, string TENKH, string GIOITINH, string SDT)
        {
            using (QL_KVCEntities db = new QL_KVCEntities())
            {
                KHACHHANG KHACHHANG = new KHACHHANG { MAKH = MAKH, TENKH = TENKH, GIOITINH = GIOITINH, SDT = SDT };
                db.KHACHHANGs.Add(KHACHHANG);
                db.SaveChanges();
            }
        }
        //public void UpdateAccount(string id, string hinhanh, string password, string hoten, string diachi, string sdt, string gioitinh, DateTime ngaysinh, string email, string gioithieu, bool admin)
        //{
        //    using (QL_TVEntities db = new QL_TVEntities())
        //    {
        //        ACCOUNT ACCOUNT = db.ACCOUNTs.Find(id);
        //        ACCOUNT.HINHANH = hinhanh;
        //        ACCOUNT.PASS = password;
        //        ACCOUNT.HOTEN = hoten;
        //        ACCOUNT.DIACHI = diachi;
        //        ACCOUNT.SDT = sdt;
        //        ACCOUNT.NGAYSINH = ngaysinh;
        //        ACCOUNT.GIOITINH = gioitinh;
        //        ACCOUNT.GIOITHIEU = gioithieu;
        //        ACCOUNT.EMAIL = email;
        //        ACCOUNT.TADMIN = admin;
        //        db.SaveChanges();
        //    }
        //}
        //public void RemoveAccount(string id)
        //{
        //    using (QL_TVEntities db = new QL_TVEntities())
        //    {
        //        ACCOUNT ACCOUNT = db.ACCOUNTs.Find(id);
        //        db.ACCOUNTs.Remove(ACCOUNT);
        //        db.SaveChanges();
        //    }
        //}

    }
}

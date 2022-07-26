using KVC_DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KVC_DAO
{
    public class CTHDDBHDAO
    {
        private static CTHDDBHDAO call;
        public static CTHDDBHDAO Call
        {
            get { if (call == null) call = new CTHDDBHDAO(); return call; }
            set => call = value;
        }
        private CTHDDBHDAO() { }
        public DataTable GetAllorOne(string MAHD = "", string MADBH = "")
        {
            using (QL_KVCEntities db = new QL_KVCEntities())
            {
                List<CTHDDOBAOHO> lst = new List<CTHDDOBAOHO>();
                if (MAHD == "" && MADBH == "")//getall
                {
                    lst = (from u in db.CTHDDOBAOHOes select u).ToList();
                }
                else if(MAHD != "" && MADBH == "")
                    lst = (from u in db.CTHDDOBAOHOes where u.MAHD == MAHD select u).ToList();//getone
                else if (MADBH != "" && MAHD == "")
                    lst = (from u in db.CTHDDOBAOHOes where u.MADBH == MADBH select u).ToList();//getone
                else lst = (from u in db.CTHDDOBAOHOes where u.MAHD == MAHD && u.MADBH == MADBH select u).ToList();//getone
                return Support.ToDataTable<CTHDDOBAOHO>(lst);
            }
        }
        public void Add(string MAHD, string MADBH, string MALOAIDBH, double DONGIA)
        {
            using (QL_KVCEntities db = new QL_KVCEntities())
            {
                CTHDDOBAOHO ctHD_DBH = new CTHDDOBAOHO { MAHD = MAHD, MADBH = MADBH, MALOAIDBH = MALOAIDBH, DONGIA = DONGIA };
                db.CTHDDOBAOHOes.Add(ctHD_DBH);
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
        public void Remove(string MAHD, string MADBH)
        {
            using (QL_KVCEntities db = new QL_KVCEntities())
            {
                CTHDDOBAOHO ctHD_DBH = db.CTHDDOBAOHOes.Find(MAHD, MADBH);
                db.CTHDDOBAOHOes.Remove(ctHD_DBH);
                db.SaveChanges();
            }
        }
    }
}

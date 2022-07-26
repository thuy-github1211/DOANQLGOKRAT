using KVC_DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KVC_DAO
{
    public class NhaCungCapDAO
    {
        private static NhaCungCapDAO call;
        public static NhaCungCapDAO Call
        {
            get { if (call == null) call = new NhaCungCapDAO(); return call; }
            set => call = value;
        }
        private NhaCungCapDAO() { }
        public DataTable GetAllorOne(string MANCC = "", string TENNCC = "")
        {
            using (QL_KVCEntities db = new QL_KVCEntities())
            {
                List<NHACC> lst = new List<NHACC>();
                if (MANCC == "" && TENNCC == "")//getall
                {
                   lst = (from u in db.NHACCs select u).ToList();
                }
                else if(TENNCC != "" && MANCC == "")
                    lst = (from u in db.NHACCs where u.TENNCC == TENNCC select u).ToList();//getone
                else lst = (from u in db.NHACCs where u.MANCC == MANCC select u).ToList();//getone
                return Support.ToDataTable<NHACC>(lst);
            }
        }
        //public void Add(string MAKH, string TENKH, string GIOITINH, string SDT)
        //{
        //    using (QL_KVCEntities db = new QL_KVCEntities())
        //    {
        //        NHACC NHACC = new NHACC { MAKH = MAKH, TENKH = TENKH, GIOITINH = GIOITINH, SDT = SDT };
        //        db.NHACCs.Add(NHACC);
        //        db.SaveChanges();
        //    }
        //}
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

using KVC_DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KVC_DAO
{
    public class CTHDBVDAO
    {
        private static CTHDBVDAO call;
        public static CTHDBVDAO Call
        {
            get { if (call == null) call = new CTHDBVDAO(); return call; }
            set => call = value;
        }
        private CTHDBVDAO() { }
        public DataTable GetAllorOne(string MACTHD = "", string MAHD = "")
        {
            using (QL_KVCEntities db = new QL_KVCEntities())
            {
                List<CTHOADONBANVE> lst = new List<CTHOADONBANVE>();
                if (MACTHD == "")//getall
                {
                    if(MAHD == "")
                        lst = (from u in db.CTHOADONBANVEs select u).ToList();
                    else lst = (from u in db.CTHOADONBANVEs where u.MAHD == MAHD select u).ToList();
                }
                else lst = (from u in db.CTHOADONBANVEs where u.MACTHD == MACTHD select u).ToList();//getone
                return Support.ToDataTable<CTHOADONBANVE>(lst);
            }
        }
        public void Add(string MACTHD, string MAHD, string MALOAIVE, string MAXE, double DONGIA)
        {
            using (QL_KVCEntities db = new QL_KVCEntities())
            {
                CTHOADONBANVE ctHDBV = new CTHOADONBANVE { MACTHD = MACTHD, MAHD = MAHD, MALOAIVE = MALOAIVE, MAXE = MAXE, DONGIA = DONGIA };
                db.CTHOADONBANVEs.Add(ctHDBV);
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
        public void Remove(string MACTHD = "")
        {
            using (QL_KVCEntities db = new QL_KVCEntities())
            {
                CTHOADONBANVE ctHDBV = db.CTHOADONBANVEs.Find(MACTHD);
                db.CTHOADONBANVEs.Remove(ctHDBV);
                db.SaveChanges();
            }
        }
    }
}

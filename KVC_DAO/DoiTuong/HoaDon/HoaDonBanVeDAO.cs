using KVC_DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace KVC_DAO
{
    public class HoaDonBanVeDAO
    {
        private static HoaDonBanVeDAO call;
        public static HoaDonBanVeDAO Call
        {
            get { if (call == null) call = new HoaDonBanVeDAO(); return call; }
            set => call = value;
        }
        private HoaDonBanVeDAO() { }
        public DataTable GetAllorOne(string MAHD = "", string MANV = "", string MAKH = "")
        {
            using (QL_KVCEntities db = new QL_KVCEntities())
            {
                List<HOADONBANVE> lst = new List<HOADONBANVE>();
                if (MAHD == "")//getall
                {
                    if(MANV == "" && MAKH == "")
                    {
                        lst = (from u in db.HOADONBANVEs select u).ToList();
                    }
                    else if(MAKH == "")
                    {
                        lst = (from u in db.HOADONBANVEs where u.MANV == MANV select u).ToList();
                    }
                    else if (MANV == "") lst = (from u in db.HOADONBANVEs where u.MAKH == MAKH select u).ToList();
                }
                else lst = (from u in db.HOADONBANVEs where u.MAHD == MAHD select u).ToList();//getone
                return Support.ToDataTable<HOADONBANVE>(lst);
            }
        }
        public void Add(string MAHD, string MAKH, string MANV, DateTime NGAYIN, double TONGTIEN)
        {
            using (QL_KVCEntities db = new QL_KVCEntities())
            {
                HOADONBANVE HDBV = new HOADONBANVE { MAHD = MAHD, MAKH = MAKH, MANV = MANV, NGAYIN = NGAYIN, TONGTIEN = TONGTIEN };
                db.HOADONBANVEs.Add(HDBV);
                db.SaveChanges();
            }
        }
        public void Update(string MAHD, string MAKH = "", string MANV = "", double TONGTIEN = 0)
        {
            using (QL_KVCEntities db = new QL_KVCEntities())
            {
                HOADONBANVE HDBV = db.HOADONBANVEs.Find(MAHD);
                if(MAKH != "")
                    HDBV.MAKH = MAKH;
                if (MANV != "")
                    HDBV.MANV = MANV;
                if (TONGTIEN != 0)
                    HDBV.TONGTIEN = TONGTIEN;
                db.SaveChanges();
            }
        }
        public void Remove(string MAHD)
        {
            using (QL_KVCEntities db = new QL_KVCEntities())
            {
                HOADONBANVE HDBV = db.HOADONBANVEs.Find(MAHD);
                db.HOADONBANVEs.Remove(HDBV);
                db.SaveChanges();
            }
        }
    }
}

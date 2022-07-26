using KVC_DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KVC_DAO
{
    public class HDThueHLVDAO
    {
        private static HDThueHLVDAO call;
        public static HDThueHLVDAO Call
        {
            get { if (call == null) call = new HDThueHLVDAO(); return call; }
            set => call = value;
        }
        private HDThueHLVDAO() { }
        public DataTable GetAllorOne(string MAHD = "", string MANV = "", string MAKH = "")
        {
            using (QL_KVCEntities db = new QL_KVCEntities())
            {
                List<HOADONTHUEHLV> lst = new List<HOADONTHUEHLV>();
                if (MAHD == "")//getall
                {
                    //if (MANV == "" && MAKH == "")
                    //{
                        lst = (from u in db.HOADONTHUEHLVs select u).ToList();
                    //}
                    //else if (MAKH == "")
                    //{
                    //    lst = (from u in db.HOADONTHUEHLVs where u.MANV == MANV select u).ToList();
                    //}
                    //else if (MANV == "") lst = (from u in db.HOADONTHUEHLVs where u.MAKH == MAKH select u).ToList();
                }
                else lst = (from u in db.HOADONTHUEHLVs where u.MAHD == MAHD select u).ToList();//getone
                return Support.ToDataTable<HOADONTHUEHLV>(lst);
            }
        }
        public void Add(string MAHD, DateTime NGAYLAP, string MANV, string MAKH, string MAHLV, string MADV)
        {
            using (QL_KVCEntities db = new QL_KVCEntities())
            {
                HOADONTHUEHLV HD = new HOADONTHUEHLV { MAHD = MAHD, NGAYLAP = NGAYLAP, MANV = MANV, MAKH = MAKH, MAHLV = MAHLV, MADV = MADV };
                db.HOADONTHUEHLVs.Add(HD);
                db.SaveChanges();
            }
        }
        //public void Update(string MAHD, string MANV = "", string MAKH ="", string MAHLV = "", string MADV = "")
        //{
        //    using (QL_KVCEntities db = new QL_KVCEntities())
        //    {
        //        HOADONTHUEHLV HD = db.HOADONTHUEHLVs.Find(MAHD);
        //        if (MAKH != "")
        //            HDBV.MAKH = MAKH;
        //        if (MANV != "")
        //            HDBV.MANV = MANV;
        //        if (TONGTIEN != 0)
        //            HDBV.TONGTIEN = TONGTIEN;
        //        db.SaveChanges();
        //    }
        //}
        public void Remove(string MAHD)
        {
            using (QL_KVCEntities db = new QL_KVCEntities())
            {
                HOADONTHUEHLV HD = db.HOADONTHUEHLVs.Find(MAHD);
                db.HOADONTHUEHLVs.Remove(HD);
                db.SaveChanges();
            }
        }
    }
}

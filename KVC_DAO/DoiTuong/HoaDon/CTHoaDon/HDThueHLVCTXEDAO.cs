using KVC_DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KVC_DAO
{
    public class HDThueHLVCTXEDAO
    {
        private static HDThueHLVCTXEDAO call;
        public static HDThueHLVCTXEDAO Call
        {
            get { if (call == null) call = new HDThueHLVCTXEDAO(); return call; }
            set => call = value;
        }
        private HDThueHLVCTXEDAO() { }
        public DataTable GetAllorOne(string MAHD = "", string MAXE = "")
        {
            using (QL_KVCEntities db = new QL_KVCEntities())
            {
                List<HOADONTHUEHLVCTXE> lst = new List<HOADONTHUEHLVCTXE>();
                if (MAHD == "" && MAXE == "")//getall
                {
                    lst = (from u in db.HOADONTHUEHLVCTXEs select u).ToList();
                }
                else if(MAHD == "")
                    lst = (from u in db.HOADONTHUEHLVCTXEs where u.MAXE == MAXE select u).ToList();//getone
                else if (MAXE == "")
                    lst = (from u in db.HOADONTHUEHLVCTXEs where u.MAHD == MAHD select u).ToList();//getone
                return Support.ToDataTable<HOADONTHUEHLVCTXE>(lst);
            }
        }
        public void Add(string MAHD, string MAXE)
        {
            using (QL_KVCEntities db = new QL_KVCEntities())
            {
                HOADONTHUEHLVCTXE xe = new HOADONTHUEHLVCTXE { MAXE = MAXE, MAHD = MAHD, DEOBIET = true};
                db.HOADONTHUEHLVCTXEs.Add(xe);
                db.SaveChanges();
            }
        }
        //public void Update(string MAHLV, string TENHLV, string DIACHI, bool TRANGTHAI)
        //{
        //    using (QL_KVCEntities db = new QL_KVCEntities())
        //    {
        //        HUANLUYENVIEN HUANLUYENVIEN = db.HUANLUYENVIENs.Find(MAHLV);
        //        HUANLUYENVIEN.TENHLV = TENHLV;
        //        HUANLUYENVIEN.DIACHI = DIACHI;
        //        HUANLUYENVIEN.TRANGTHAI = TRANGTHAI;
        //        db.SaveChanges();
        //    }
        //}
    }
}

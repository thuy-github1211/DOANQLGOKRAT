using KVC_DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KVC_DAO
{
    public class CTPhieuNhapTTBDAO
    {
        private static CTPhieuNhapTTBDAO call;
        public static CTPhieuNhapTTBDAO Call
        {
            get { if (call == null) call = new CTPhieuNhapTTBDAO(); return call; }
            set => call = value;
        }
        private CTPhieuNhapTTBDAO() { }
        public DataTable GetAllorOne(string MAPN = "", string MANCC = "", string MATHIETBI = "")
        {
            using (QL_KVCEntities db = new QL_KVCEntities())
            {
                List<CTPHIEUNHAPTTB> lst = new List<CTPHIEUNHAPTTB>();
                if (MAPN == "" && MANCC == "" && MATHIETBI == "")//getall
                {
                    lst = (from u in db.CTPHIEUNHAPTTBs select u).ToList();
                }
                else if(MAPN == "" && MANCC == "")
                    lst = (from u in db.CTPHIEUNHAPTTBs where u.MATHIETBI == MATHIETBI select u).ToList();//getone
                else if (MAPN == "" && MATHIETBI == "")
                    lst = (from u in db.CTPHIEUNHAPTTBs where u.MANCC == MANCC select u).ToList();//getone
                else if (MANCC == "" && MATHIETBI == "")
                    lst = (from u in db.CTPHIEUNHAPTTBs where u.MAPN == MAPN select u).ToList();//getone
                else lst = (from u in db.CTPHIEUNHAPTTBs where (u.MAPN == MAPN && u.MANCC == MANCC && u.MATHIETBI == MATHIETBI) select u).ToList();//getone
                return Support.ToDataTable<CTPHIEUNHAPTTB>(lst);
            }
        }
        public void Add(string MAPN, string MANCC, string MATHIETBI, int SOLUONG, double DONGIA)
        {
            using (QL_KVCEntities db = new QL_KVCEntities())
            {
                CTPHIEUNHAPTTB PN = new CTPHIEUNHAPTTB { MAPN = MAPN, MANCC = MANCC, MATHIETBI = MATHIETBI, SOLUONG = SOLUONG, DONGIA = DONGIA };
                db.CTPHIEUNHAPTTBs.Add(PN);
                db.SaveChanges();
            }
        }
        public void Update(string MAPN, string MANCC, string MATHIETBI, int SOLUONG, double DONGIA)
        {
            using (QL_KVCEntities db = new QL_KVCEntities())
            {
                CTPHIEUNHAPTTB PN = db.CTPHIEUNHAPTTBs.Find(MAPN, MANCC, MATHIETBI);
                PN.SOLUONG = SOLUONG;
                PN.DONGIA = DONGIA;
                db.SaveChanges();
            }
        }
        public void Remove(string MAPN, string MANCC, string MATHIETBI)
        {
            using (QL_KVCEntities db = new QL_KVCEntities())
            {
                CTPHIEUNHAPTTB PN = db.CTPHIEUNHAPTTBs.Find(MAPN, MANCC, MATHIETBI);
                db.CTPHIEUNHAPTTBs.Remove(PN);
                db.SaveChanges();
            }
        }
    }
}

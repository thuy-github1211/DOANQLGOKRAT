using KVC_DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KVC_DAO
{
    public class CTPhieuNhapDBHDAO
    {
        private static CTPhieuNhapDBHDAO call;
        public static CTPhieuNhapDBHDAO Call
        {
            get { if (call == null) call = new CTPhieuNhapDBHDAO(); return call; }
            set => call = value;
        }
        private CTPhieuNhapDBHDAO() { }
        public DataTable GetAllorOne(string MAPN = "", string MANCC = "", string TENDBH = "")
        {
            using (QL_KVCEntities db = new QL_KVCEntities())
            {
                List<CTPHIEUNHAPDBH> lst = new List<CTPHIEUNHAPDBH>();
                if (MAPN == "")//getall
                {
                    lst = (from u in db.CTPHIEUNHAPDBHs select u).ToList();
                }
                else if (MAPN == "" && MANCC == "")
                    lst = (from u in db.CTPHIEUNHAPDBHs where u.TENDBH == TENDBH select u).ToList();//getone
                else if (MAPN == "" && TENDBH == "")
                    lst = (from u in db.CTPHIEUNHAPDBHs where u.MANCC == MANCC select u).ToList();//getone
                else if (MANCC == "" && TENDBH == "")
                    lst = (from u in db.CTPHIEUNHAPDBHs where u.MAPN == MAPN select u).ToList();//getone
                else lst = (from u in db.CTPHIEUNHAPDBHs where (u.MAPN == MAPN && u.MANCC == MANCC && u.TENDBH == TENDBH) select u).ToList();//getone
                return Support.ToDataTable<CTPHIEUNHAPDBH>(lst);
            }
        }
        public void Add(string MAPN, string MANCC, string TENDBH, int SOLUONG, double DONGIA)
        {
            using (QL_KVCEntities db = new QL_KVCEntities())
            {
                CTPHIEUNHAPDBH PN = new CTPHIEUNHAPDBH { MAPN = MAPN, MANCC = MANCC, TENDBH = TENDBH, SOLUONG = SOLUONG, DONGIA = DONGIA };
                db.CTPHIEUNHAPDBHs.Add(PN);
                db.SaveChanges();
            }
        }
        public void Update(string MAPN, string MANCC, string MADOBAOHO, int SOLUONG, double DONGIA)
        {
            using (QL_KVCEntities db = new QL_KVCEntities())
            {
                CTPHIEUNHAPDBH PN = db.CTPHIEUNHAPDBHs.Find(MAPN, MANCC, MADOBAOHO);
                PN.SOLUONG = SOLUONG;
                PN.DONGIA = DONGIA;
                db.SaveChanges();
            }
        }
        public void Remove(string MAPN, string MANCC, string MADOBAOHO)
        {
            using (QL_KVCEntities db = new QL_KVCEntities())
            {
                CTPHIEUNHAPDBH PN = db.CTPHIEUNHAPDBHs.Find(MAPN, MANCC, MADOBAOHO);
                db.CTPHIEUNHAPDBHs.Remove(PN);
                db.SaveChanges();
            }
        }
    }
}

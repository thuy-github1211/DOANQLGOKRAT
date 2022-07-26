using KVC_DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KVC_DAO
{
    public class CTPhieuNhapXeDAO
    {
        private static CTPhieuNhapXeDAO call;
        public static CTPhieuNhapXeDAO Call
        {
            get { if (call == null) call = new CTPhieuNhapXeDAO(); return call; }
            set => call = value;
        }
        private CTPhieuNhapXeDAO() { }
        public DataTable GetAllorOne(string MAPN = "", string MANCC = "", string TENXE = "")
        {
            using (QL_KVCEntities db = new QL_KVCEntities())
            {
                List<CTPHIEUNHAPXE> lst = new List<CTPHIEUNHAPXE>();
                if (MAPN == "")//getall
                {
                    lst = (from u in db.CTPHIEUNHAPXEs select u).ToList();
                }
                else if (MAPN == "" && MANCC == "")
                    lst = (from u in db.CTPHIEUNHAPXEs where u.TENXE == TENXE select u).ToList();//getone
                else if (MAPN == "" && TENXE == "")
                    lst = (from u in db.CTPHIEUNHAPXEs where u.MANCC == MANCC select u).ToList();//getone
                else if (MANCC == "" && TENXE == "")
                    lst = (from u in db.CTPHIEUNHAPXEs where u.MAPN == MAPN select u).ToList();//getone
                else lst = (from u in db.CTPHIEUNHAPXEs where (u.MAPN == MAPN && u.MANCC == MANCC && u.TENXE == TENXE) select u).ToList();//getone
                return Support.ToDataTable<CTPHIEUNHAPXE>(lst);
            }
        }
        public void Add(string MAPN, string MANCC, string TENXE, int SOLUONG, double DONGIA)
        {
            using (QL_KVCEntities db = new QL_KVCEntities())
            {
                CTPHIEUNHAPXE PN = new CTPHIEUNHAPXE { MAPN = MAPN, MANCC = MANCC, TENXE = TENXE, SOLUONG = SOLUONG, DONGIA = DONGIA };
                db.CTPHIEUNHAPXEs.Add(PN);
                db.SaveChanges();
            }
        }
        public void Update(string MAPN, string MANCC, string MAXE, int SOLUONG, double DONGIA)
        {
            using (QL_KVCEntities db = new QL_KVCEntities())
            {
                CTPHIEUNHAPXE PN = db.CTPHIEUNHAPXEs.Find(MAPN, MANCC, MAXE);
                PN.SOLUONG = SOLUONG;
                PN.DONGIA = DONGIA;
                db.SaveChanges();
            }
        }
        public void Remove(string MAPN, string MANCC, string MAXE)
        {
            using (QL_KVCEntities db = new QL_KVCEntities())
            {
                CTPHIEUNHAPXE PN = db.CTPHIEUNHAPXEs.Find(MAPN, MANCC, MAXE);
                db.CTPHIEUNHAPXEs.Remove(PN);
                db.SaveChanges();
            }
        }
    }
}

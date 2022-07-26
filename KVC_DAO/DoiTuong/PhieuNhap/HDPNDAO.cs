using KVC_DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KVC_DAO
{
    public class HDPNDAO
    {
        private static HDPNDAO call;
        public static HDPNDAO Call
        {
            get { if (call == null) call = new HDPNDAO(); return call; }
            set => call = value;
        }
        private HDPNDAO() { }
        public DataTable GetAllorOne(string MAPN = "")
        {
            using (QL_KVCEntities db = new QL_KVCEntities())
            {
                List<PHIEUNHAP> lst = new List<PHIEUNHAP>();
                if (MAPN == "")//getall
                {
                        lst = (from u in db.PHIEUNHAPs select u).ToList();
                }
                else lst = (from u in db.PHIEUNHAPs where u.MAPN == MAPN select u).ToList();//getone
                return Support.ToDataTable<PHIEUNHAP>(lst);
            }
        }
        public void Add(string MAPN, int LOAIPN, string MANV)
        {
            using (QL_KVCEntities db = new QL_KVCEntities())
            {
                PHIEUNHAP PN = new PHIEUNHAP { MAPN = MAPN, LOAIPN = LOAIPN, NGAYNHAP = DateTime.Now, TONGTIEN = 0, MANV = MANV};
                db.PHIEUNHAPs.Add(PN);
                db.SaveChanges();
            }
        }
        public void Update(string MAPN, int LOAIPN = -1, string NGAYNHAP = "", string TONGTIEN = "", string MANV = "")
        {
            using (QL_KVCEntities db = new QL_KVCEntities())
            {
                PHIEUNHAP PN = db.PHIEUNHAPs.Find(MAPN);
                if(LOAIPN != -1)
                    PN.LOAIPN = LOAIPN;
                if(NGAYNHAP != "")
                    PN.NGAYNHAP = DateTime.Parse(NGAYNHAP);
                if (TONGTIEN != "")
                    PN.TONGTIEN = double.Parse(TONGTIEN);
                if (MANV != "")
                    PN.MANV = MANV;
                db.SaveChanges();
            }
        }
        public void Remove(string MAPN = "")
        {
            using (QL_KVCEntities db = new QL_KVCEntities())
            {
                PHIEUNHAP PN = db.PHIEUNHAPs.Find(MAPN);
                db.PHIEUNHAPs.Remove(PN);
                db.SaveChanges();
            }
        }
    }
}

using KVC_DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KVC_DAO
{
    public class BaoTriDAO
    {
        private static BaoTriDAO call;
        public static BaoTriDAO Call
        {
            get { if (call == null) call = new BaoTriDAO(); return call; }
            set => call = value;
        }
        private BaoTriDAO() { }
        public DataTable GetAllorOne(string MABAOTRI = "")
        {
            using (QL_KVCEntities db = new QL_KVCEntities())
            {
                List<BAOTRI> lst = new List<BAOTRI>();
                if (MABAOTRI == "")//getall
                {
                    lst = (from u in db.BAOTRIs select u).ToList();
                }
                else lst = (from u in db.BAOTRIs where u.MABAOTRI == MABAOTRI select u).ToList();//getone
                return Support.ToDataTable<BAOTRI>(lst);
            }
        }
        public void Add(string MABAOTRI, string MAXE, string MANV, int tg)
        {
            using (QL_KVCEntities db = new QL_KVCEntities())
            {
                BAOTRI bt = new BAOTRI { MAXE = MAXE, MABAOTRI = MABAOTRI, MANV = MANV, NGAYLAP = DateTime.Now, TONGTHOIGIANBAOTRI = tg };
                db.BAOTRIs.Add(bt);
                db.SaveChanges();
            }
        }

        public void Update(string MABAOTRI, int tg)
        {
            using (QL_KVCEntities db = new QL_KVCEntities())
            {
                BAOTRI hd = db.BAOTRIs.Find(MABAOTRI);
                hd.TONGTHOIGIANBAOTRI = tg;
                db.SaveChanges();
            }
        }
        public void Remove(string MABAOTRI)
        {
            using (QL_KVCEntities db = new QL_KVCEntities())
            {
                BAOTRI hd = db.BAOTRIs.Find(MABAOTRI);
                db.BAOTRIs.Remove(hd);
                db.SaveChanges();
            }
        }
    }
}

using KVC_DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KVC_DAO
{
    public class TrangThietBiDAO
    {
        private static TrangThietBiDAO call;
        public static TrangThietBiDAO Call
        {
            get { if (call == null) call = new TrangThietBiDAO(); return call; }
            set => call = value;
        }
        private TrangThietBiDAO() { }
        public DataTable GetAllorOne(string MATHIETBI = "")
        {
            using (QL_KVCEntities db = new QL_KVCEntities())
            {
                List<TRANGTHIETBI> lst = new List<TRANGTHIETBI>();
                if (MATHIETBI == "")//getall
                {
                        lst = (from u in db.TRANGTHIETBIs select u).ToList();
                }
                else lst = (from u in db.TRANGTHIETBIs where u.MATHIETBI == MATHIETBI select u).ToList();//getone
                return Support.ToDataTable<TRANGTHIETBI>(lst);
            }
        }
        public void Add(string MATHIETBI, string TENTHIETBI, string MANCC,int SOLUONG)
        {
            using (QL_KVCEntities db = new QL_KVCEntities())
            {
                TRANGTHIETBI TRANGTHIETBI = new TRANGTHIETBI { MATHIETBI = MATHIETBI, TENTHIETBI = TENTHIETBI, NGAYNHAP = DateTime.Now, MANCC = MANCC , TONKHO = SOLUONG };
                db.TRANGTHIETBIs.Add(TRANGTHIETBI);
                db.SaveChanges();
            }
        }
        public void Remove(string MATHIETBI)
        {
            using (QL_KVCEntities db = new QL_KVCEntities())
            {
                TRANGTHIETBI TRANGTHIETBI = db.TRANGTHIETBIs.Find(MATHIETBI);
                db.TRANGTHIETBIs.Remove(TRANGTHIETBI);
                db.SaveChanges();
            }
        }
        public void Update(string MATHIETBI, int TONKHO, string TENTHIETBI = "", string MANCC = "")
        {
            using (QL_KVCEntities db = new QL_KVCEntities())
            {
                TRANGTHIETBI TRANGTHIETBI = db.TRANGTHIETBIs.Find(MATHIETBI);
                if (TENTHIETBI != "")
                    TRANGTHIETBI.TENTHIETBI = TENTHIETBI;
                if (MANCC != "")
                    TRANGTHIETBI.MANCC = MANCC;
                TRANGTHIETBI.TONKHO = TONKHO;
                db.SaveChanges();
            }
        }
    }
}

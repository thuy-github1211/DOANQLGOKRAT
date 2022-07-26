using KVC_DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KVC_DAO
{
    public class HoatDongDAO
    {
        private static HoatDongDAO call;
        public static HoatDongDAO Call
        {
            get { if (call == null) call = new HoatDongDAO(); return call; }
            set => call = value;
        }
        private HoatDongDAO() { }
        public DataTable GetAllorOne(string MAXE = "")
        {
            using (QL_KVCEntities db = new QL_KVCEntities())
            {
                List<HOATDONG> lst = new List<HOATDONG>();
                if (MAXE == "")//getall
                {
                    lst = (from u in db.HOATDONGs select u).ToList();
                }
                else lst = (from u in db.HOATDONGs where u.MAXE == MAXE select u).ToList();//getone
                return Support.ToDataTable<HOATDONG>(lst);
            }
        }
        public void Add(string MAXE, string MAKH, TimeSpan THOIGIAN, bool TRANGTHAI, string MAHLV = "", string MAHD = "")
        {
            using (QL_KVCEntities db = new QL_KVCEntities())
            {
                HOATDONG hd;
                if (MAHLV != "")
                {
                    hd = new HOATDONG { MAXE = MAXE, MAKH = MAKH, THOIGIAN = THOIGIAN, MAHLV = MAHLV, TRANGTHAI = TRANGTHAI };
                }    
                else hd = new HOATDONG { MAXE = MAXE, MAKH = MAKH, THOIGIAN = THOIGIAN, TRANGTHAI = TRANGTHAI, MAHD = MAHD };
                db.HOATDONGs.Add(hd);
                db.SaveChanges();
            }
        }

        public void Update(string MAXE, bool TRANGTHAI = false, string MAKH = "", string MAHLV = "")
        {
            using (QL_KVCEntities db = new QL_KVCEntities())
            {
                HOATDONG hd = db.HOATDONGs.Find(MAXE);
                hd.TRANGTHAI = TRANGTHAI;
                if (MAKH != "")
                    hd.MAKH = MAKH;
                if (MAHLV != "")
                    hd.MAHLV = MAHLV;
                hd.BATDAU = DateTime.Now.TimeOfDay;
                db.SaveChanges();
            }
        }
        public void Remove(string MAXE)
        { 
            using (QL_KVCEntities db = new QL_KVCEntities())
            {
                HOATDONG hd = db.HOATDONGs.Find(MAXE);
                db.HOATDONGs.Remove(hd);
                db.SaveChanges();
            }
        }
    }
}

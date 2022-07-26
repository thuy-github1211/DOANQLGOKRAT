using KVC_DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KVC_DAO
{
    public class XeDAO
    {
        private static XeDAO call;
        public static XeDAO Call
        {
            get { if (call == null) call = new XeDAO(); return call; }
            set => call = value;
        }
        private XeDAO() { }
        public DataTable GetAllorOne(string MAXE = "", string TENXE = "", string TRANGTHAI = "", string MANCC = "")
        {
            using (QL_KVCEntities db = new QL_KVCEntities())
            {
                bool trangthai;
                if (TRANGTHAI == "1")
                    trangthai = true;
                else trangthai = false;
                List<XE> lst = new List<XE>();
                if (MAXE == "")//getall
                {
                    if (TRANGTHAI == "" && TENXE == "" && MANCC == "")
                        lst = (from u in db.XEs select u).ToList();
                    else if (TRANGTHAI != "" && TENXE == "" && MANCC == "")
                        lst = (from u in db.XEs where u.TRANGTHAI == trangthai select u).ToList();
                    else if (TENXE != "" && TRANGTHAI == "" && MANCC == "")
                        lst = (from u in db.XEs where u.TENXE == TENXE select u).ToList();
                    else if (MANCC != "" && TENXE == "" && TRANGTHAI == "")
                        lst = (from u in db.XEs where u.MANCC == MANCC select u).GroupBy(x => x.TENXE).Select(x => x.FirstOrDefault()).ToList();
                    else lst = (from u in db.XEs where u.TENXE == TENXE && u.TRANGTHAI == trangthai select u).ToList();
                }
                else lst = (from u in db.XEs where u.MAXE == MAXE select u).ToList();//getone
                return Support.ToDataTable<XE>(lst);
            }
        }
        public void Add(string MAXE, string TENXE, string MANCC)
        {
            using (QL_KVCEntities db = new QL_KVCEntities())
            {
                XE xe = new XE { MAXE = MAXE, TRANGTHAI = true, TENXE = TENXE, NGAYNHAP = DateTime.Now, MANCC = MANCC };
                db.XEs.Add(xe);
                db.SaveChanges();
            }
        }
        public void Update(string MAXE, bool TRANGTHAI, string TENXE = "", string NGAYNHAP = "", string MANCC = "")
        {
            using (QL_KVCEntities db = new QL_KVCEntities())
            {
                XE XE = db.XEs.Find(MAXE);
                if(TENXE != "")
                    XE.TENXE = TENXE;
                if (NGAYNHAP != "")
                    XE.NGAYNHAP = DateTime.Parse(NGAYNHAP);
                if (MANCC != "")
                    XE.MANCC = MANCC;
                XE.TRANGTHAI = TRANGTHAI;
                db.SaveChanges();
            }
        }
    }
}

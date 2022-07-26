using KVC_DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KVC_DAO
{
    public class HLVDAO
    {
            private static HLVDAO call;
            public static HLVDAO Call
            {
                get { if (call == null) call = new HLVDAO(); return call; }
                set => call = value;
            }
            private HLVDAO() { }
            public DataTable GetAllorOne(string MAHLV = "")
            {
                using (QL_KVCEntities db = new QL_KVCEntities())
                {
                    List<HUANLUYENVIEN> lst = new List<HUANLUYENVIEN>();
                    if (MAHLV == "")//getall
                    {
                        lst = (from u in db.HUANLUYENVIENs select u).ToList();
                    }
                    else lst = (from u in db.HUANLUYENVIENs where u.MAHLV == MAHLV select u).ToList();//getone
                    return Support.ToDataTable<HUANLUYENVIEN>(lst);
                }
            }
            public void Add(string MAHLV, string TENHLV, string DIACHI, bool TRANGTHAI)
            {
                using (QL_KVCEntities db = new QL_KVCEntities())
                {
                    HUANLUYENVIEN HUANLUYENVIEN = new HUANLUYENVIEN { MAHLV = MAHLV, TENHLV = TENHLV, DIACHI = DIACHI, TRANGTHAI = TRANGTHAI };
                    db.HUANLUYENVIENs.Add(HUANLUYENVIEN);
                    db.SaveChanges();
                }
            }
        public void Update(string MAHLV, bool TRANGTHAI, string TENHLV = "", string DIACHI = "")
        {
            using (QL_KVCEntities db = new QL_KVCEntities())
            {
                HUANLUYENVIEN HUANLUYENVIEN = db.HUANLUYENVIENs.Find(MAHLV);
                if(TENHLV != "")
                    HUANLUYENVIEN.TENHLV = TENHLV;
                if (DIACHI != "")
                    HUANLUYENVIEN.DIACHI = DIACHI;
                HUANLUYENVIEN.TRANGTHAI = TRANGTHAI;
                db.SaveChanges();
            }
        }
    }
}
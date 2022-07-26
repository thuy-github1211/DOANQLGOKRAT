using KVC_DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KVC_DAO
{
    public class DoBaoHoDAO
    {
        private static DoBaoHoDAO call;
        public static DoBaoHoDAO Call
        {
            get { if (call == null) call = new DoBaoHoDAO(); return call; }
            set => call = value;
        }
        private DoBaoHoDAO() { }
        public DataTable GetAllorOne(string MADOBAOHO = "",string TENDBH = "", string TRANGTHAI = "", string MALOAIDBH = "", string MANHACC = "")
        {
            using (QL_KVCEntities db = new QL_KVCEntities())
            {
                bool trangthai;
                if (TRANGTHAI == "1")
                    trangthai = true;
                else trangthai = false;
                List<DOBAOHO> lst = new List<DOBAOHO>();
                if (MADOBAOHO == "" && TENDBH == "")//getall
                {
                    if(TRANGTHAI == "" && MALOAIDBH == "" && MANHACC == "")
                        lst = (from u in db.DOBAOHOes select u).ToList();
                    else if(MALOAIDBH != "")
                        lst = (from u in db.DOBAOHOes where u.MALOAIDBH == MALOAIDBH select u).ToList();
                    else if(TRANGTHAI != "")
                        lst = (from u in db.DOBAOHOes where u.TRANGTHAI == trangthai select u).ToList();
                    else if(MANHACC != "")
                        lst = (from u in db.DOBAOHOes where u.MANHACC == MANHACC select u).GroupBy(x => x.TENDOBAOHO).Select(x => x.FirstOrDefault()).ToList();
                    else lst = (from u in db.DOBAOHOes where u.MALOAIDBH == MALOAIDBH && u.TRANGTHAI == trangthai select u).ToList();
                }
                else if(TENDBH == "") 
                    lst = (from u in db.DOBAOHOes where  u.MADOBAOHO == MADOBAOHO select u).ToList();//getone
                else lst = (from u in db.DOBAOHOes where u.TENDOBAOHO == TENDBH select u).ToList();//getone
                return Support.ToDataTable<DOBAOHO>(lst);
            }
        }
        public void Add(string MADOBAOHO, bool TRANGTHAI, string TENDOBAOHO , string MALOAIDBH, string MANHACC )
        {
            using (QL_KVCEntities db = new QL_KVCEntities())
            {
                DOBAOHO DBH = new DOBAOHO { MADOBAOHO = MADOBAOHO, TRANGTHAI = TRANGTHAI, TENDOBAOHO = TENDOBAOHO , MALOAIDBH  = MALOAIDBH , MANHACC = MANHACC };
                db.DOBAOHOes.Add(DBH);
                db.SaveChanges();
            }
        }
        public void Update(string MADOBAOHO, bool TRANGTHAI, string TENDOBAOHO = "", string MALOAIDBH = "", string MANHACC = "")
        {
            using (QL_KVCEntities db = new QL_KVCEntities())
            {
                DOBAOHO DBH = db.DOBAOHOes.Find(MADOBAOHO);
                if(TENDOBAOHO != "")
                    DBH.TENDOBAOHO = TENDOBAOHO;
                if (MALOAIDBH != "")
                    DBH.MALOAIDBH = MALOAIDBH;
                if (MANHACC != "")
                    DBH.MANHACC = MANHACC;
                DBH.TRANGTHAI = TRANGTHAI;
                db.SaveChanges();
            }
        }
        public void Remove(string MADOBAOHO)
        {
            using (QL_KVCEntities db = new QL_KVCEntities())
            {
                DOBAOHO dbh = db.DOBAOHOes.Find(MADOBAOHO);
                db.DOBAOHOes.Remove(dbh);
                db.SaveChanges();
            }
        }
    }
}

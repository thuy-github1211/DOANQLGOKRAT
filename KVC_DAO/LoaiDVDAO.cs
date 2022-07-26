using KVC_DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KVC_DAO
{
    public class LoaiDVDAO
    {
        private static LoaiDVDAO call;
        public static LoaiDVDAO Call
        {
            get { if (call == null) call = new LoaiDVDAO(); return call; }
            set => call = value;
        }
        private LoaiDVDAO() { }
        public DataTable GetAllorOne(string MADV = "", string TENDV = "")
        {
            using (QL_KVCEntities db = new QL_KVCEntities())
            {
                List<DICHVUTHUEHLV> lst = new List<DICHVUTHUEHLV>();
                if (MADV == "" && TENDV == "")//getall
                {
                    lst = (from u in db.DICHVUTHUEHLVs select u).ToList();
                }
                else if (TENDV == "")
                    lst = (from u in db.DICHVUTHUEHLVs where u.MADV == MADV select u).ToList();//getone
                else if (MADV == "")
                    lst = (from u in db.DICHVUTHUEHLVs where u.TENDV == TENDV select u).ToList();//getone
                return Support.ToDataTable<DICHVUTHUEHLV>(lst);
            }
        }
    }
}

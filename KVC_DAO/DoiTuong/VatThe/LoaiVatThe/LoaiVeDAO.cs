using KVC_DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KVC_DAO
{
    public class LoaiVeDAO
    {
        private static LoaiVeDAO call;
        public static LoaiVeDAO Call
        {
            get { if (call == null) call = new LoaiVeDAO(); return call; }
            set => call = value;
        }
        private LoaiVeDAO() { }
        public DataTable GetAllorOne(string MALOAIVE = "", string TENLOAIVE = "")
        {
            using (QL_KVCEntities db = new QL_KVCEntities())
            {
                List<LOAIVE> lst = new List<LOAIVE>();
                if (MALOAIVE == "" && TENLOAIVE == "")//getall
                {
                    lst = (from u in db.LOAIVEs select u).ToList();
                }
                else if(TENLOAIVE == "")
                    lst = (from u in db.LOAIVEs where u.MALOAIVE == MALOAIVE select u).ToList();//getone
                else if (MALOAIVE == "")
                    lst = (from u in db.LOAIVEs where u.TENLOAIVE == TENLOAIVE select u).ToList();//getone
                return Support.ToDataTable<LOAIVE>(lst);
            }
        }
    }
}

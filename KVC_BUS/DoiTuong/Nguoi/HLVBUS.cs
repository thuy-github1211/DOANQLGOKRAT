using KVC_DAO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KVC_BUS
{
    public class HLVBUS
    {
        private static HLVBUS call;
        public static HLVBUS Call
        {
            get { if (call == null) call = new HLVBUS(); return call; }
            set => call = value;
        }
        private HLVBUS() { }
        public DataTable GetAllorOne(string MAHLV = "")
        {
            return HLVDAO.Call.GetAllorOne(MAHLV);
        }
        public void Add(string MAHLV, string TENHLV, string DIACHI, bool TRANGTHAI)
        {
            HLVDAO.Call.Add(MAHLV, TENHLV, DIACHI, TRANGTHAI);
        }
        public void Update(string MAHLV, bool TRANGTHAI, string TENHLV = "", string DIACHI = "")
        {
            HLVDAO.Call.Update(MAHLV, TRANGTHAI, TENHLV, DIACHI);
        }
    }
}

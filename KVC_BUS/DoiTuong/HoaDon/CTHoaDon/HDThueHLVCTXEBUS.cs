using KVC_DAO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KVC_BUS
{
    public class HDThueHLVCTXEBUS
    {
        private static HDThueHLVCTXEBUS call;
        public static HDThueHLVCTXEBUS Call
        {
            get { if (call == null) call = new HDThueHLVCTXEBUS(); return call; }
            set => call = value;
        }
        private HDThueHLVCTXEBUS() { }
        public DataTable GetAllorOne(string MAHD = "", string MAXE = "")
        {
            return HDThueHLVCTXEDAO.Call.GetAllorOne(MAHD, MAXE);
        }
        public void Add(string MAHD, string MAXE)
        {
            HDThueHLVCTXEDAO.Call.Add(MAHD, MAXE);
        }
    }
}

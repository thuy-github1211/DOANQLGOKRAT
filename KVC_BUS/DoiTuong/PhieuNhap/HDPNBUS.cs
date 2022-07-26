using KVC_DAO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KVC_BUS
{
    public class HDPNBUS
    {
        private static HDPNBUS call;
        public static HDPNBUS Call
        {
            get { if (call == null) call = new HDPNBUS(); return call; }
            set => call = value;
        }
        private HDPNBUS() { }
        public DataTable GetAllorOne(string MAPN = "")
        {
            return HDPNDAO.Call.GetAllorOne(MAPN);
        }
        public void Add(string MAPN, int LOAIPN, string MANV)
        {
            HDPNDAO.Call.Add(MAPN, LOAIPN, MANV);
        }
        public void Remove(string MAPN)
        {
            HDPNDAO.Call.Remove(MAPN);
        }
        public void Update(string MAPN, int LOAIPN = -1, string NGAYNHAP = "", string TONGTIEN = "", string MANV = "")
        {
            HDPNDAO.Call.Update(MAPN, LOAIPN, NGAYNHAP, TONGTIEN, MANV);
        }
    }
}

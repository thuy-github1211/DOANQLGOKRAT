using KVC_DAO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KVC_BUS
{
    public class BaoTriBUS
    {
        private static BaoTriBUS call;
        public static BaoTriBUS Call
        {
            get { if (call == null) call = new BaoTriBUS(); return call; }
            set => call = value;
        }
        private BaoTriBUS() { }
        public DataTable GetAllorOne(string MABAOTRI = "")
        {
            return BaoTriDAO.Call.GetAllorOne(MABAOTRI);
        }
        public void Add(string MABAOTRI, string MAXE, string MANV, int tg)
        {
            BaoTriDAO.Call.Add(MABAOTRI, MAXE, MANV, tg);
        }

        public void Update(string MABAOTRI, int tg)
        {
            BaoTriDAO.Call.Update(MABAOTRI, tg);
        }
        public void Remove(string MABAOTRI)
        {
            BaoTriDAO.Call.Remove(MABAOTRI);
        }
    }
}

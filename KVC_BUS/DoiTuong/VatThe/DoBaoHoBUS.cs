using KVC_DAO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KVC_BUS
{
    public class DoBaoHoBUS
    {
        private static DoBaoHoBUS call;
        public static DoBaoHoBUS Call
        {
            get { if (call == null) call = new DoBaoHoBUS(); return call; }
            set => call = value;
        }
        private DoBaoHoBUS() { }
        public DataTable GetAllorOne(string MADOBAOHO = "", string TENDBH = "", string TRANGTHAI = "", string MALOAIDBH = "", string MANHACC = "")
        {
            return DoBaoHoDAO.Call.GetAllorOne(MADOBAOHO, TENDBH, TRANGTHAI, MALOAIDBH, MANHACC);
        }
        public void Add(string MADOBAOHO, bool TRANGTHAI, string TENDOBAOHO, string MALOAIDBH, string MANHACC)
        {
            DoBaoHoDAO.Call.Add(MADOBAOHO, TRANGTHAI, TENDOBAOHO, MALOAIDBH, MANHACC);
        }
        public void Remove(string MADOBAOHO)
        {
            DoBaoHoDAO.Call.Remove(MADOBAOHO);
        }
        public void Update(string MADOBAOHO, bool TRANGTHAI, string TENDOBAOHO = "", string MALOAIDBH = "", string MANHACC = "")
        {
            DoBaoHoDAO.Call.Update(MADOBAOHO, TRANGTHAI, TENDOBAOHO, MALOAIDBH, MANHACC);
        }
    }
}

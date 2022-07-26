using KVC_DAO;
using KVC_DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KVC_BUS
{
    public class CTPhieuNhapDBHBUS
    {
        private static CTPhieuNhapDBHBUS call;
        public static CTPhieuNhapDBHBUS Call
        {
            get { if (call == null) call = new CTPhieuNhapDBHBUS(); return call; }
            set => call = value;
        }
        private CTPhieuNhapDBHBUS() { }
        public DataTable GetAllorOne(string MAPN = "", string MANCC = "", string TENDBH = "")
        {
            return CTPhieuNhapDBHDAO.Call.GetAllorOne(MAPN, MANCC, TENDBH);
        }
        public void Add(string MAPN, string MANCC, string TENDOBAOHO, int SOLUONG, double DONGIA)
        {
            CTPhieuNhapDBHDAO.Call.Add(MAPN, MANCC, TENDOBAOHO, SOLUONG, DONGIA);
        }
        public void Update(string MAPN, string MANCC, string TENDOBAOHO, int SOLUONG, double DONGIA)
        {
            CTPhieuNhapDBHDAO.Call.Update(MAPN, MANCC, TENDOBAOHO, SOLUONG, DONGIA);
        }
        public void Remove(string MAPN, string MANCC, string TENDOBAOHO)
        {
            CTPhieuNhapDBHDAO.Call.Remove(MAPN, MANCC, TENDOBAOHO);
        }
    }
}

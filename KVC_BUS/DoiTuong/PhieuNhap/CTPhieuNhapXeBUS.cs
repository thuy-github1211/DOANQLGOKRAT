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
    public class CTPhieuNhapXeBUS
    {
        private static CTPhieuNhapXeBUS call;
        public static CTPhieuNhapXeBUS Call
        {
            get { if (call == null) call = new CTPhieuNhapXeBUS(); return call; }
            set => call = value;
        }
        private CTPhieuNhapXeBUS() { }
        public DataTable GetAllorOne(string MAPN = "", string MANCC = "", string TENXE = "")
        {
            return CTPhieuNhapXeDAO.Call.GetAllorOne(MAPN, MANCC, TENXE);
        }
        public void Add(string MAPN, string MANCC, string TENXE, int SOLUONG, double DONGIA)
        {
            CTPhieuNhapXeDAO.Call.Add(MAPN, MANCC, TENXE, SOLUONG, DONGIA);
        }
        public void Update(string MAPN, string MANCC, string TENXE, int SOLUONG, double DONGIA)
        {
            CTPhieuNhapXeDAO.Call.Update(MAPN, MANCC, TENXE, SOLUONG, DONGIA);
        }
        public void Remove(string MAPN, string MANCC, string TENXE)
        {
            CTPhieuNhapXeDAO.Call.Remove(MAPN, MANCC, TENXE);
        }
    }
}

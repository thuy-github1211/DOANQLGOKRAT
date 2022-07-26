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
    public class CTPhieuNhapTTBBUS
    {
        private static CTPhieuNhapTTBBUS call;
        public static CTPhieuNhapTTBBUS Call
        {
            get { if (call == null) call = new CTPhieuNhapTTBBUS(); return call; }
            set => call = value;
        }
        private CTPhieuNhapTTBBUS() { }
        public DataTable GetAllorOne(string MAPN = "", string MANCC = "", string MATHIETBI = "")
        {
            return CTPhieuNhapTTBDAO.Call.GetAllorOne(MAPN, MANCC, MATHIETBI);
        }
        public void Add(string MAPN, string MANCC, string MATHIETBI, int SOLUONG, double DONGIA)
        {
            CTPhieuNhapTTBDAO.Call.Add(MAPN, MANCC, MATHIETBI, SOLUONG, DONGIA);
        }
        public void Update(string MAPN, string MANCC, string MATHIETBI, int SOLUONG, double DONGIA)
        {
            CTPhieuNhapTTBDAO.Call.Update(MAPN, MANCC, MATHIETBI, SOLUONG, DONGIA);
        }
        public void Remove(string MAPN, string MANCC, string MATHIETBI)
        {
            CTPhieuNhapTTBDAO.Call.Remove(MAPN, MANCC, MATHIETBI);
        }
    }
}

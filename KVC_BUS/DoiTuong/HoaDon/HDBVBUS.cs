using KVC_DAO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KVC_BUS
{
    public class HDBVBUS
    {
        private static HDBVBUS call;
        public static HDBVBUS Call
        {
            get { if (call == null) call = new HDBVBUS(); return call; }
            set => call = value;
        }
        private HDBVBUS() { }
        public DataTable GetAllorOne(string MAHD = "", string MANV = "", string MAKH = "")
        {
            return HoaDonBanVeDAO.Call.GetAllorOne(MAHD, MANV, MAKH);
        }
        public void Add(string MAHD, string MAKH, string MANV, DateTime INLUC, double TONGTIEN)
        {
            HoaDonBanVeDAO.Call.Add(MAHD, MAKH, MANV, INLUC, TONGTIEN);
        }
        public void Remove(string MAHD)
        {
            HoaDonBanVeDAO.Call.Remove(MAHD);
        }
        public void Update(string MAHD, string MAKH = "", string MANV = "", double TONGTIEN = 0)
        {
            HoaDonBanVeDAO.Call.Update(MAHD, MAKH, MANV, TONGTIEN);
        }
    }
}

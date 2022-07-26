using KVC_DAO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KVC_BUS
{
    public class CTHDBVBUS
    {
        private static CTHDBVBUS call;
        public static CTHDBVBUS Call
        {
            get { if (call == null) call = new CTHDBVBUS(); return call; }
            set => call = value;
        }
        private CTHDBVBUS() { }
        public DataTable GetAllorOne(string MACTHD = "", string MAHD = "")
        {
            return CTHDBVDAO.Call.GetAllorOne(MACTHD, MAHD);
        }
        public void Add(string MACTHD, string MAHD, string MAVE, string MAXE, double DONGIA)
        {
            CTHDBVDAO.Call.Add(MACTHD, MAHD, MAVE, MAXE, DONGIA);
        }
        public void Remove(string MACTHD)
        {
            CTHDBVDAO.Call.Remove(MACTHD);
        }
        //public void UpdateAccount(string id, string hinhanh, string password, string hoten, string diachi, string sdt, string gioitinh, DateTime ngaysinh, string email, string gioithieu, bool admin)
        //{
        //    AccountsDAO.Call.UpdateAccount(id, hinhanh, password, hoten, diachi, sdt, gioitinh, ngaysinh, email, gioithieu, admin);
        //}
    }
}

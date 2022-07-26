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
    public class TrangThietBiBUS
    {
        private static TrangThietBiBUS call;
        public static TrangThietBiBUS Call
        {
            get { if (call == null) call = new TrangThietBiBUS(); return call; }
            set => call = value;
        }
        private TrangThietBiBUS() { }
        public DataTable GetAllorOne(string MATHIETBI = "")
        {
            return TrangThietBiDAO.Call.GetAllorOne(MATHIETBI);
        }
        public void Add(string MATHIETBI, string TENTHIETBI, string MANCC,int soluong)
        {
            TrangThietBiDAO.Call.Add(MATHIETBI, TENTHIETBI, MANCC, soluong);
        }
        public void Remove(string MATHIETBI)
        {
            TrangThietBiDAO.Call.Remove(MATHIETBI);
        }
        public void Update(string MATHIETBI, int TONKHO, string TENTHIETBI = "", string MANCC = "")
        {
            TrangThietBiDAO.Call.Update(MATHIETBI, TONKHO, TENTHIETBI, MANCC);
        }
    }
}

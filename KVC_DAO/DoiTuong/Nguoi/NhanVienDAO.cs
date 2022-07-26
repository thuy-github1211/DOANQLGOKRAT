using KVC_DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KVC_DAO
{
    public class NhanVienDAO
    {
        private static NhanVienDAO call;
        public static NhanVienDAO Call
        {
            get { if (call == null) call = new NhanVienDAO(); return call; }
            set => call = value;
        }
        private NhanVienDAO() { }
        public DataTable GetAllorOne(string MANV = "")
        {
            using (QL_KVCEntities db = new QL_KVCEntities())
            {
                List<NHANVIEN> lst = new List<NHANVIEN>();
                if (MANV == "")//getall
                {
                    lst = (from u in db.NHANVIENs select u).ToList();
                }
                else lst = (from u in db.NHANVIENs where u.MANV == MANV select u).ToList();//getone
                return Support.ToDataTable<NHANVIEN>(lst);
            }
        }
        public void Add(string MANV, string TENNV, string GIOITINH, string MALOAI, string DIACHI)
        {
            using (QL_KVCEntities db = new QL_KVCEntities())
            {
                NHANVIEN NHANVIEN = new NHANVIEN { MANV = MANV, TENNV = TENNV, GIOITINH = GIOITINH, MALOAI = MALOAI, DIACHI = DIACHI };
                db.NHANVIENs.Add(NHANVIEN);
                db.SaveChanges();
            }
        }
        public void Update(string MANV, string TENNV, string GIOITINH, string MALOAI, string DIACHI)
        {
            using (QL_KVCEntities db = new QL_KVCEntities())
            {
                NHANVIEN NHANVIEN = db.NHANVIENs.Find(MANV);
                NHANVIEN.TENNV = TENNV;
                NHANVIEN.GIOITINH = GIOITINH;
                NHANVIEN.MALOAI = MALOAI;
                NHANVIEN.DIACHI = DIACHI;
                db.SaveChanges();
            }
        }
    }
}
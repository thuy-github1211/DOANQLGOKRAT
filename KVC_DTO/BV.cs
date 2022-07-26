using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KVC_DTO
{
    public partial class BV
    {
        string sanpham;
        int soluong;
        double dongia;
        double thanhtien;

        public string Sanpham { get => sanpham; set => sanpham = value; }
        public int Soluong { get => soluong; set => soluong = value; }
        public double Dongia { get => dongia; set => dongia = value; }
        public double Thanhtien { get => thanhtien; set => thanhtien = value; }
    }
}

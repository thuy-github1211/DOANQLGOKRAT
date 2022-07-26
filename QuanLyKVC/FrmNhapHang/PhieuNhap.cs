using DevExpress.XtraEditors;
using KVC_BUS;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyKVC
{
    public partial class PhieuNhap : DevExpress.XtraEditors.XtraForm
    {
        DataRow nhanvien;
        Main main;
        public PhieuNhap()
        {
            InitializeComponent();
        }
        public PhieuNhap(Main main,DataRow nhanvien) : this()
        {
            this.nhanvien = nhanvien;
            this.main = main;
        }
        private void NhapXe_Load(object sender, EventArgs e)
        {
            gcPhieuNhap.DataSource = HDPNBUS.Call.GetAllorOne();
        }
        private void NhapPhieu(int loaipn)
        {
            string IdLast = "0";
            if(HDPNBUS.Call.GetAllorOne().Rows.Count > 0)
            {
                DataRow PN = HDPNBUS.Call.GetAllorOne().Rows[HDPNBUS.Call.GetAllorOne().Rows.Count - 1];
                IdLast = PN["MAPN"].ToString();
            }    
            string mapn = Help.AutoIncreaseID.IncreaseID("PN", IdLast, 3);
            HDPNBUS.Call.Add(mapn, loaipn, nhanvien["MANV"].ToString());
            gcPhieuNhap.DataSource = HDPNBUS.Call.GetAllorOne();
            if (loaipn == 0)
            {

                main.callCTPNX(mapn);
                this.Hide();
            }else if (loaipn == 1)
            {
                main.callCTPNTTB(mapn);
                this.Hide();
            }
            else
            {
                main.callCTPNDBH(mapn);
                this.Hide();
            }

        }
        private void barButtonItem8_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            NhapPhieu(0);
        }

        private void barButtonItem9_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            NhapPhieu(1);
        }

        private void barButtonItem10_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            NhapPhieu(2);
        }

        private void gvPhieuNhap_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            if (e.Column.FieldName == "LOAIPN")
            {
                if (e.CellValue.ToString() == "0")
                    e.DisplayText = "Xe";
                if (e.CellValue.ToString() == "1")
                    e.DisplayText = "Trang thiết bị";
                if (e.CellValue.ToString() == "2")
                    e.DisplayText = "Đồ bảo hộ";
            }    
                

        }

        private void barButtonItem11_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if(gvPhieuNhap.RowCount > 0)
                if(gcPhieuNhap.IsFocused)
                {
                    int choose = int.Parse(gvPhieuNhap.GetRowCellValue(gvPhieuNhap.FocusedRowHandle, "LOAIPN").ToString());
                    if (choose == 2)
                    {
                        main.callCTPNDBH(gvPhieuNhap.GetRowCellValue(gvPhieuNhap.FocusedRowHandle, "MAPN").ToString());
                        this.Hide();
                    }
                    if (choose == 0)
                    {
                        main.callCTPNX(gvPhieuNhap.GetRowCellValue(gvPhieuNhap.FocusedRowHandle, "MAPN").ToString());
                        this.Hide();
                    }
                }    
        }
    }
}
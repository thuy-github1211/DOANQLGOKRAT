using DevExpress.XtraEditors;
using DevExpress.XtraReports.UI;
using KVC_BUS;
using QuanLyKVC.report;
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
    public partial class CTPhieuNhapTTB : DevExpress.XtraEditors.XtraForm
    {
        bool NK;
        Main main;
        DataRow account;
        string mapn;
        public CTPhieuNhapTTB()
        {
            InitializeComponent();
        }
        public CTPhieuNhapTTB(Main main, DataRow account, string mapn) : this()
        {
            this.main = main;
            this.account = account;
            this.mapn = mapn;
        }
        public void load()
        {
            gcCTPhieuNhapTTB.DataSource = CTPhieuNhapTTBBUS.Call.GetAllorOne(mapn);
        }
        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Enabled = false;
            AddPhieuNhapTTB f = new AddPhieuNhapTTB(this,mapn);
            f.Show();
        }

        private void CTPhieuNhapTTB_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!NK)
            {
                if (XtraMessageBox.Show("Bạn có muốn xóa phiếu nhập không?", "Thông Báo", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    if (gvCTPhieuNhapTTB.RowCount > 0)
                    {
                        foreach (DataRow item in CTPhieuNhapTTBBUS.Call.GetAllorOne(mapn).Rows)
                        {
                            CTPhieuNhapTTBBUS.Call.Remove(mapn, item["MANCC"].ToString(), item["MATHIETBI"].ToString());
                        }
                    }
                    HDPNBUS.Call.Remove(mapn);
                    main.callPN(account);
                }
                else e.Cancel = true;
            }
        }

        private void barButtonItem3_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gvCTPhieuNhapTTB.RowCount > 0)
            {
                if (gcCTPhieuNhapTTB.IsFocused == true)
                {
                    if (XtraMessageBox.Show("Bạn có muốn xóa không?", "Thông Báo", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                    {
                        try
                        {
                            CTPhieuNhapTTBBUS.Call.Remove(mapn, gvCTPhieuNhapTTB.GetFocusedRowCellValue(bandedGridColumn1).ToString(), gvCTPhieuNhapTTB.GetFocusedRowCellValue(bandedGridColumn2).ToString());
                            double tongtien = double.Parse(HDPNBUS.Call.GetAllorOne(mapn).Rows[0]["TONGTIEN"].ToString());
                            double thanhtien = int.Parse(gvCTPhieuNhapTTB.GetFocusedRowCellValue(bandedGridColumn4).ToString()) * double.Parse(gvCTPhieuNhapTTB.GetFocusedRowCellValue(bandedGridColumn3).ToString());
                            HDPNBUS.Call.Update(mapn, -1, "", (tongtien - thanhtien).ToString(), "");
                            gcCTPhieuNhapTTB.DataSource = CTPhieuNhapTTBBUS.Call.GetAllorOne();
                        }
                        catch
                        {
                            XtraMessageBox.Show("Không thể xóa.");
                        }
                    }
                    else gcCTPhieuNhapTTB.Focus();
                }
                else XtraMessageBox.Show("Vui lòng chọn hàng hóa muốn xóa!", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void barButtonItem4_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gvCTPhieuNhapTTB.RowCount > 0)
            {
                foreach (DataRow item in CTPhieuNhapTTBBUS.Call.GetAllorOne(mapn).Rows)
                {
                        int tonkho = int.Parse(TrangThietBiBUS.Call.GetAllorOne(item["MATHIETBI"].ToString()).Rows[0]["TONKHO"].ToString());
                        TrangThietBiBUS.Call.Update(item["MATHIETBI"].ToString(), (tonkho + int.Parse(item["SOLUONG"].ToString())));
                }
                DataRow hd = HDPNBUS.Call.GetAllorOne(mapn).Rows[0];
                var rp = new HoaDonNhapTTB();
                rp.DataSource = CTPhieuNhapTTBBUS.Call.GetAllorOne(mapn);
                rp.DataMember = "";
                rp.QL.Value = NhanVienBUS.Call.GetAllorOne(hd["MANV"].ToString()).Rows[0]["TENNV"];
                rp.PN.Value = mapn;
                rp.NL.Value = hd["NGAYNHAP"].ToString();
                rp.TT.Value = hd["TONGTIEN"].ToString();
                rp.ShowPreviewDialog();
                NK = true;
                main.callPN(account);
                this.Close();
            }
            else
            {
                XtraMessageBox.Show("Chưa có dữ liệu!", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }
        }
    }
}
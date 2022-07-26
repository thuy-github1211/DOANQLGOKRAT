using DevExpress.XtraEditors;
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
using DevExpress.XtraReports.UI;
using System.Windows.Forms;
using KVC_DTO;

namespace QuanLyKVC
{
    public partial class BanVe : DevExpress.XtraEditors.XtraForm
    {
        bool TT;
        Main frm;
        double tongtien = 0;
        string mahd;
        public double Tongtien { get => tongtien; set => tongtien = value; }

        public BanVe()
        {
            InitializeComponent();
        }
        public BanVe(Main main, string mahd) : this()
        {
            this.mahd = mahd;
            frm = main;
        }
        public void load()
        {
            gcCTHDBV.DataSource = CTHDBVBUS.Call.GetAllorOne("", mahd);
            gcDBH.DataSource = CTHDDBHBUS.Call.GetAllorOne(mahd);
            tbxTT.Text = tongtien.ToString() + " VNĐ";
        }
        private Form KiemTraTonTai(Type fType)
        {
            foreach (Form f in MdiChildren)
            {
                if (f.GetType() == fType) //Neu Form duoc truyen vao da duoc mo
                    return f;
            }
            return null;
        }
        private void btnThemVe_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Enabled = false;
            Form frm = this.KiemTraTonTai(typeof(AddVe));
            if (frm != null)
                frm.Activate();
            else
            {
                AddVe f = new AddVe(this, mahd);
                f.Show();
            }
        }
        private void btnXoaVe_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gvCTHDBV.RowCount > 0)
            {
                if (gcCTHDBV.IsFocused == true)
                {
                    if (XtraMessageBox.Show("Bạn có muốn xóa không?", "Thông Báo", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                    {
                        try
                        {
                            CTHDBVBUS.Call.Remove(gvCTHDBV.GetFocusedRowCellValue(colMaCTHDBV).ToString());
                            XeBUS.Call.Update(gvCTHDBV.GetFocusedRowCellValue(colMAXE).ToString(), true, "", "", "");
                            HoatDongBUS.Call.Remove(gvCTHDBV.GetFocusedRowCellValue(colMAXE).ToString());
                            tongtien -= double.Parse(gvCTHDBV.GetFocusedRowCellValue(colDONGIA1).ToString());
                            tbxTT.Text = tongtien.ToString() + "VNĐ";
                            gcCTHDBV.DataSource = CTHDBVBUS.Call.GetAllorOne("", mahd);
                        }
                        catch
                        {
                            XtraMessageBox.Show("Không thể xóa.");
                        }
                    }
                    else gcCTHDBV.Focus();
                }
                else XtraMessageBox.Show("Vui lòng chọn vé muốn xóa!", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void DelAllGridVe()
        {
            if (gvCTHDBV.RowCount > 0)
            {
                if (XtraMessageBox.Show("Bạn có muốn xóa không?", "Thông Báo", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    try
                    {
                        foreach (DataRow item in CTHDBVBUS.Call.GetAllorOne("", mahd).Rows)
                        {
                            CTHDBVBUS.Call.Remove(item["MACTHD"].ToString());
                            XeBUS.Call.Update(item["MAXE"].ToString(), true, "", "", "");
                            HoatDongBUS.Call.Remove(item["MAXE"].ToString());
                            tongtien -= double.Parse(item["DONGIA"].ToString());
                        }
                        tbxTT.Text = tongtien.ToString() + "VNĐ";
                        gcCTHDBV.DataSource = CTHDBVBUS.Call.GetAllorOne("", mahd);
                    }
                    catch
                    {
                        XtraMessageBox.Show("Không thể xóa.");
                    }
                }
                else gcCTHDBV.Focus();
            }
        }

        private void btnXoaAllVe_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            DelAllGridVe();
        }

        private void btnThemDBH_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Enabled = false;
            Form frm = this.KiemTraTonTai(typeof(AddDBH));
            if (frm != null)
                frm.Activate();
            else
            {
                AddDBH f = new AddDBH(this, mahd);
                f.Show();
            }
        }

        private void btnXoaDBH_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gvDBH.RowCount > 0)
            {
                if (gcDBH.IsFocused == true)
                {
                    gvDBH.GetFocusedRowCellValue(colMALOAIDBH).ToString();
                    if (XtraMessageBox.Show("Bạn có muốn xóa không?", "Thông Báo", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                    {
                        try
                        {
                            CTHDDBHBUS.Call.Remove(mahd, gvDBH.GetFocusedRowCellValue(colTENDOBAOHO).ToString());
                            DoBaoHoBUS.Call.Update(gvDBH.GetFocusedRowCellValue(colTENDOBAOHO).ToString(), true, "", "", "");
                            tongtien -= double.Parse(gvDBH.GetFocusedRowCellValue(colDONGIA).ToString());
                            tbxTT.Text = tongtien.ToString() + "VNĐ";
                            gcDBH.DataSource = CTHDDBHBUS.Call.GetAllorOne(mahd, "");
                        }
                        catch
                        {
                            XtraMessageBox.Show("Không thể xóa.");
                        }
                    }
                    else gcCTHDBV.Focus();
                }
                else XtraMessageBox.Show("Vui lòng chọn đồ bảo hộ muốn xóa!", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void DelAllGridDBH()
        {
            if (gvDBH.RowCount > 0)
            {
                gvDBH.GetFocusedRowCellValue(colMALOAIDBH).ToString();
                if (XtraMessageBox.Show("Bạn có muốn xóa không?", "Thông Báo", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    try
                    {
                        foreach (DataRow item in CTHDDBHBUS.Call.GetAllorOne(mahd, "").Rows)
                        {
                            CTHDDBHBUS.Call.Remove(mahd, item["MADBH"].ToString());
                            DoBaoHoBUS.Call.Update(item["MADBH"].ToString(), true, "", "", "");
                            tongtien -= double.Parse(item["DONGIA"].ToString());
                        }
                        tbxTT.Text = tongtien.ToString() + "VNĐ";
                        gcDBH.DataSource = CTHDDBHBUS.Call.GetAllorOne(mahd, "");
                    }
                    catch
                    {
                        XtraMessageBox.Show("Không thể xóa.");
                    }
                }
                else gcCTHDBV.Focus();
            }
        }
        private void btnXoaAllDBH_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            DelAllGridDBH();
        }
        public List<BV> getslve(string mahd)
        {
            bool next = false;
            List<BV> ve = new List<BV>();
            foreach (DataRow item in CTHDBVBUS.Call.GetAllorOne("", mahd).Rows)
            {
                foreach (var item1 in ve)
                {
                    string nameve = LoaiVeBUS.Call.GetAllorOne(item["MALOAIVE"].ToString()).Rows[0]["TENLOAIVE"].ToString();
                    if (nameve == item1.Sanpham)
                    {
                        next = true;
                        break;
                    }
                    next = false;
                }
                if (!next)
                {
                    BV n = new BV();
                    n.Sanpham = LoaiVeBUS.Call.GetAllorOne(item["MALOAIVE"].ToString()).Rows[0]["TENLOAIVE"].ToString();
                    n.Soluong = 1;
                    n.Dongia = double.Parse(item["DONGIA"].ToString());
                    n.Thanhtien = n.Soluong * n.Dongia;
                    ve.Add(n);
                }
                else
                {
                    foreach (var item1 in ve)
                    {
                        if (LoaiVeBUS.Call.GetAllorOne(item["MALOAIVE"].ToString()).Rows[0]["TENLOAIVE"].ToString() == item1.Sanpham)
                        {
                            item1.Soluong += 1;
                            item1.Thanhtien = item1.Soluong * item1.Dongia;
                        }
                    }
                }
            }
            foreach (DataRow item in CTHDDBHBUS.Call.GetAllorOne(mahd).Rows)
            {
                foreach (var item1 in ve)
                {
                    string namedbh = LoaiDBHBUS.Call.GetAllorOne(item["MALOAIDBH"].ToString()).Rows[0]["TENLOAIDBH"].ToString();
                    if (namedbh == item1.Sanpham)
                    {
                        next = true;
                        break;
                    }
                    next = false;
                }
                if (!next)
                {
                    BV n = new BV();
                    n.Sanpham = LoaiDBHBUS.Call.GetAllorOne(item["MALOAIDBH"].ToString()).Rows[0]["TENLOAIDBH"].ToString();
                    n.Soluong = 1;
                    n.Dongia = double.Parse(item["DONGIA"].ToString());
                    n.Thanhtien = n.Soluong * n.Dongia;
                    ve.Add(n);
                }
                else
                {
                    foreach (var item1 in ve)
                    {
                        if (LoaiDBHBUS.Call.GetAllorOne(item["MALOAIDBH"].ToString()).Rows[0]["TENLOAIDBH"].ToString() == item1.Sanpham)
                        {
                            item1.Soluong += 1;
                            item1.Thanhtien = item1.Soluong * item1.Dongia;
                        }
                    }
                }
            }
            return ve;
        }
        private void btnTT_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (tongtien > 0)
            {
                HDBVBUS.Call.Update(mahd, "", "", tongtien);
                DataRow hd = HDBVBUS.Call.GetAllorOne(mahd).Rows[0];
                var rp = new HoaDonBanVe();
                rp.DataSource = getslve(mahd);
                rp.DataMember = "";
                rp.lbNV.Value = NhanVienBUS.Call.GetAllorOne(hd["MANV"].ToString()).Rows[0]["TENNV"];
                rp.lbKH.Value = KhachHangBUS.Call.GetAllorOne(hd["MAKH"].ToString()).Rows[0]["TENKH"];
                rp.lbNL.Value = hd["NGAYIN"].ToString();
                rp.lbTT.Value = hd["TONGTIEN"].ToString();
                rp.ShowPreviewDialog();
                frm.callKH();
                TT = true;
                this.Close();
            }
            else
            {
                if (XtraMessageBox.Show("Bạn chưa thêm chi tiết, bạn có muốn xóa hóa đơn không?", "Thông Báo", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    this.Close();
                }
            }
        }

        private void gvCTHDBV_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            if (e.Column.FieldName == "MALOAIVE")
            {
                e.DisplayText = LoaiVeBUS.Call.GetAllorOne(e.CellValue.ToString(), "").Rows[0]["TENLOAIVE"].ToString();
            }
        }

        private void BanVe_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!TT)
            {
                if (XtraMessageBox.Show("Bạn có muốn xóa hóa đơn không?", "Thông Báo", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    if (!(gvDBH.RowCount > 0) && !(gvCTHDBV.RowCount > 0))
                    {
                        HDBVBUS.Call.Remove(mahd);
                        frm.callKH();
                    }
                    else
                    {
                        XtraMessageBox.Show("Vui lòng xóa hết dữ liệu của hai bảng trước!", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        e.Cancel = true;
                    }
                }
                else e.Cancel = true;
            }
        }

        private void gvDBH_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            if (e.Column.FieldName == "MALOAIDBH")
            {
                e.DisplayText = LoaiDBHBUS.Call.GetAllorOne(e.CellValue.ToString()).Rows[0]["TENLOAIDBH"].ToString();
            }
        }
    }
}
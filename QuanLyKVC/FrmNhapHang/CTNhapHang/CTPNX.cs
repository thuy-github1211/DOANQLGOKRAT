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
    public partial class CTPNX : DevExpress.XtraEditors.XtraForm
    {
        bool NK;
        Main main;
        DataRow account;
        string mapn;
        public CTPNX()
        {
            InitializeComponent();
        }
        public CTPNX(Main main, DataRow account, string mapn) : this()
        {
            this.main = main;
            this.account = account;
            this.mapn = mapn;
        }
        public void load()
        {
            gcCTNhapXe.DataSource = CTPhieuNhapXeBUS.Call.GetAllorOne(mapn);
        }
        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if(lookUpEdit1.EditValue != null)
            {
                this.Enabled = false;
                AddPNX f = new AddPNX(this, mapn, lookUpEdit1.Text);
                f.Show();
            }    
        }

        private void CTPNX_FormClosing(object sender, FormClosingEventArgs e)
        {
            bool cancel = false;
            if(!NK)
            {
                if (XtraMessageBox.Show("Bạn có muốn xóa phiếu nhập không?", "Thông Báo", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    if (gvCTNhapXe.RowCount > 0)
                    {
                        foreach (DataRow item in CTPhieuNhapXeBUS.Call.GetAllorOne(mapn).Rows)
                        {
                            CTPhieuNhapXeBUS.Call.Remove(mapn, item["MANCC"].ToString(), item["TENXE"].ToString());
                        }
                    }
                    HDPNBUS.Call.Remove(mapn);
                }
                else
                {
                    e.Cancel = true;
                    cancel = true;
                }
            }
            if(!cancel)
                main.callPN(account);
        }

        private void barButtonItem3_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gvCTNhapXe.RowCount > 0)
            {
                if (gcCTNhapXe.IsFocused == true)
                {
                    if (XtraMessageBox.Show("Bạn có muốn xóa không?", "Thông Báo", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                    {
                        try
                        {
                            CTPhieuNhapXeBUS.Call.Remove(mapn, gvCTNhapXe.GetFocusedRowCellValue(bandedGridColumn1).ToString(), gvCTNhapXe.GetFocusedRowCellValue(bandedGridColumn2).ToString());
                            double tongtien = double.Parse(HDPNBUS.Call.GetAllorOne(mapn).Rows[0]["TONGTIEN"].ToString());
                            double thanhtien = int.Parse(gvCTNhapXe.GetFocusedRowCellValue(bandedGridColumn4).ToString()) * double.Parse(gvCTNhapXe.GetFocusedRowCellValue(bandedGridColumn3).ToString());
                            HDPNBUS.Call.Update(mapn, -1, "", (tongtien - thanhtien).ToString(), "");
                            gcCTNhapXe.DataSource = CTPhieuNhapXeBUS.Call.GetAllorOne();
                        }
                        catch
                        {
                            XtraMessageBox.Show("Không thể xóa.");
                        }
                    }
                    else gcCTNhapXe.Focus();
                }
                else XtraMessageBox.Show("Vui lòng chọn hàng hóa muốn xóa!", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void barButtonItem4_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gvCTNhapXe.RowCount > 0)
            {
                foreach (DataRow item in CTPhieuNhapXeBUS.Call.GetAllorOne(mapn).Rows)
                {
                    for (int i = 0; i < int.Parse(item["SOLUONG"].ToString()); i++)
                    {
                        string IdLast = "0";
                        if (XeBUS.Call.GetAllorOne().Rows.Count > 0)
                        {
                            DataRow allxe = XeBUS.Call.GetAllorOne().Rows[XeBUS.Call.GetAllorOne().Rows.Count - 1];
                            IdLast = allxe["MAXE"].ToString();
                        }
                        string maxe = Help.AutoIncreaseID.IncreaseID("XE", IdLast, 3);
                        XeBUS.Call.Add(maxe, item["TENXE"].ToString(), item["MANCC"].ToString());
                    }
                }
                DataRow hd = HDPNBUS.Call.GetAllorOne(mapn).Rows[0];
                var rp = new HoaDonNhapXe();
                rp.DataSource = CTPhieuNhapXeBUS.Call.GetAllorOne(mapn);
                rp.DataMember = "";
                rp.QL.Value = NhanVienBUS.Call.GetAllorOne(hd["MANV"].ToString()).Rows[0]["TENNV"];
                rp.PN.Value = mapn;
                rp.NL.Value = hd["NGAYNHAP"].ToString();
                rp.TT.Value = hd["TONGTIEN"].ToString();
                rp.ShowPreviewDialog();
                NK = true;
                this.Close();
            }
            else
            {
                XtraMessageBox.Show("Chưa có dữ liệu!", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }
        }

        private void CTPNX_Load(object sender, EventArgs e)
        {
            if (CTPhieuNhapXeBUS.Call.GetAllorOne(mapn).Rows.Count > 0)
            {
                barButtonItem1.Enabled = true;
                barButtonItem3.Enabled = true;
                barButtonItem4.Enabled = true;
                bar2.Visible = false;
                layoutControlItem2.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                gcCTNhapXe.DataSource = CTPhieuNhapXeBUS.Call.GetAllorOne(mapn);
                gcCTNhapXe.Enabled = true;
                NK = true;
            }else
            {
                DataTable ncc = new DataTable();
                ncc.Columns.Add("Nhà cung cấp");
                foreach (DataRow item in NhaCungCapBUS.Call.GetAllorOne().Rows)
                {
                    DataRow itemn = ncc.NewRow();
                    itemn[0] = item["TENNCC"];
                    ncc.Rows.Add(itemn);
                }
                lookUpEdit1.Properties.DataSource = ncc;
                lookUpEdit1.Properties.DisplayMember = "Nhà cung cấp";
            }    
            
        }

        private void lookUpEdit1_EditValueChanged(object sender, EventArgs e)
        {
            if(lookUpEdit1.EditValue != null)
            {
                lookUpEdit1.Enabled = false;
                gcCTNhapXe.Enabled = true;
                barButtonItem1.Enabled = true;
                barButtonItem3.Enabled = true;
                barButtonItem4.Enabled = true;
            }
        }
    }
}
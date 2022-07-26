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
    public partial class CTPhieuNhapDBH : DevExpress.XtraEditors.XtraForm
    {
        public string NCC = "";
        bool NK;
        Main main;
        DataRow account;
        string mapn;
        public CTPhieuNhapDBH()
        {
            InitializeComponent();
        }
        public CTPhieuNhapDBH(Main main, DataRow account, string mapn) : this()
        {
            this.main = main;
            this.account = account;
            this.mapn = mapn;
        }
        public void load()
        {
            gcCTPhieuNhapDBH.DataSource = CTPhieuNhapDBHBUS.Call.GetAllorOne(mapn);
        }
        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if(NCC == "")
            {
                AddPhieuNhapDBH f = new AddPhieuNhapDBH(this, mapn);
                f.ShowDialog();
            }    
            else
            {
                AddPhieuNhapDBH f = new AddPhieuNhapDBH(this, mapn, NCC);
                f.ShowDialog();
            }
        }

        private void CTPhieuNhapDBH_FormClosing(object sender, FormClosingEventArgs e)
        {
            bool cancel = false;
            if (!NK)
            {
                if (XtraMessageBox.Show("Bạn có muốn xóa phiếu nhập không?", "Thông Báo", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    if (gvCTPhieuNhapDBH.RowCount > 0)
                    {
                        foreach (DataRow item in CTPhieuNhapDBHBUS.Call.GetAllorOne(mapn).Rows)
                        {
                            CTPhieuNhapDBHBUS.Call.Remove(mapn, item["MANCC"].ToString(), item["TENDBH"].ToString());
                        }
                    }
                    HDPNBUS.Call.Remove(mapn);
                }
                else e.Cancel = true;
            }
            if (!cancel)
                main.callPN(account);
        }

        private void barButtonItem3_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gvCTPhieuNhapDBH.RowCount > 0)
            {
                if (gcCTPhieuNhapDBH.IsFocused == true)
                {
                    if (XtraMessageBox.Show("Bạn có muốn xóa không?", "Thông Báo", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                    {
                        try
                        {
                            CTPhieuNhapDBHBUS.Call.Remove(mapn, gvCTPhieuNhapDBH.GetFocusedRowCellValue(bandedGridColumn1).ToString(), gvCTPhieuNhapDBH.GetFocusedRowCellValue(bandedGridColumn2).ToString());
                            double tongtien = double.Parse(HDPNBUS.Call.GetAllorOne(mapn).Rows[0]["TONGTIEN"].ToString());
                            double thanhtien = int.Parse(gvCTPhieuNhapDBH.GetFocusedRowCellValue(bandedGridColumn4).ToString()) * double.Parse(gvCTPhieuNhapDBH.GetFocusedRowCellValue(bandedGridColumn3).ToString());
                            HDPNBUS.Call.Update(mapn, -1, "", (tongtien - thanhtien).ToString(), "");
                            gcCTPhieuNhapDBH.DataSource = CTPhieuNhapDBHBUS.Call.GetAllorOne(mapn);
                            if(gvCTPhieuNhapDBH.RowCount == 0)
                            {
                                NCC = "";
                            }    
                        }
                        catch
                        {
                            XtraMessageBox.Show("Không thể xóa.");
                        }
                    }
                    else gcCTPhieuNhapDBH.Focus();
                }
                else XtraMessageBox.Show("Vui lòng chọn hàng hóa muốn xóa!", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void barButtonItem4_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gvCTPhieuNhapDBH.RowCount > 0)
            {
                foreach (DataRow item in CTPhieuNhapDBHBUS.Call.GetAllorOne(mapn).Rows)
                {
                    for (int i = 0; i < int.Parse(item["SOLUONG"].ToString()); i++)
                    {
                        string IdLast = "0";
                        if (DoBaoHoBUS.Call.GetAllorOne().Rows.Count > 0)
                        {
                            DataRow allxe = DoBaoHoBUS.Call.GetAllorOne().Rows[DoBaoHoBUS.Call.GetAllorOne().Rows.Count - 1];
                            IdLast = allxe["MADOBAOHO"].ToString();
                        }
                        string madbh = Help.AutoIncreaseID.IncreaseID("BH", IdLast, 3);
                        DoBaoHoBUS.Call.Add(madbh,true, item["TENDBH"].ToString(), DoBaoHoBUS.Call.GetAllorOne("", item["TENDBH"].ToString()).Rows[0]["MALOAIDBH"].ToString(), item["MANCC"].ToString());
                    }
                }
                DataRow hd = HDPNBUS.Call.GetAllorOne(mapn).Rows[0];
                var rp = new HoaDonNhapDBH();
                rp.DataSource = CTPhieuNhapDBHBUS.Call.GetAllorOne(mapn);
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

        private void CTPhieuNhapDBH_Load(object sender, EventArgs e)
        {
            if (CTPhieuNhapDBHBUS.Call.GetAllorOne(mapn).Rows.Count > 0)
            {
                bar2.Visible = false;
                gcCTPhieuNhapDBH.DataSource = CTPhieuNhapDBHBUS.Call.GetAllorOne(mapn);
                NK = true;
            }
        }
    }
}
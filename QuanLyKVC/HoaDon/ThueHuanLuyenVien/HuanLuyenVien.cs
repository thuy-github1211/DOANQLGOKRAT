using DevExpress.XtraEditors;
using DevExpress.XtraReports.UI;
using KVC_BUS;
using KVC_DTO;
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
    public partial class HuanLuyenVien : DevExpress.XtraEditors.XtraForm
    {
        Main frm;
        DataRow account;
        DataRow khachHang;
        public HuanLuyenVien()
        {
            InitializeComponent();
        }
        public HuanLuyenVien(Main frm, DataRow account, string makh) : this()
        {
            this.frm = frm;
            this.account = account;
            khachHang = KhachHangBUS.Call.GetAllorOne(makh).Rows[0];
        }
        private void HuanLuyenVien_Load(object sender, EventArgs e)
        {
            gcHLV.DataSource = HLVBUS.Call.GetAllorOne();
        }

        private void HuanLuyenVien_FormClosing(object sender, FormClosingEventArgs e)
        {
            frm.callKH();
        }

        private void barButtonItem3_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gvHLV.RowCount > 0)
            {
                if (gcHLV.IsFocused == true)
                {
                    if (Convert.ToBoolean(gvHLV.GetFocusedRowCellValue("TRANGTHAI").ToString()))
                    {
                        if (XeBUS.Call.GetAllorOne("", "", "1").Rows.Count > 0)
                        {
                            string IdLast = "0";
                            if (HDThueHLVBUS.Call.GetAllorOne().Rows.Count > 0)
                            {
                                DataRow HDThueHLV = HDThueHLVBUS.Call.GetAllorOne().Rows[HDThueHLVBUS.Call.GetAllorOne().Rows.Count - 1];
                                IdLast = HDThueHLV["MAHD"].ToString();
                            }
                            string mahd = Help.AutoIncreaseID.IncreaseID("HDHLV", IdLast, 3);
                            HDThueHLVBUS.Call.Add(mahd, DateTime.Now, account["MANV"].ToString(), khachHang["MAKH"].ToString(), gvHLV.GetFocusedRowCellValue("MAHLV").ToString(), "MDV001");
                            string maxe = XeBUS.Call.GetAllorOne("", "", "1").Rows[0]["MAXE"].ToString();
                            HDThueHLVCTXEBUS.Call.Add(mahd, maxe);
                            XeBUS.Call.Update(maxe, false);
                            HLVBUS.Call.Update(gvHLV.GetFocusedRowCellValue("MAHLV").ToString(), false);
                            HoatDongBUS.Call.Add(maxe, khachHang["MAKH"].ToString(), TimeSpan.Parse("1:00:00"), false, gvHLV.GetFocusedRowCellValue("MAHLV").ToString());
                            DataRow hdhlv = HDThueHLVBUS.Call.GetAllorOne(mahd).Rows[0];
                            var rp = new HoaDonThueHLV();
                            rp.DataMember = "";
                            rp.NV.Value = NhanVienBUS.Call.GetAllorOne(hdhlv["MANV"].ToString()).Rows[0]["TENNV"];
                            rp.KH.Value = KhachHangBUS.Call.GetAllorOne(hdhlv["MAKH"].ToString()).Rows[0]["TENKH"];
                            rp.DV.Value = LoaiDVBUS.Call.GetAllorOne(hdhlv["MADV"].ToString()).Rows[0]["TENDV"];
                            rp.HLV.Value = HLVBUS.Call.GetAllorOne(hdhlv["MAHLV"].ToString()).Rows[0]["TENHLV"];
                            rp.NL.Value = hdhlv["NGAYLAP"].ToString();
                            rp.TT.Value = LoaiDVBUS.Call.GetAllorOne(hdhlv["MADV"].ToString()).Rows[0]["DONGIA"];
                            List<rpHLV> hvl = new List<rpHLV>();
                            foreach (DataRow item in HDThueHLVCTXEBUS.Call.GetAllorOne(mahd).Rows)
                            {
                                rpHLV g = new rpHLV();
                                g.Xe = XeBUS.Call.GetAllorOne(item["MAXE"].ToString()).Rows[0]["TENXE"].ToString();
                                hvl.Add(g);
                            }
                            rp.DataSource = hvl;
                            rp.ShowPreviewDialog();
                            gcHLV.DataSource = HLVBUS.Call.GetAllorOne();
                        }
                        else XtraMessageBox.Show("Không đủ xe!", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else XtraMessageBox.Show("Huấn luyện viên đang bận!", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void barButtonItem4_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gvHLV.RowCount > 0)
            {
                if (gcHLV.IsFocused == true)
                {
                    if (Convert.ToBoolean(gvHLV.GetFocusedRowCellValue("TRANGTHAI").ToString()))
                    {
                        if (XeBUS.Call.GetAllorOne("", "", "1").Rows.Count > 1)
                        {
                            string mahd;
                            string IdLast = "0";
                            if (HDThueHLVBUS.Call.GetAllorOne().Rows.Count > 0)
                            {
                                DataRow HDThueHLV = HDThueHLVBUS.Call.GetAllorOne().Rows[HDThueHLVBUS.Call.GetAllorOne().Rows.Count - 1];
                                IdLast = HDThueHLV["MAHD"].ToString();
                            }
                            mahd = Help.AutoIncreaseID.IncreaseID("HDHLV", IdLast, 3);
                            HDThueHLVBUS.Call.Add(mahd, DateTime.Now, account["MANV"].ToString(), khachHang["MAKH"].ToString(), gvHLV.GetFocusedRowCellValue("MAHLV").ToString(), "MDV002");
                            for (int i = 0; i < 2; i++)
                            {
                                string maxe = XeBUS.Call.GetAllorOne("", "", "1").Rows[i]["MAXE"].ToString();
                                HDThueHLVCTXEBUS.Call.Add(mahd, maxe);
                                XeBUS.Call.Update(maxe, false);
                                HoatDongBUS.Call.Add(maxe, khachHang["MAKH"].ToString(), TimeSpan.Parse("1:00:00"), false, gvHLV.GetFocusedRowCellValue("MAHLV").ToString());
                            }
                            HLVBUS.Call.Update(gvHLV.GetFocusedRowCellValue("MAHLV").ToString(), false);
                            DataRow hdhlv = HDThueHLVBUS.Call.GetAllorOne(mahd).Rows[0];
                            var rp = new HoaDonThueHLV();
                            rp.DataMember = "";
                            rp.NV.Value = NhanVienBUS.Call.GetAllorOne(hdhlv["MANV"].ToString()).Rows[0]["TENNV"];
                            rp.KH.Value = KhachHangBUS.Call.GetAllorOne(hdhlv["MAKH"].ToString()).Rows[0]["TENKH"];
                            rp.DV.Value = LoaiDVBUS.Call.GetAllorOne(hdhlv["MADV"].ToString()).Rows[0]["TENDV"];
                            rp.HLV.Value = HLVBUS.Call.GetAllorOne(hdhlv["MAHLV"].ToString()).Rows[0]["TENHLV"];
                            rp.NL.Value = hdhlv["NGAYLAP"].ToString();
                            rp.TT.Value = LoaiDVBUS.Call.GetAllorOne(hdhlv["MADV"].ToString()).Rows[0]["DONGIA"];
                            List<rpHLV> hvl = new List<rpHLV>();
                            foreach (DataRow item in HDThueHLVCTXEBUS.Call.GetAllorOne(mahd).Rows)
                            {
                                rpHLV g = new rpHLV();
                                g.Xe = XeBUS.Call.GetAllorOne(item["MAXE"].ToString()).Rows[0]["TENXE"].ToString();
                                hvl.Add(g);
                            }
                            rp.DataSource = hvl;
                            rp.ShowPreviewDialog();
                            gcHLV.DataSource = HLVBUS.Call.GetAllorOne();
                        }
                        else XtraMessageBox.Show("Không đủ xe!", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else XtraMessageBox.Show("Huấn luyện viên đang bận!", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
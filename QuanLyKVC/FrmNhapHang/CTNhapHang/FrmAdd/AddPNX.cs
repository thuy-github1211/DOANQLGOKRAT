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
    public partial class AddPNX : DevExpress.XtraEditors.XtraForm
    {
        CTPNX frm;
        string mapn;
        string ncc;
        public AddPNX()
        {
            InitializeComponent();
        }
        public AddPNX(CTPNX frm, string mapn, string ncc) : this()
        {
            this.mapn = mapn;
            this.frm = frm;
            this.ncc = NhaCungCapBUS.Call.GetAllorOne("", ncc).Rows[0]["MANCC"].ToString(); 
        }
        private void AddPNX_Load(object sender, EventArgs e)
        {
            DataTable xe = new DataTable();
            xe.Columns.Add("Tên xe");
            xe.Columns.Add("Số lượng tồn");
            foreach (DataRow item in XeBUS.Call.GetAllorOne("", "", "", ncc).Rows)
            {
                DataRow itemn = xe.NewRow();
                itemn[0] = item["TENXE"];
                itemn[1] = XeBUS.Call.GetAllorOne("", itemn[0].ToString()).Rows.Count;
                xe.Rows.Add(itemn);
            }
            lookUpEdit1.Properties.DataSource = xe;
            lookUpEdit1.Properties.DisplayMember = "Tên xe";
            lookUpEdit1.Properties.ValueMember = "Tên xe";
        }
        private void updatePhieuNhap()
        {
            double tongtien = double.Parse(HDPNBUS.Call.GetAllorOne(mapn).Rows[0]["TONGTIEN"].ToString());
            double thanhtien = int.Parse(textEdit1.Text) * double.Parse(textEdit2.Text);
            HDPNBUS.Call.Update(mapn, -1, "", (tongtien + thanhtien).ToString(), "");
        }
        private void simpleButton1_Click(object sender, EventArgs e)
        {
            DataRow xe = XeBUS.Call.GetAllorOne("", lookUpEdit1.Text).Rows[0];
            DataTable allctpnx = new DataTable();
            try
            {

                allctpnx = CTPhieuNhapXeBUS.Call.GetAllorOne(mapn, xe["MANCC"].ToString(), xe["TENXE"].ToString());
            }
            catch (Exception) { }
            if (!(allctpnx.Rows.Count > 0))
            {
                CTPhieuNhapXeBUS.Call.Add(mapn, xe["MANCC"].ToString(), xe["TENXE"].ToString(), int.Parse(textEdit1.Text), double.Parse(textEdit2.Text));
            }
            else
            {
                DataRow ctpnx = allctpnx.Rows[0];
                if (int.Parse(ctpnx["DONGIA"].ToString()) != int.Parse(textEdit2.Text))
                {
                    XtraMessageBox.Show("Đơn giá khác ban đầu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    textEdit2.Focus();
                    return;
                }
                int sl = int.Parse(ctpnx["SOLUONG"].ToString()) + int.Parse(textEdit1.Text);
                double dongia = double.Parse(textEdit2.Text);
                CTPhieuNhapXeBUS.Call.Update(mapn, xe["MANCC"].ToString(), xe["TENXE"].ToString(), sl, dongia);
            }
            updatePhieuNhap();
            this.Close();
        }

        private void AddPNX_FormClosing(object sender, FormClosingEventArgs e)
        {
            frm.Enabled = true;
            frm.load();
        }
    }
}
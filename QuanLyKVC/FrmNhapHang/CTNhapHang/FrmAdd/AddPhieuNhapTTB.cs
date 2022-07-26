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
    public partial class AddPhieuNhapTTB : DevExpress.XtraEditors.XtraForm
    {
        CTPhieuNhapTTB frm;
        string mapn;
        public AddPhieuNhapTTB()
        {
            InitializeComponent();
        }
        public AddPhieuNhapTTB(CTPhieuNhapTTB frm, string mapn) : this()
        {
            this.mapn = mapn;
            this.frm = frm;
        }
        private void AddPhieuNhapTTB_Load(object sender, EventArgs e)
        {
            DataTable xe = new DataTable();
            xe.Columns.Add("Mã thiết bị");
            xe.Columns.Add("Tên thiết bị");
            xe.Columns.Add("Tên nhà cung cấp");
            foreach (DataRow item in TrangThietBiBUS.Call.GetAllorOne().Rows)
            {
                DataRow itemn = xe.NewRow();
                itemn[0] = item["MATHIETBI"];
                itemn[1] = item["TENTHIETBI"];
                itemn[2] = NhaCungCapBUS.Call.GetAllorOne(item["MANCC"].ToString()).Rows[0]["TENNCC"];
                xe.Rows.Add(itemn);
            }
            lookUpEdit1.Properties.DataSource = xe;
            lookUpEdit1.Properties.DisplayMember = "Tên thiết bị";
            lookUpEdit1.Properties.ValueMember = "Mã thiết bị";
        }
        private void updatePhieuNhap()
        {
            double tongtien = double.Parse(HDPNBUS.Call.GetAllorOne(mapn).Rows[0]["TONGTIEN"].ToString());
            double thanhtien = int.Parse(textEdit1.Text) * double.Parse(textEdit2.Text);
            HDPNBUS.Call.Update(mapn, -1, "", (tongtien + thanhtien).ToString(), "");
        }
        private void simpleButton1_Click(object sender, EventArgs e)
        {
            DataRow ttb = TrangThietBiBUS.Call.GetAllorOne(lookUpEdit1.EditValue.ToString()).Rows[0];
            DataTable allctpnttb = new DataTable();
            try
            {
                allctpnttb = CTPhieuNhapTTBBUS.Call.GetAllorOne(mapn, ttb["MANCC"].ToString(), ttb["TENDOBAOHO"].ToString());
            }catch (Exception){}
            if (!(allctpnttb.Rows.Count > 0))
            {
                CTPhieuNhapTTBBUS.Call.Add(mapn, ttb["MANCC"].ToString(), ttb["MATHIETBI"].ToString(), int.Parse(textEdit1.Text), double.Parse(textEdit2.Text));
            }
            else
            {
                DataRow ctpnttb = allctpnttb.Rows[0];
                int sl = int.Parse(ctpnttb["SOLUONG"].ToString()) + int.Parse(textEdit1.Text);
                double dongia = double.Parse(textEdit2.Text);
                CTPhieuNhapTTBBUS.Call.Update(mapn, ttb["MANCC"].ToString(), ttb["MATHIETBI"].ToString(), sl, dongia);
            }
            updatePhieuNhap();
            this.Close();
        }

        private void AddPhieuNhapTTB_FormClosing(object sender, FormClosingEventArgs e)
        {
            frm.Enabled = true;
            frm.load();
        }
    }
}
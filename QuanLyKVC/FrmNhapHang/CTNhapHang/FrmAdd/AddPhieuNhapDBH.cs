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
    public partial class AddPhieuNhapDBH : DevExpress.XtraEditors.XtraForm
    {
        CTPhieuNhapDBH frm;
        string mapn;
        string ncc1;
        public AddPhieuNhapDBH()
        {
            InitializeComponent();
        }
        public AddPhieuNhapDBH(CTPhieuNhapDBH frm, string mapn, string ncc = "") : this()
        {
            this.mapn = mapn;
            this.frm = frm;
            this.ncc1 = ncc;
        }
        private void AddPhieuNhapDBH_Load(object sender, EventArgs e)
        {
            DataTable ncc = new DataTable();
            ncc.Columns.Add("Nhà cung cấp");
            foreach (DataRow item in NhaCungCapBUS.Call.GetAllorOne().Rows)
            {
                DataRow itemn = ncc.NewRow();
                itemn[0] = item["TENNCC"];
                ncc.Rows.Add(itemn);
            }
            lookUpEdit2.Properties.DataSource = ncc;
            lookUpEdit2.Properties.DisplayMember = "Nhà cung cấp";
            if(ncc1 != "")
            {
                lookUpEdit2.EditValue = ncc1;
                lookUpEdit2.Text = ncc1;
                lookUpEdit2.ReadOnly = true;
            }    

        }
        private void updatePhieuNhap()
        {
            double tongtien = double.Parse(HDPNBUS.Call.GetAllorOne(mapn).Rows[0]["TONGTIEN"].ToString());
            double thanhtien = int.Parse(textEdit1.Text) * double.Parse(textEdit2.Text);
            HDPNBUS.Call.Update(mapn, -1, "", (tongtien + thanhtien).ToString(), "");
        }
        private void simpleButton1_Click(object sender, EventArgs e)
        {

            DataRow dbh = DoBaoHoBUS.Call.GetAllorOne("", lookUpEdit1.Text).Rows[0];
            DataTable allctpndbh = new DataTable();
            try
            {
                allctpndbh = CTPhieuNhapDBHBUS.Call.GetAllorOne(mapn, dbh["MANHACC"].ToString(), dbh["TENDOBAOHO"].ToString());

            }
            catch (Exception) { }
            if (!(allctpndbh.Rows.Count > 0))
            {
                CTPhieuNhapDBHBUS.Call.Add(mapn, dbh["MANHACC"].ToString(), dbh["TENDOBAOHO"].ToString(), int.Parse(textEdit1.Text), double.Parse(textEdit2.Text));
            }
            else
            {
                DataRow ctpndbh = allctpndbh.Rows[0];
                if (int.Parse(ctpndbh["DONGIA"].ToString()) != int.Parse(textEdit2.Text))
                {
                    XtraMessageBox.Show("Đơn giá khác ban đầu!","Thông báo" , MessageBoxButtons.OK, MessageBoxIcon.Error);
                    textEdit2.Focus();
                    return;
                }
                int sl = int.Parse(ctpndbh["SOLUONG"].ToString()) + int.Parse(textEdit1.Text);
                double dongia = double.Parse(textEdit2.Text);
                CTPhieuNhapDBHBUS.Call.Update(mapn, dbh["MANHACC"].ToString(), dbh["TENDOBAOHO"].ToString(), sl, dongia);
            }
            updatePhieuNhap();
            frm.load();
            frm.NCC = lookUpEdit2.Text;
            this.Close();
        }

        private void AddPhieuNhapDBH_FormClosing(object sender, FormClosingEventArgs e)
        {
            frm.Enabled = true;
            frm.load();
        }

        private void lookUpEdit2_EditValueChanged(object sender, EventArgs e)
        {
            if (lookUpEdit2.EditValue != null)
            {
                lookUpEdit1.ReadOnly = false;
                textEdit1.ReadOnly = false;
                textEdit2.ReadOnly = false;
                simpleButton1.Enabled = true;
                string ma = NhaCungCapBUS.Call.GetAllorOne("", lookUpEdit2.Text).Rows[0]["MANCC"].ToString();
                DataTable xe = new DataTable();
                xe.Columns.Add("Đồ bảo hộ");
                xe.Columns.Add("Số lượng tồn");
                foreach (DataRow item in DoBaoHoBUS.Call.GetAllorOne("", "", "", "", ma).Rows)
                {
                    DataRow itemn = xe.NewRow();
                    itemn[0] = item["TENDOBAOHO"];
                    itemn[1] = DoBaoHoBUS.Call.GetAllorOne("", itemn[0].ToString()).Rows.Count;
                    xe.Rows.Add(itemn);
                }
                lookUpEdit1.Properties.DataSource = xe;
                lookUpEdit1.Properties.DisplayMember = "Đồ bảo hộ";
            }
            else
            {
                lookUpEdit1.ReadOnly = true;
                textEdit1.ReadOnly = true;
                textEdit2.ReadOnly = true;
                simpleButton1.Enabled = false;
            }
        }
    }
}
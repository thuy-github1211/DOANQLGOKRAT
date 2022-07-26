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
    public partial class AddDBH : DevExpress.XtraEditors.XtraForm
    {
        double tongtien = 0;
        string mahd;
        BanVe bv;
        public AddDBH()
        {
            InitializeComponent();
        }
        public AddDBH(BanVe bv, string mahd) : this()
        {
            this.mahd = mahd;
            this.bv = bv;
        }
        bool CheckNull()
        {
            if (cbxLDBH.Text == "" || tbxSLDBH.Text == "0")
                return true;
            return false;
        }
        private void btnThemDBH_Click(object sender, EventArgs e)
        {
            if (!CheckNull())
            {
                bool outt = true;
                string maloai = LoaiDBHBUS.Call.GetAllorOne("", cbxLDBH.Text).Rows[0]["MALOAIDBH"].ToString();
                double dongia = double.Parse(LoaiDBHBUS.Call.GetAllorOne(maloai, "").Rows[0]["DONGIA"].ToString());
                DataTable DBH1 = DoBaoHoBUS.Call.GetAllorOne("", "", "1", maloai);
                if (DBH1.Rows.Count >= int.Parse(tbxSLDBH.Text))
                {
                    for (int i = 0; i < int.Parse(tbxSLDBH.Text); i++)
                    {
                        DataTable DBH = DoBaoHoBUS.Call.GetAllorOne("", "", "1", maloai);
                        CTHDDBHBUS.Call.Add(mahd, DBH.Rows[0]["MADOBAOHO"].ToString(), maloai, dongia);
                        DoBaoHoBUS.Call.Update(DBH.Rows[0]["MADOBAOHO"].ToString(), false, "", "", "");
                        tongtien += dongia;
                        bv.Tongtien += tongtien;
                    }
                }
                else
                {
                    XtraMessageBox.Show("Hết đồ bảo hộ!", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    outt = false;
                }
                if(outt == true)
                    this.Close();
            }
        }

        private void AddDBH_Load(object sender, EventArgs e)
        {
            foreach (DataRow row in LoaiDBHBUS.Call.GetAllorOne().Rows)
            {
                cbxLDBH.Properties.Items.Add(row["TENLOAIDBH"]);
            }
        }

        private void AddDBH_FormClosing(object sender, FormClosingEventArgs e)
        {
            bv.load();
            bv.Enabled = true;
        }
    }
}
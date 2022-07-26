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
    public partial class AddKH : DevExpress.XtraEditors.XtraForm
    {
        KhachHang kh;
        public AddKH()
        {
            InitializeComponent();
        }
        public AddKH(KhachHang kh) :this()
        {
            this.kh = kh;
        }
        bool CheckNull()
        {
            if (tbxSDT.Text == "" || tbxTenKH.Text == "" || cbxGioiTinh.Text == "")
                return true;
            return false;
        }
        private void btnLuu_Click(object sender, EventArgs e)
        {
            if(!CheckNull())
            {
                KhachHangBUS.Call.AddKH(tbxMaKH.Text, tbxTenKH.Text, cbxGioiTinh.Text, tbxSDT.Text);
                kh.load();
                kh.Enabled = true;
                this.Close();
            }    
        }

        private void AddKH_Load(object sender, EventArgs e)
        {
            tbxMaKH.Enabled = false;
            DataRow KH1 = KhachHangBUS.Call.GetAllorOne().Rows[KhachHangBUS.Call.GetAllorOne().Rows.Count - 1];
            string IdLast = KH1["MAKH"].ToString();
            tbxMaKH.Text = Help.AutoIncreaseID.IncreaseID("KH", IdLast, 3);
        }

        private void AddKH_FormClosed(object sender, FormClosedEventArgs e)
        {
            kh.Enabled = true;
        }
    }
}
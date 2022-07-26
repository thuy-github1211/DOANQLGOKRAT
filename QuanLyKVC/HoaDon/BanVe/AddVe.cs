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
    public partial class AddVe : DevExpress.XtraEditors.XtraForm
    {
        double tongtien = 0;
        string mahd;
        string makh;
        BanVe bv;
        public AddVe()
        {
            InitializeComponent();
        }
        public AddVe(BanVe bv,string mahd) : this()
        {
            this.mahd = mahd;
            this.bv = bv;
            makh = HDBVBUS.Call.GetAllorOne(mahd).Rows[0]["MAKH"].ToString();
        }
        bool CheckNull()
        {
            if (cbxVe.Text == "" || tbxSL.Text == "0")
                return true;
            return false;
        }
        private void AddVe_Load(object sender, EventArgs e)
        {
            foreach (DataRow row in LoaiVeBUS.Call.GetAllorOne().Rows)
            {
                cbxVe.Properties.Items.Add(row["TENLOAIVE"]);
            }
        }

        private void btnThemVe_Click(object sender, EventArgs e)
        {
            if(!CheckNull())
            {
                string mave = LoaiVeBUS.Call.GetAllorOne("", cbxVe.Text).Rows[0]["MALOAIVE"].ToString();
                double dongia = double.Parse(LoaiVeBUS.Call.GetAllorOne(mave, "").Rows[0]["DONGIA"].ToString());
                for (int i = 0; i < int.Parse(tbxSL.Text); i++)
                {
                    string IdLast = "0";
                    if (CTHDBVBUS.Call.GetAllorOne().Rows.Count > 0)
                    {
                        DataRow CTHDBV = CTHDBVBUS.Call.GetAllorOne().Rows[CTHDBVBUS.Call.GetAllorOne().Rows.Count - 1];
                        IdLast = CTHDBV["MACTHD"].ToString();
                    }
                    string macthd = Help.AutoIncreaseID.IncreaseID("CTHDBV", IdLast, 3);
                    DataTable Xe = XeBUS.Call.GetAllorOne("","", "1");
                    if (Xe.Rows.Count > 0)
                    {
                        CTHDBVBUS.Call.Add(macthd, mahd, mave, Xe.Rows[0]["MAXE"].ToString(), dongia);
                        XeBUS.Call.Update(Xe.Rows[0]["MAXE"].ToString(), false, "", "", "");
                        HoatDongBUS.Call.Add(Xe.Rows[0]["MAXE"].ToString(), makh, TimeSpan.Parse(LoaiVeBUS.Call.GetAllorOne(mave).Rows[0]["THOIGIAN"].ToString()), false,"",mahd);
                        tongtien += dongia;
                        bv.Tongtien += tongtien;
                        this.Close();
                    }
                    else
                    {
                        XtraMessageBox.Show("Không đủ xe!", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;
                    }
                }
            }    
        }

        private void AddVe_FormClosing(object sender, FormClosingEventArgs e)
        {
            bv.load();
            bv.Enabled = true;
        }
    }
}
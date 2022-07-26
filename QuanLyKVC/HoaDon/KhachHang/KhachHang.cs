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
    public partial class KhachHang : DevExpress.XtraEditors.XtraForm
    {
        Main frm;
        DataRow account;
        public KhachHang()
        {
            InitializeComponent();
        }
        public KhachHang(Main frm,DataRow account) : this()
        {
            this.account = account;
            this.frm = frm;
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
        private void btnThem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Enabled = false;
            Form frm = this.KiemTraTonTai(typeof(AddKH));
            if (frm != null)
                frm.Activate();
            else
            {
                AddKH f = new AddKH(this);
                f.Show();
            }
        }
        private void btnXoa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gvKhachHang.RowCount > 0)
            {
                if (gcKhachHang.IsFocused == true)
                {
                    string IdLast = "0";
                    if (HDBVBUS.Call.GetAllorOne().Rows.Count > 0)
                    {
                        DataRow HDBV = HDBVBUS.Call.GetAllorOne().Rows[HDBVBUS.Call.GetAllorOne().Rows.Count - 1];
                        IdLast = HDBV["MAHD"].ToString();
                    }
                    string mahd = Help.AutoIncreaseID.IncreaseID("HDBV", IdLast, 3);
                    HDBVBUS.Call.Add(mahd, gvKhachHang.GetFocusedRowCellValue(colMAKH).ToString(), account["MANV"].ToString(), DateTime.Now, 0); ;
                    frm.callBV(mahd);
                    this.Close();
                }
                else XtraMessageBox.Show("Vui lòng chọn khách hàng!", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


        }
        public void load()
        {
            gcKhachHang.DataSource = KhachHangBUS.Call.GetAllorOne();
        }
        private void KhachHang_Load(object sender, EventArgs e)
        {
            load();
        }

        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gvKhachHang.RowCount > 0)
            {
                if (gcKhachHang.IsFocused == true)
                {
                    frm.callHLV(gvKhachHang.GetFocusedRowCellValue(colMAKH).ToString());
                    this.Close();
                }
                else XtraMessageBox.Show("Vui lòng chọn khách hàng!", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void KhachHang_FormClosing(object sender, FormClosingEventArgs e)
        {
            
        }
    }
}
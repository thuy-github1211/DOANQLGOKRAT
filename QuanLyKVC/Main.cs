using DevExpress.XtraEditors;
using DevExpress.XtraLayout;
using DevExpress.XtraReports.UI;
using DevExpress.XtraSplashScreen;
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
    public partial class Main : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        #region Variable
        string idaccount = "";
        DataRow account;
        Login frmLogin;
        #endregion

        #region Constructor
        public Main()
        {
            InitializeComponent();
        }
        public Main(Login frmlogin, string iduser) : this()
        {
            idaccount = iduser;
            frmLogin = frmlogin;
        }
        #endregion

        #region MainForm
        private void Main_Load(object sender, EventArgs e)
        {
            account = AccountBUS.Call.GetAllorOne(idaccount).Rows[0];
            string maloai = NhanVienBUS.Call.GetAllorOne(account["MANV"].ToString()).Rows[0]["MALOAI"].ToString();
            if (maloai == "NVL003")
            {
                layoutControlItem2.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                layoutControlItem3.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            }
            if (maloai == "NVL002")
            {
                layoutControlItem4.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                layoutControlItem3.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            }
        }
        #endregion

        #region Function
        
        private Form KiemTraTonTai(Type fType)
        {
            foreach (Form f in MdiChildren)
            {
                if (f.GetType() == fType) //Neu Form duoc truyen vao da duoc mo
                    return f;
            }
            return null;
        }
        #endregion

        #region FormCall
        private void labelKH_Click(object sender, EventArgs e)
        {
            Form frm = this.KiemTraTonTai(typeof(KhachHang));
            if (frm != null)
                frm.Activate();
            else
            {
                callKH();
            }
        }

        private void labelHD_Click(object sender, EventArgs e)
        {
            Form frm = this.KiemTraTonTai(typeof(HoatDong));
            if (frm != null)
                frm.Activate();
            else
            {
                SplashScreenManager.ShowDefaultWaitForm();
                HoatDong f = new HoatDong(account)
                {
                    MdiParent = this
                };
                f.Show();
                SplashScreenManager.CloseDefaultSplashScreen();
            }
        }
        private void labelPN_Click(object sender, EventArgs e)
        {
            DataRow nv = NhanVienBUS.Call.GetAllorOne(account["MANV"].ToString()).Rows[0];
            Form frm = this.KiemTraTonTai(typeof(PhieuNhap));
            if (frm != null)
                frm.Activate();
            else
            {
                callPN(nv);
            }
        }
        private void labelBC_Click(object sender, EventArgs e)
        {
            //Form frm = this.KiemTraTonTai(typeof(HoatDong));
            //if (frm != null)
            //    frm.Activate();
            //else
            //{
            //    SplashScreenManager.ShowDefaultWaitForm();
            //    HoatDong f = new HoatDong()
            //    {
            //        MdiParent = this
            //    };
            //    f.Show();
            //    SplashScreenManager.CloseDefaultSplashScreen();
            //}
        }
        public void callBV(string mahd)
        {
            SplashScreenManager.ShowDefaultWaitForm();
            BanVe f = new BanVe(this, mahd)
            {
                MdiParent = this
            };
            f.Show();
            SplashScreenManager.CloseDefaultSplashScreen();
        }
        public void callHLV(string makh)
        {
            SplashScreenManager.ShowDefaultWaitForm();
            HuanLuyenVien f = new HuanLuyenVien(this,account, makh)
            {
                MdiParent = this
            };
            f.Show();
            SplashScreenManager.CloseDefaultSplashScreen();
        }
        public void callKH()
        {
            SplashScreenManager.ShowDefaultWaitForm();
            KhachHang f = new KhachHang(this, account)
            {
                MdiParent = this
            };
            f.Show();
            SplashScreenManager.CloseDefaultSplashScreen();
        }
        public void callPN(DataRow nv)
        {
            SplashScreenManager.ShowDefaultWaitForm();
            PhieuNhap f = new PhieuNhap(this,nv)
            {
                MdiParent = this
            };
            f.Show();
            SplashScreenManager.CloseDefaultSplashScreen();
        }
        public void callCTPNX(string mapn)
        {
            SplashScreenManager.ShowDefaultWaitForm();
            CTPNX f = new CTPNX(this, account, mapn)
            {
                MdiParent = this
            };
            f.Show();
            SplashScreenManager.CloseDefaultSplashScreen();
        }
        public void callCTPNTTB(string mapn)
        {
            SplashScreenManager.ShowDefaultWaitForm();
            CTPhieuNhapTTB f = new CTPhieuNhapTTB(this, account, mapn)
            {
                MdiParent = this
            };
            f.Show();
            SplashScreenManager.CloseDefaultSplashScreen();
        }
        public void callCTPNDBH(string mapn)
        {
            SplashScreenManager.ShowDefaultWaitForm();
            CTPhieuNhapDBH f = new CTPhieuNhapDBH(this, account, mapn)
            {
                MdiParent = this
            };
            f.Show();
            SplashScreenManager.CloseDefaultSplashScreen();
        }
        #endregion

        #region TaskControl
        private void btnDangxuat_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Close();
            frmLogin.Visible = true;
        }







        #endregion


    }
}
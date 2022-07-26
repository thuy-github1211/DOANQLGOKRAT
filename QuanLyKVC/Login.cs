using DevExpress.XtraEditors;
using DevExpress.XtraSplashScreen;
using KVC_BUS;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyKVC
{
    public partial class Login : DevExpress.XtraEditors.XtraForm
    {
        DataTable listaccount = new DataTable();
        string iDUser = "";
        public string IDUser { get => iDUser; set => iDUser = value; }
        public Login()
        {
            InitializeComponent();
        }
        private void ReadSettings()
        {
            if (Properties.Settings.Default.remember == "true")
            {
                tbxUsername.Text = Properties.Settings.Default.user;
                tbxPass.Text = Properties.Settings.Default.pass;
                ck_Remember.Checked = true;
            }
        }
        private void SaveSettings()
        {
            if (ck_Remember.Checked)
            {
                Properties.Settings.Default.user = tbxUsername.Text;
                Properties.Settings.Default.pass = tbxPass.Text;
                Properties.Settings.Default.remember = "true";
            }
            else
            {
                Properties.Settings.Default.user = "";
                Properties.Settings.Default.pass = "";
                Properties.Settings.Default.remember = "false";
            }
            Properties.Settings.Default.Save();
        }
        private void btnDangNhap_Click(object sender, EventArgs e)
        {
            SaveSettings();
            //Load data
            if (listaccount.Rows.Count == 0)
            {
                IOverlaySplashScreenHandle LoadData = SplashScreenManager.ShowOverlayForm(layoutControl1);
                listaccount = AccountBUS.Call.GetAllorOne();
                LoadData.Close();
            }
            foreach (DataRow item in listaccount.Rows)
            {

                if (tbxUsername.Text == item["USERNAME"].ToString() && tbxPass.Text == item["PASS"].ToString())
                {
                    IDUser = item["ID"].ToString();
                    listaccount.Clear();
                    tbxUsername.Select();
                    Hide();
                    SplashScreenManager.ShowDefaultSplashScreen("Đang mở...", "Phần Mềm Quản Lý Khu Vui Chơi");
                    Thread.Sleep(500);
                    Main frmMain = new Main(this, IDUser);
                    frmMain.Show();
                    SplashScreenManager.CloseDefaultSplashScreen();
                    return;
                }
            }
            XtraMessageBox.Show("Tài khoản hoặc mật khẩu không đúng!", "Thông Báo!", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void Login_Load(object sender, EventArgs e)
        {
            ReadSettings();
        }

        private void ck_Remember_CheckedChanged(object sender, EventArgs e)
        {
            ReadSettings();
        }
    }
}
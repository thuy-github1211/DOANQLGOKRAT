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
    public partial class HoatDong : DevExpress.XtraEditors.XtraForm
    {
        DataRow account;
        public HoatDong()
        {
            InitializeComponent();
        }
        public HoatDong(DataRow account):this()
        {
            this.account = account;
        }
        private void HoatDong_Load(object sender, EventArgs e)
        {
            gridControl1.DataSource = HoatDongBUS.Call.GetAllorOne();
        }

        private void gridView1_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            if(e.Column.FieldName == "THOIGIAN")
            {
                e.DisplayText = "";
            }    
        }

        private void bandedGridView1_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            if (e.Column.FieldName == "MAKH")
                e.DisplayText = KhachHangBUS.Call.GetAllorOne(e.CellValue.ToString()).Rows[0]["TENKH"].ToString();
            if (e.Column.FieldName == "MAHLV")
            {
                if (e.CellValue.ToString() != "")
                    e.DisplayText = HLVBUS.Call.GetAllorOne(e.CellValue.ToString()).Rows[0]["TENHLV"].ToString();
                else e.DisplayText = "";
            }    
               
                
        }

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if(bandedGridView1.RowCount > 0)
            {
                if(gridControl1.IsFocused == true)
                {
                    if(bool.Parse(bandedGridView1.GetFocusedRowCellValue(gridColumn4).ToString()) == false)
                    {
                        HoatDongBUS.Call.Update(bandedGridView1.GetFocusedRowCellValue(gridColumn1).ToString(),true);
                        gridControl1.DataSource = HoatDongBUS.Call.GetAllorOne();
                    } else XtraMessageBox.Show("Xe đang sử dụng!");
                }    
            }    
        }

        private void bandedGridView1_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            if (bool.Parse(bandedGridView1.GetFocusedRowCellValue(gridColumn4).ToString()) == false)
                barButtonItem1.Enabled = true;
            else barButtonItem1.Enabled = false;
        }

        private void barButtonItem3_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (bandedGridView1.RowCount > 0)
            {
                if (gridControl1.IsFocused == true)
                {
                    if (bool.Parse(bandedGridView1.GetFocusedRowCellValue(gridColumn4).ToString()) == true)
                    {
                        HoatDongBUS.Call.Remove(bandedGridView1.GetFocusedRowCellValue(gridColumn1).ToString());
                        if(bandedGridView1.GetFocusedRowCellValue(gridColumn5).ToString() == "")
                        {
                            foreach (DataRow item in CTHDDBHBUS.Call.GetAllorOne(bandedGridView1.GetFocusedRowCellValue(bandedGridColumn1).ToString()).Rows)
                            {
                                DoBaoHoBUS.Call.Update(item["MADBH"].ToString(),true);
                            }
                        }  
                        else
                        {
                            HLVBUS.Call.Update(bandedGridView1.GetFocusedRowCellValue(gridColumn5).ToString(),true);
                        }
                        XeBUS.Call.Update(bandedGridView1.GetFocusedRowCellValue(gridColumn1).ToString(), true);
                        gridControl1.DataSource = HoatDongBUS.Call.GetAllorOne();
                    }
                    else XtraMessageBox.Show("Xe đang sử dụng!");
                }
            }
        }

        private void barButtonItem5_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            gridControl1.DataSource = HoatDongBUS.Call.GetAllorOne();
        }

        private void barButtonItem4_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (bandedGridView1.RowCount > 0)
            {
                foreach (DataRow item in HoatDongBUS.Call.GetAllorOne().Rows)
                {
                    HoatDongBUS.Call.Remove(item["MAXE"].ToString());
                    if (bandedGridView1.GetFocusedRowCellValue(gridColumn5).ToString() == "")
                    {
                        foreach (DataRow item1 in CTHDDBHBUS.Call.GetAllorOne(bandedGridView1.GetFocusedRowCellValue(bandedGridColumn1).ToString()).Rows)
                        {
                            DoBaoHoBUS.Call.Update(item1["MADBH"].ToString(), true);
                        }
                    }
                    else
                    {
                        HLVBUS.Call.Update(bandedGridView1.GetFocusedRowCellValue(gridColumn5).ToString(), true);
                    }
                    XeBUS.Call.Update(bandedGridView1.GetFocusedRowCellValue(gridColumn1).ToString(), true);
                    gridControl1.DataSource = HoatDongBUS.Call.GetAllorOne();
                }
            }
        }
    }
}
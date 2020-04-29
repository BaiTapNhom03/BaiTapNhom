using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using btlquanlycuahanginternet.Class;
using COMExcel = Microsoft.Office.Interop.Excel;

namespace btlquanlycuahanginternet
{
    public partial class frmBCCPBaoTri : Form
    {
        DataTable tableBaoCao;
        public frmBCCPBaoTri()
        {
            InitializeComponent();
        }

        private void frmBCCPBaoTri_Load(object sender, EventArgs e)
        {
            Class.functions.Connect();
            txtmamay.ReadOnly = true;
            txtmaphong.ReadOnly = true;
            txttenmay.ReadOnly = true;
            txtthanhten.ReadOnly = true;
            txtTongTien.ReadOnly = true;
            txtTongTien.Text = "0";
            cboThang.Items.Add("1");
            cboThang.Items.Add("2");
            cboThang.Items.Add("3");
            cboThang.Items.Add("4");
            cboThang.Items.Add("5");
            cboThang.Items.Add("6");
            cboThang.Items.Add("7");
            cboThang.Items.Add("8");
            cboThang.Items.Add("9");
            cboThang.Items.Add("10");
            cboThang.Items.Add("11");
            cboThang.Items.Add("12");
            cboQuy.Items.Add("1");
            cboQuy.Items.Add("2");
            cboQuy.Items.Add("3");
            cboQuy.Items.Add("4");
            Class.functions.FillCombo("SELECT MaPhong FROM Phong", cboMaPhong, "MaPhong", "MaPhong");
            cboMaPhong.SelectedIndex = -1;
            loadDataToGridView();
            dataGridView_CPBT.DataSource = null;
        }
        private void loadDataToGridView()
        {
            String sql;
            sql = "select MaPhong, MayTinh.MaMay, TenMay,NgayBaoTri,ThanhTien from MayTinh join ChiTietBaoTri on MayTinh.MaMay=ChiTietBaoTri.MaMay join BaoTri on ChiTietBaoTri.MaBaoTri=BaoTri.MaBaoTri";
            tableBaoCao = Class.functions.GetDataToTable(sql);
            dataGridView_CPBT.DataSource = tableBaoCao;
            dataGridView_CPBT.Columns[0].HeaderText = "Mã Phòng";
            dataGridView_CPBT.Columns[1].HeaderText = "Mã Máy";
            dataGridView_CPBT.Columns[2].HeaderText = "Tên Máy";
            dataGridView_CPBT.Columns[3].HeaderText = "Ngày Bảo Trì";
            dataGridView_CPBT.Columns[4].HeaderText = "Thành Tiền";
            dataGridView_CPBT.Columns[0].Width = 150;
            dataGridView_CPBT.Columns[1].Width = 100;
            dataGridView_CPBT.Columns[2].Width = 100;
            dataGridView_CPBT.Columns[3].Width = 100;
            dataGridView_CPBT.Columns[4].Width = 100;
            dataGridView_CPBT.AllowUserToAddRows = false;
            dataGridView_CPBT.EditMode = DataGridViewEditMode.EditProgrammatically;
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            string sql;
            double tong;
            if ((cboMaPhong.Text == "") && (cboThang.Text == "") && (cboQuy.Text == "") &&
               (txtNam.Text == ""))
            {
                MessageBox.Show("Hãy nhập một điều kiện tìm kiếm!!!", "Yêu cầu ...", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            sql = "SELECT MaPhong, MayTinh.MaMay, TenMay,NgayBaoTri,ThanhTien from MayTinh join ChiTietBaoTri on MayTinh.MaMay=ChiTietBaoTri.MaMay join BaoTri on ChiTietBaoTri.MaBaoTri=BaoTri.MaBaoTri WHERE 1=1";
            if (cboMaPhong.Text != "")
                sql = sql + " AND MaPhong Like '%" + cboMaPhong.Text + "%' ";
            if (cboThang.Text != "")
                sql = sql + " AND MONTH(NgayBaoTri) Like '%" + cboThang.Text + "%' ";
            if (cboQuy.Text != "")
                sql = sql + " AND DATEPART(quarter, NgayBaoTri) Like '%" + cboQuy.Text + "%'";
            if (txtNam.Text != "")
                sql = sql + "AND Year(NgayBaoTri) Like '%" + txtNam.Text + "%'";
            tableBaoCao = Class.functions.GetDataToTable(sql);
            if (tableBaoCao.Rows.Count == 0)
            {
                MessageBox.Show("Không có bản ghi thỏa mãn điều kiện!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtTongTien.Text = "0";
                lblBangChu.Text = " Bằng chữ: " + "";

            }
            else
            {
                MessageBox.Show("Có " + tableBaoCao.Rows.Count + " bản ghi thỏa mãn điều kiện!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //Cập nhật lại tổng tiền cho báo cáo
                if(cboMaPhong.Text != "")
                {
                    tong = Convert.ToDouble(Class.functions.GetFieldValues("select sum(ThanhTien) from ChiTietBaoTri join BaoTri on ChiTietBaoTri.MaBaoTri = BaoTri.MaBaoTri " +
                    "join MayTinh on ChiTietBaoTri.MaMay=MayTinh.MaMay where MaPhong='" + cboMaPhong.Text + "'"));
                    txtTongTien.Text = tong.ToString();
                    lblBangChu.Text = "Bằng chữ: " + Class.functions.ChuyenSoSangChu(tong.ToString());
                }
                if ((cboMaPhong.Text != "") && (cboThang.Text != ""))
                {
                    tong = Convert.ToDouble(Class.functions.GetFieldValues("select sum(ThanhTien) from ChiTietBaoTri join BaoTri on ChiTietBaoTri.MaBaoTri = BaoTri.MaBaoTri " +
                    "join MayTinh on ChiTietBaoTri.MaMay=MayTinh.MaMay where DATEPART(mm,(NgayBaoTri)) = '" + cboThang.Text + "'" +
                    "and MaPhong='" + cboMaPhong.Text + "'"));
                    txtTongTien.Text = tong.ToString();
                    lblBangChu.Text = "Bằng chữ: " + Class.functions.ChuyenSoSangChu(tong.ToString());
                }
                if ((cboMaPhong.Text != "") && (cboQuy.Text != ""))
                {
                    tong = Convert.ToDouble(Class.functions.GetFieldValues("select sum(ThanhTien) from ChiTietBaoTri join BaoTri on ChiTietBaoTri.MaBaoTri = BaoTri.MaBaoTri " +
                    "join MayTinh on ChiTietBaoTri.MaMay=MayTinh.MaMay where DATEPART(qq,(NgayBaoTri)) = '" + cboQuy.Text + "'" +
                    "and MaPhong='" + cboMaPhong.Text + "'"));
                    txtTongTien.Text = tong.ToString();
                    lblBangChu.Text = "Bằng chữ: " + Class.functions.ChuyenSoSangChu(tong.ToString());
                }
                if ((cboMaPhong.Text != "") && (txtNam.Text != ""))
                {
                    tong = Convert.ToDouble(Class.functions.GetFieldValues("select sum(ThanhTien) from ChiTietBaoTri join BaoTri on ChiTietBaoTri.MaBaoTri = BaoTri.MaBaoTri " +
                   "join MayTinh on ChiTietBaoTri.MaMay=MayTinh.MaMay where DATEPART(yyyy,(NgayBaoTri)) = '" + txtNam.Text + "'" +
                   "and MaPhong='" + cboMaPhong.Text + "'"));
                    txtTongTien.Text = tong.ToString();
                    lblBangChu.Text = "Bằng chữ: " + Class.functions.ChuyenSoSangChu(tong.ToString());
                }
                if ((cboMaPhong.Text != "") && (cboThang.Text != "") && (txtNam.Text != ""))
                {
                    tong = Convert.ToDouble(Class.functions.GetFieldValues("select sum(ThanhTien) from ChiTietBaoTri join BaoTri on ChiTietBaoTri.MaBaoTri = BaoTri.MaBaoTri " +
                  "join MayTinh on ChiTietBaoTri.MaMay=MayTinh.MaMay where DATEPART(mm,(NgayBaoTri)) = '" + cboThang.Text + "'" +
                    "and MaPhong='" + cboMaPhong.Text + "'and DATEPART(yyyy,(NgayBaoTri))='" + txtNam.Text + "'"));
                    txtTongTien.Text = tong.ToString();
                    lblBangChu.Text = "Bằng chữ: " + Class.functions.ChuyenSoSangChu(tong.ToString());
                }
                if ((cboMaPhong.Text != "") && (cboQuy.Text != "") && (txtNam.Text != ""))
                {
                    tong = Convert.ToDouble(Class.functions.GetFieldValues("select sum(ThanhTien) from ChiTietBaoTri join BaoTri on ChiTietBaoTri.MaBaoTri = BaoTri.MaBaoTri " +
                "join MayTinh on ChiTietBaoTri.MaMay=MayTinh.MaMay where DATEPART(qq,(NgayBaoTri)) = '" + cboQuy.Text + "'" +
                    "and MaPhong='" + cboMaPhong.Text + "'and DATEPART(yyyy,(NgayBaoTri))='" + txtNam.Text + "'"));
                    txtTongTien.Text = tong.ToString();
                    lblBangChu.Text = "Bằng chữ: " + Class.functions.ChuyenSoSangChu(tong.ToString());
                }    


            }
                tableBaoCao = Class.functions.GetDataToTable(sql);
            dataGridView_CPBT.DataSource = tableBaoCao;

            Class.functions.RunSQL(sql);
            
        }
        private void ResetValues()
        {
            cboMaPhong.Text = "";
            txtNam.Text = "";
            cboThang.Text = "";
            cboQuy.Text = "";
            txtTongTien.Text = "0";
            cboMaPhong.Focus();
        }
        private void btnHienThi_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có muốn hiển thị thông tin chi tiết?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)

            {
                string sql;
                sql = "SELECT MaPhong, MayTinh.MaMay, TenMay,NgayBaoTri,ThanhTien from MayTinh join ChiTietBaoTri on MayTinh.MaMay=ChiTietBaoTri.MaMay join BaoTri on ChiTietBaoTri.MaBaoTri=BaoTri.MaBaoTri";
                tableBaoCao = Class.functions.GetDataToTable(sql);
                dataGridView_CPBT.DataSource = tableBaoCao;
            }
        }
        private void btnTimLai_Click(object sender, EventArgs e)
        {
            ResetValues();
            dataGridView_CPBT.DataSource = null;
        }

        private void btnIn_Click(object sender, EventArgs e)
        {
            // Khởi động chương trình Excel
            COMExcel.Application exApp = new COMExcel.Application();
            COMExcel.Workbook exBook; //Trong 1 chương trình Excel có nhiều Workbook
            COMExcel.Worksheet exSheet; //Trong 1 Workbook có nhiều Worksheet
            COMExcel.Range exRange;
            int phong = 0, cot = 0;
            exBook = exApp.Workbooks.Add(COMExcel.XlWBATemplate.xlWBATWorksheet);
            exSheet = exBook.Worksheets[1];
            // Định dạng chung
            exRange = exSheet.Cells[1, 1];
            exRange.Range["A1:Z300"].Font.Name = "Times new roman"; //Font chữ
            exRange.Range["A1:B3"].Font.Size = 10;
            exRange.Range["A1:B3"].Font.Bold = true;
            exRange.Range["A1:B3"].Font.ColorIndex = 5; //Màu xanh da trời
            exRange.Range["A1:A1"].ColumnWidth = 7;
            exRange.Range["B1:B1"].ColumnWidth = 15;
            exRange.Range["A1:B1"].MergeCells = true;
            exRange.Range["A1:B1"].HorizontalAlignment = COMExcel.XlHAlign.xlHAlignCenter;
            exRange.Range["A1:B1"].Value = "Cửa Hàng Internet03";
            exRange.Range["A2:B2"].MergeCells = true;
            exRange.Range["A2:B2"].HorizontalAlignment = COMExcel.XlHAlign.xlHAlignCenter;
            exRange.Range["A2:B2"].Value = "Xuân Mai - Hà Nội";
            exRange.Range["A3:B3"].MergeCells = true;
            exRange.Range["A3:B3"].HorizontalAlignment = COMExcel.XlHAlign.xlHAlignCenter;
            exRange.Range["A3:B3"].Value = "Điện thoại: (04)39641582";
            exRange.Range["C2:E2"].Font.Size = 16;
            exRange.Range["C2:E2"].Font.Bold = true;
            exRange.Range["C2:E2"].Font.ColorIndex = 3; //Màu đỏ
            exRange.Range["C2:E2"].MergeCells = true;
            exRange.Range["C2:E2"].HorizontalAlignment = COMExcel.XlHAlign.xlHAlignCenter;
            exRange.Range["C2:E2"].Value = "Báo Cáo Tổng Phí Bảo Trì";
            //Tạo dòng tiêu đề bảng
            exRange.Range["A6:F6"].Font.Bold = true;
            exRange.Range["A6:F6"].HorizontalAlignment = COMExcel.XlHAlign.xlHAlignCenter;
            exRange.Range["C6:F6"].ColumnWidth = 12;
            exRange.Range["A6:A6"].Value = "STT";
            exRange.Range["B6:B6"].Value = "Mã Phòng";
            exRange.Range["C6:C6"].Value = "Mã Máy";
            exRange.Range["D6:D6"].Value = "Tên Máy";
            exRange.Range["E6:E6"].Value = "Ngày Bảo Trì";
            exRange.Range["F6:F6"].Value = "Thành Tiền";
            for (phong = 0; phong < tableBaoCao.Rows.Count; phong++)
            {
                //Điền số thứ tự vào cột 1 từ dòng 12
                exSheet.Cells[1][phong + 7] = phong + 1;
                for (cot = 0; cot < tableBaoCao.Columns.Count; cot++)
                //Điền thông tin hàng từ cột thứ 2, dòng 7
                {
                    exSheet.Cells[cot + 2][phong + 7] = tableBaoCao.Rows[phong][cot].ToString();
                    if (cot == 3) exSheet.Cells[cot + 2][phong + 7] = tableBaoCao.Rows[phong][cot].ToString();
                }
            }
            exRange = exSheet.Cells[cot][phong + 10];
            exRange.Font.Bold = true;
            exRange.Value2 = "Tổng tiền:";
            exRange = exSheet.Cells[cot + 1][phong + 10];
            exRange.Font.Bold = true;
            double tong;
            if ((cboMaPhong.Text != "") && (cboThang.Text != ""))
            {
                tong = Convert.ToDouble(Class.functions.GetFieldValues("select sum(ThanhTien) from ChiTietBaoTri join BaoTri on ChiTietBaoTri.MaBaoTri = BaoTri.MaBaoTri " +
                "join MayTinh on ChiTietBaoTri.MaMay=MayTinh.MaMay where DATEPART(mm,(NgayBaoTri)) = '" + cboThang.Text + "'" +
                "and MaPhong='" + cboMaPhong.Text + "'"));
                exRange.Value2 = tong.ToString();
                exRange = exSheet.Cells[1][phong + 11]; //Ô A1 
                exRange.Range["A1:F1"].MergeCells = true;
                exRange.Range["A1:F1"].Font.Bold = true;
                exRange.Range["A1:F1"].Font.Italic = true;
                exRange.Range["A1:F1"].HorizontalAlignment = COMExcel.XlHAlign.xlHAlignRight;

                exRange.Range["A1:F1"].Value = "Bằng chữ: " + Class.functions.ChuyenSoSangChu(tong.ToString());
            }
            if ((cboMaPhong.Text != "") && (cboQuy.Text != ""))
            {
                tong = Convert.ToDouble(Class.functions.GetFieldValues("select sum(ThanhTien) from ChiTietBaoTri join BaoTri on ChiTietBaoTri.MaBaoTri = BaoTri.MaBaoTri " +
                "join MayTinh on ChiTietBaoTri.MaMay=MayTinh.MaMay where DATEPART(qq,(NgayBaoTri)) = '" + cboQuy.Text + "'" +
                "and MaPhong='" + cboMaPhong.Text + "'"));
                exRange.Value2 = tong.ToString();
                exRange = exSheet.Cells[1][phong + 11]; //Ô A1 
                exRange.Range["A1:F1"].MergeCells = true;
                exRange.Range["A1:F1"].Font.Bold = true;
                exRange.Range["A1:F1"].Font.Italic = true;
                exRange.Range["A1:F1"].HorizontalAlignment = COMExcel.XlHAlign.xlHAlignRight;

                exRange.Range["A1:F1"].Value = "Bằng chữ: " + Class.functions.ChuyenSoSangChu(tong.ToString());
            }
            if ((cboMaPhong.Text != "") && (txtNam.Text != ""))
            {
                tong = Convert.ToDouble(Class.functions.GetFieldValues("select sum(ThanhTien) from ChiTietBaoTri join BaoTri on ChiTietBaoTri.MaBaoTri = BaoTri.MaBaoTri " +
               "join MayTinh on ChiTietBaoTri.MaMay=MayTinh.MaMay where DATEPART(yyyy,(NgayBaoTri)) = '" + txtNam.Text + "'" +
               "and MaPhong='" + cboMaPhong.Text + "'"));
                exRange.Value2 = tong.ToString();
                exRange = exSheet.Cells[1][phong + 11]; //Ô A1 
                exRange.Range["A1:F1"].MergeCells = true;
                exRange.Range["A1:F1"].Font.Bold = true;
                exRange.Range["A1:F1"].Font.Italic = true;
                exRange.Range["A1:F1"].HorizontalAlignment = COMExcel.XlHAlign.xlHAlignRight;

                exRange.Range["A1:F1"].Value = "Bằng chữ: " + Class.functions.ChuyenSoSangChu(tong.ToString());
            }
            if ((cboMaPhong.Text != "") && (cboThang.Text != "") && (txtNam.Text != ""))
            {
                tong = Convert.ToDouble(Class.functions.GetFieldValues("select sum(ThanhTien) from ChiTietBaoTri join BaoTri on ChiTietBaoTri.MaBaoTri = BaoTri.MaBaoTri " +
              "join MayTinh on ChiTietBaoTri.MaMay=MayTinh.MaMay where DATEPART(mm,(NgayBaoTri)) = '" + cboThang.Text + "'" +
                "and MaPhong='" + cboMaPhong.Text + "'and DATEPART(yyyy,(NgayBaoTri))='" + txtNam.Text + "'"));
                exRange.Value2 = tong.ToString();
                exRange = exSheet.Cells[1][phong + 11]; //Ô A1 
                exRange.Range["A1:F1"].MergeCells = true;
                exRange.Range["A1:F1"].Font.Bold = true;
                exRange.Range["A1:F1"].Font.Italic = true;
                exRange.Range["A1:F1"].HorizontalAlignment = COMExcel.XlHAlign.xlHAlignRight;

                exRange.Range["A1:F1"].Value = "Bằng chữ: " + Class.functions.ChuyenSoSangChu(tong.ToString());
            }
            if ((cboMaPhong.Text != "") && (cboQuy.Text != "") && (txtNam.Text != ""))
            {
                tong = Convert.ToDouble(Class.functions.GetFieldValues("select sum(ThanhTien) from ChiTietBaoTri join BaoTri on ChiTietBaoTri.MaBaoTri = BaoTri.MaBaoTri " +
            "join MayTinh on ChiTietBaoTri.MaMay=MayTinh.MaMay where DATEPART(qq,(NgayBaoTri)) = '" + cboQuy.Text + "'" +
                "and MaPhong='" + cboMaPhong.Text + "'and DATEPART(yyyy,(NgayBaoTri))='" + txtNam.Text + "'"));
                exRange.Value2 = tong.ToString();
                exRange = exSheet.Cells[1][phong + 11]; //Ô A1 
                exRange.Range["A1:F1"].MergeCells = true;
                exRange.Range["A1:F1"].Font.Bold = true;
                exRange.Range["A1:F1"].Font.Italic = true;
                exRange.Range["A1:F1"].HorizontalAlignment = COMExcel.XlHAlign.xlHAlignRight;

                exRange.Range["A1:F1"].Value = "Bằng chữ: " + Class.functions.ChuyenSoSangChu(tong.ToString());

            }
            if (cboMaPhong.Text != "")
            {
                tong = Convert.ToDouble(Class.functions.GetFieldValues("select sum(ThanhTien) from ChiTietBaoTri join BaoTri on ChiTietBaoTri.MaBaoTri = BaoTri.MaBaoTri " +
                "join MayTinh on ChiTietBaoTri.MaMay=MayTinh.MaMay where MaPhong='" + cboMaPhong.Text + "'"));
                exRange.Value2 = tong.ToString();
                exRange = exSheet.Cells[1][phong + 11]; //Ô A1 
                exRange.Range["A1:F1"].MergeCells = true;
                exRange.Range["A1:F1"].Font.Bold = true;
                exRange.Range["A1:F1"].Font.Italic = true;
                exRange.Range["A1:F1"].HorizontalAlignment = COMExcel.XlHAlign.xlHAlignRight;

                exRange.Range["A1:F1"].Value = "Bằng chữ: " + Class.functions.ChuyenSoSangChu(tong.ToString());
            }




                exRange = exSheet.Cells[4][phong + 13]; //Ô A1 
            exRange.Range["A1:C1"].MergeCells = true;
            exRange.Range["A1:C1"].Font.Italic = true;
            exRange.Range["A1:C1"].HorizontalAlignment = COMExcel.XlHAlign.xlHAlignCenter;
            DateTime d = Convert.ToDateTime(DateTime.Now);
            exRange.Range["A1:C1"].Value = "Hà Nội, ngày " + d.Day + " tháng " + d.Month + " năm " + d.Year;
            exRange.Range["A2:C2"].MergeCells = true;
            exRange.Range["A2:C2"].Font.Italic = true;
            exRange.Range["A2:C2"].HorizontalAlignment = COMExcel.XlHAlign.xlHAlignCenter;
            exRange.Range["A2:C2"].Value = "Ký Tên";
            exRange.Range["A6:C6"].MergeCells = true;
            exRange.Range["A6:C6"].Font.Italic = true;
            exRange.Range["A6:C6"].HorizontalAlignment = COMExcel.XlHAlign.xlHAlignCenter;
            exApp.Visible = true;
        }

        private void btnDong_Click(object sender, EventArgs e)
        {
            this.Close();
        }

       
    }
}

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

namespace btlquanlycuahanginternet
{
    public partial class frmChonMay : Form
    {
        DataTable tableChonMay;
        public frmChonMay()
        {
            InitializeComponent();
        }

        private void frmChonMay_Load(object sender, EventArgs e)
        {
            Class.functions.Connect();
            txtMaMay.ReadOnly = true;
            txtTenMay.ReadOnly = true;
            txtTenPhong.ReadOnly = true;
            txtNgayThue.Enabled = false;
            txtGioVao.Enabled = false;
            Class.functions.FillCombo("select MaPhong from Phong ", cboMaPhong, "MaPhong", "MaPhong");
            cboMaPhong.SelectedIndex = -1;
            txtma.Focus();
            loadDataToGridView();
            dataGridView_ChonMay.DataSource = null;
        }
        private void loadDataToGridView()
        {
            string sql = " select ThueMay.MaPhong,TenPhong,ThueMay.MaMay,TenMay,TinhTrang from ThueMay join MayTinh on ThueMay.MaMay=MayTinh.MaMay join Phong on ThueMay.MaPhong=Phong.MaPhong ";
            tableChonMay = Class.functions.GetDataToTable(sql);
            dataGridView_ChonMay.DataSource = tableChonMay;
            dataGridView_ChonMay.Columns[0].HeaderText = "Mã Phòng";
            dataGridView_ChonMay.Columns[1].HeaderText = "Tên Phòng";
            dataGridView_ChonMay.Columns[2].HeaderText = "Mã Máy";
            dataGridView_ChonMay.Columns[3].HeaderText = "Tên Máy";
            dataGridView_ChonMay.Columns[4].HeaderText = "Tình Trạng";
            dataGridView_ChonMay.Columns[0].Width = 80;
            dataGridView_ChonMay.Columns[1].Width = 100;
            dataGridView_ChonMay.Columns[2].Width = 80;
            dataGridView_ChonMay.Columns[3].Width = 100;
            dataGridView_ChonMay.Columns[4].Width = 100;
            dataGridView_ChonMay.AllowUserToAddRows = false;
            dataGridView_ChonMay.EditMode = DataGridViewEditMode.EditProgrammatically;
        }
        private void ResetValues()
        {
            txtma.Text = "";
            txtTenKhach.Text = "";
            txtMaMay.Text = "";
            txtTenMay.Text = "";
            cboMaPhong.Text = "";
            txtTenPhong.Text = "";
            txtNgayThue.Text = "";
            txtGioVao.Text = "";
        }
        private void dataGridView_ChonMay_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtMaMay.Text = dataGridView_ChonMay.CurrentRow.Cells["MaMay"].Value.ToString();
            txtTenMay.Text = dataGridView_ChonMay.CurrentRow.Cells["TenMay"].Value.ToString();
            txtTenPhong.Text = dataGridView_ChonMay.CurrentRow.Cells["TenPhong"].Value.ToString();
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            string sql;
            txtNgayThue.Text = DateTime.Now.ToShortDateString();
            txtGioVao.Text = DateTime.Now.TimeOfDay.ToString();
            sql = "SELECT MaSTT FROM ThueMay WHERE MaSTT='" + txtma.Text.Trim() + "'";
            if (Class.functions.CheckKey(sql))
            {
                MessageBox.Show("Mã này đã tồn tại, vui lòng nhập mã khác!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtma.Focus();
                return;
            }
            if (txtma.Text == "")
            {
                MessageBox.Show("Hãy nhập mã của bạn!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtma.Focus();
                return;
            }
            if (txtTenKhach.Text == "")
            {
                MessageBox.Show("Hãy nhập tên của bạn!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTenKhach.Focus();
                return;
            }
            if ((cboMaPhong.Text == ""))
            {
                MessageBox.Show("Hãy nhập mã phòng!!!", "Yêu cầu ...", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            sql = "SELECT ThueMay.MaPhong,TenPhong,ThueMay.MaMay,TenMay,TinhTrang from ThueMay join MayTinh on ThueMay.MaMay=MayTinh.MaMay join Phong on ThueMay.MaPhong=Phong.MaPhong WHERE 1=1 and TinhTrang='Trong'";
            if (cboMaPhong.Text != "")
            {
                sql = sql + " AND ThueMay.MaPhong Like '%" + cboMaPhong.Text + "%'";
            }
            DataTable tblMT = Class.functions.GetDataToTable(sql);
            if (tblMT.Rows.Count == 0)
            {
                MessageBox.Show("Phòng " + cboMaPhong.Text + " không còn máy trống, hãy chọn phòng khác!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Phòng " + cboMaPhong.Text + " còn " + tblMT.Rows.Count + " máy trống, xin hãy chọn máy!!!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            tableChonMay = Class.functions.GetDataToTable(sql);
            dataGridView_ChonMay.DataSource = tableChonMay;
        }

        private void btnTimLai_Click(object sender, EventArgs e)
        {
            ResetValues();
            dataGridView_ChonMay.DataSource = null;
            btnTimKiem.Enabled = true;
        }

        private void btnHienThi_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có muốn hiển thị thông tin các phòng không?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)

            {
                string sql;
                sql = "SELECT MayTinh.MaPhong,TenPhong,MayTinh.MaMay,TenMay,TinhTrang FROM MayTinh join Phong on MayTinh.MaPhong=Phong.MaPhong WHERE TinhTrang='Trong'";
                tableChonMay = Class.functions.GetDataToTable(sql);
                dataGridView_ChonMay.DataSource = tableChonMay;
            }
        }
        private void btnDangKi_Click(object sender, EventArgs e)
        {
            if (txtMaMay.Text == "")
            {
                MessageBox.Show("Bạn chưa chọn máy nào!!!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            frmTraMay frm = new frmTraMay();
            frm.Message = txtGioVao.Text;
            frm.Message1 = cboMaPhong.Text;
            frm.Message2 = txtTenKhach.Text;
            frm.Message3 = txtMaMay.Text;
            frm.Message4 = txtNgayThue.Text;
            frm.Message5 = txtma.Text;
            frm.ShowDialog();
            if (txtMaMay.Text == "")
            {
                MessageBox.Show("Bạn chưa chọn máy nào!!!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (MessageBox.Show("Bạn có muốn chọn máy này không?", "Thông báo",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                MessageBox.Show("  Mã STT:  " + txtma.Text +"" +
                    "    Tên Khách:  "+txtTenKhach.Text+"" +
                    "    Mã Phòng:  "+cboMaPhong.Text+""+"Tên Phòng:  "+txtTenPhong.Text+"" +
                    "    Mã Máy:    "+txtMaMay.Text+""+"  Tên Máy:    "+txtTenMay.Text+""+
                    "    Ngày Thuê: "+txtNgayThue.Text+",  Giờ Vào:    "+txtGioVao.Text,                   
                    "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                loadDataToGridView();
                ResetValues();
            }
            btnTimKiem.Enabled = false;
            btnDong.Enabled = true;
            btnTimLai.Enabled = true;
            btnHienThi.Enabled = false;
           
        }

        private void btnDong_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        
    }
}

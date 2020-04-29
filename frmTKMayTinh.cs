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
    public partial class frmTKMayTinh : Form
    {
        DataTable tableTKMT;
        public frmTKMayTinh()
        {
            InitializeComponent();
        }

        private void frmTKMayTinh_Load(object sender, EventArgs e)
        {
            Class.functions.Connect();
            txtmamay.ReadOnly = true;
            txtMaPhong.ReadOnly = true;
            txtTenMay.ReadOnly = true;
            txtTinhTrang.ReadOnly = true;
            Class.functions.FillCombo("SELECT DISTINCT TinhTrang FROM MayTinh", cboTinhTrang, "TinhTrang", "TinhTrang");
            cboTinhTrang.SelectedIndex = -1;
            Class.functions.FillCombo("SELECT MaPhong, TenPhong FROM Phong", cboMaPhong, "MaPhong", "MaPhong");
            cboMaPhong.SelectedIndex = -1;
            loadDataToGridView();
            dataGridView_TKMT.DataSource = null;

        }
        private void loadDataToGridView()
        {
            String sql;
            sql = "select*from MayTinh";
            tableTKMT = Class.functions.GetDataToTable(sql);
            dataGridView_TKMT.DataSource = tableTKMT;
            dataGridView_TKMT.Columns[0].HeaderText = "Mã Máy";
            dataGridView_TKMT.Columns[1].HeaderText = "Tên Máy";
            dataGridView_TKMT.Columns[2].HeaderText = "Mã Phòng";
            dataGridView_TKMT.Columns[3].HeaderText = "Tình Trạng";
            dataGridView_TKMT.Columns[0].Width = 150;
            dataGridView_TKMT.Columns[1].Width = 100;
            dataGridView_TKMT.Columns[2].Width = 100;
            dataGridView_TKMT.Columns[3].Width = 100;
            dataGridView_TKMT.AllowUserToAddRows = false;
            dataGridView_TKMT.EditMode = DataGridViewEditMode.EditProgrammatically;

        }
        private void dataGridView_TKMT_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtmamay.Text = dataGridView_TKMT.CurrentRow.Cells["MaMay"].Value.ToString();
            txtTenMay.Text = dataGridView_TKMT.CurrentRow.Cells["TenMay"].Value.ToString();
            txtMaPhong.Text = dataGridView_TKMT.CurrentRow.Cells["MaPhong"].Value.ToString();
            txtTinhTrang.Text = dataGridView_TKMT.CurrentRow.Cells["TinhTrang"].Value.ToString();
        }
        private void ResetValues()
        {
            foreach (Control Ctl in this.Controls)
                if (Ctl is TextBox)
                    Ctl.Text = "";
            cboMaPhong.Focus();
        }
        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            string sql;
            if ((cboMaPhong.Text == "") && (cboTinhTrang.Text == ""))
            {
                MessageBox.Show("Hãy nhập một điều kiện tìm kiếm!!!", "Yêu cầu ...", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            sql = "SELECT * FROM MayTinh WHERE 1=1";
            if (cboMaPhong.Text != "")
                sql = sql + " AND MaPhong Like '%" + cboMaPhong.Text + "%' ";
            if (cboTinhTrang.Text != "")
                sql = sql + " AND TinhTrang Like '%" + cboTinhTrang.Text + "%'";
            DataTable tblMT = Class.functions.GetDataToTable(sql);
            if (tblMT.Rows.Count == 0)
            {
                MessageBox.Show("Không có bản ghi thỏa mãn điều kiện!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
                MessageBox.Show("Có " + tblMT.Rows.Count + " bản ghi thỏa mãn điều kiện!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            tableTKMT = Class.functions.GetDataToTable(sql);
            dataGridView_TKMT.DataSource = tableTKMT;
            ResetValues();
        }
        private void btnTimLai_Click(object sender, EventArgs e)
        {
            ResetValues();
            dataGridView_TKMT.DataSource = null;
        }

        private void btnHienThi_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có muốn hiển thị thông tin chi tiết?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)

            {
                string sql;
                sql = "SELECT * FROM MayTinh";
                DataTable tblMT = Class.functions.GetDataToTable(sql);
                dataGridView_TKMT.DataSource = tblMT;
            }
        }

        private void btnDong_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        

       
    }
}

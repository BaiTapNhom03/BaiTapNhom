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
    public partial class frmTKNV : Form
    {
        DataTable tableTKNV;
        public frmTKNV()
        {
            InitializeComponent();
        }

        private void frmTKNV_Load(object sender, EventArgs e)
        {
            Class.functions.Connect();
            txtmanv.ReadOnly = true;
            txtTenNVien.ReadOnly = true;
            txtmaca.ReadOnly = true;
            txtgioitinh.ReadOnly = true;
            txtnamsinh.ReadOnly = true;
            txtsdt.ReadOnly = true;
            Class.functions.FillCombo("SELECT DISTINCT GioiTinh FROM NhanVien", cbogioitinh, "GioiTinh", "GioiTinh");
            cbogioitinh.SelectedIndex = -1;
            Class.functions.FillCombo("SELECT DISTINCT MaCa FROM NhanVien", cbomaca, "MaCa", "MaCa");
            cbomaca.SelectedIndex = -1;
            loadDataToGridView();
            dataGridView_TKNV.DataSource = null;
        }
        private void loadDataToGridView()
        {
            String sql;
            sql = "select*from NhanVien";
            tableTKNV = Class.functions.GetDataToTable(sql);
            dataGridView_TKNV.DataSource = tableTKNV;
            dataGridView_TKNV.Columns[0].HeaderText = "Mã NV";
            dataGridView_TKNV.Columns[1].HeaderText = "Tên NV";
            dataGridView_TKNV.Columns[2].HeaderText = "Mã Ca";
            dataGridView_TKNV.Columns[3].HeaderText = "Giới Tính";
            dataGridView_TKNV.Columns[4].HeaderText = "Năm Sinh";
            dataGridView_TKNV.Columns[5].HeaderText = "SĐT";
            dataGridView_TKNV.Columns[2].HeaderText = "Địa Chỉ";
            dataGridView_TKNV.Columns[0].Width = 50;
            dataGridView_TKNV.Columns[1].Width = 250;
            dataGridView_TKNV.Columns[2].Width = 50;
            dataGridView_TKNV.Columns[3].Width = 50;
            dataGridView_TKNV.Columns[4].Width = 80;
            dataGridView_TKNV.Columns[5].Width = 80;
            dataGridView_TKNV.Columns[6].Width = 80;
            dataGridView_TKNV.AllowUserToAddRows = false;
            dataGridView_TKNV.EditMode = DataGridViewEditMode.EditProgrammatically;
        }

        private void dataGridView_TKNV_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtmanv.Text = dataGridView_TKNV.CurrentRow.Cells["MaNV"].Value.ToString();
            txtTenNVien.Text = dataGridView_TKNV.CurrentRow.Cells["TenNV"].Value.ToString();
            txtmaca.Text = dataGridView_TKNV.CurrentRow.Cells["MaCa"].Value.ToString();
            txtgioitinh.Text = dataGridView_TKNV.CurrentRow.Cells["GioiTinh"].Value.ToString();
            txtnamsinh.Text = dataGridView_TKNV.CurrentRow.Cells["NamSinh"].Value.ToString();
            txtsdt.Text = dataGridView_TKNV.CurrentRow.Cells["SDT"].Value.ToString();
        }
        private void ResetValues()
        {
            txttenNV.Text = "";
            cbogioitinh.Text = "";
            cbomaca.Text = "";
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            string sql;
            if ((txttenNV.Text == "") && (cbomaca.Text == "")&&(cbogioitinh.Text==""))
            {
                MessageBox.Show("Hãy nhập một điều kiện tìm kiếm!!!", "Yêu cầu ...", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            sql = "SELECT * FROM NhanVien WHERE 1=1";
            if (txttenNV.Text != "")
                sql = sql + " AND TenNV Like '%" + txttenNV.Text + "%' ";
            if (cbogioitinh.Text != "")
                sql = sql + " AND GioiTinh Like '%" + cbogioitinh.Text + "%'";
            if (cbomaca.Text != "")
                sql = sql + " AND MaCa Like '%" + cbomaca.Text + "%'";
            tableTKNV = Class.functions.GetDataToTable(sql);
            if (tableTKNV.Rows.Count == 0)
            {
                MessageBox.Show("Không có bản ghi thỏa mãn điều kiện!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
                MessageBox.Show("Có " + tableTKNV.Rows.Count + " bản ghi thỏa mãn điều kiện!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            tableTKNV = Class.functions.GetDataToTable(sql);
            dataGridView_TKNV.DataSource = tableTKNV;
            ResetValues();
        }

        private void btnTimLai_Click(object sender, EventArgs e)
        {
            ResetValues();
            dataGridView_TKNV.DataSource = null;
        }

        private void btnHienThi_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có muốn hiển thị thông tin chi tiết?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)

            {
                string sql;
                sql = "SELECT * FROM NhanVien";
                DataTable tblMT = Class.functions.GetDataToTable(sql);
                dataGridView_TKNV.DataSource = tblMT;
            }
        }

        private void btnDong_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        
    }
}

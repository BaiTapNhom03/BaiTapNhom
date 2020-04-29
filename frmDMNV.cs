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
    public partial class frmDMNV : Form
    {
        DataTable tableNhanVien;
        public frmDMNV()
        {
            InitializeComponent();
        }

        private void frmDMNV_Load(object sender, EventArgs e)
        {
            Class.functions.Connect();
            txtmanv.Enabled = false;
            btnLuu.Enabled = false;
            btnHuy.Enabled = false;
            Class.functions.FillCombo("SELECT MaCa FROM CaLam", cbomaca, "MaCa", "MaCa");
            cbomaca.SelectedIndex = -1;
            Class.functions.FillCombo("SELECT DISTINCT GioiTinh FROM NhanVien", cbogt, "GioiTinh", "GioiTinh");
            cbogt.SelectedIndex = -1;
            loadDataToGridView();
            ResetValues();
        }
        private void loadDataToGridView()
        {
            string sql = "select * from NhanVien";
            tableNhanVien = Class.functions.GetDataToTable(sql);
            dataGridView_NV.DataSource = tableNhanVien;
        }
        private void ResetValues()
        {
            txtmanv.Text = "";
            txttennv.Text = "";
            txtdiachi.Text = "";
            cbomaca.Text = "";
            cbogt.Text = "";
            masksdt.Text = "";
            msknamsinh.Text = "";
        }

        private void dataGridView_NV_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (btnThem.Enabled == false)
            {
                MessageBox.Show("Đang ở chế độ thêm mới!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtmanv.Focus();
                return;
            }
            txtmanv.Text = dataGridView_NV.CurrentRow.Cells["MaNV"].Value.ToString();
            txttennv.Text = dataGridView_NV.CurrentRow.Cells["TenNV"].Value.ToString();
            txtdiachi.Text = dataGridView_NV.CurrentRow.Cells["DiaChi"].Value.ToString();
            cbomaca.Text = dataGridView_NV.CurrentRow.Cells["MaCa"].Value.ToString();
            cbogt.Text = dataGridView_NV.CurrentRow.Cells["GioiTinh"].Value.ToString();
            masksdt.Text = dataGridView_NV.CurrentRow.Cells["SDT"].Value.ToString();
            msknamsinh.Text = dataGridView_NV.CurrentRow.Cells["NamSinh"].Value.ToString();
            btnSua.Enabled = true;
            btnXoa.Enabled = true;
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
            btnHuy.Enabled = true;
            btnLuu.Enabled = true;
            btnThem.Enabled = false;
            txtmanv.Enabled = true;
            txtmanv.Focus();
            ResetValues();
            loadDataToGridView();
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            string sql;
            if (txtmanv.Text == "")
            {
                MessageBox.Show(" Bạn cần nhập mã nhân viên", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtmanv.Focus();
                return;
            }
            if (txttennv.Text == "")
            {
                MessageBox.Show(" Bạn cần nhập tên nhân viên", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txttennv.Focus();
                return;
            }
            if (cbogt.Text == "")
            {
                MessageBox.Show(" Bạn cần nhập giới tính", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cbogt.Focus();
                return;
            }
            if (cbomaca.Text == "")
            {
                MessageBox.Show(" Bạn cần nhập mã ca", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cbomaca.Focus();
                return;
            }
            if (msknamsinh.Text == "  /  /")
            {
                MessageBox.Show(" Bạn cần nhập năm sinh", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                msknamsinh.Focus();
                return;

            }
            if (!Class.functions.IsDate(msknamsinh.Text))
            {
                MessageBox.Show("Bạn phải nhập lại năm sinh theo dạng dd/mm/yyyy", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                msknamsinh.Text = "";
                msknamsinh.Focus();
                return;
            }
            if (masksdt.Text == "(   )    -")
            {
                MessageBox.Show("Bạn cần nhập SĐT", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                masksdt.Focus();
                return;
            }
            sql = "select * from NhanVien where MaNV='" + txtmanv.Text + "'";
            if (Class.functions.CheckKey(sql) == true)
            {
                MessageBox.Show("Mã nhân viên này đã có, hãy nhập mã khác", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtmanv.Focus();
                txtmanv.Text = "";
                return;
            }
            sql = "INSERT INTO NhanVien(MaNV,TenNV,MaCa, NamSinh,GioiTinh, DiaChi,SDT) VALUES('" + txtmanv.Text.Trim() + "'," +
                "'" + txttennv.Text.Trim() + "','" + cbomaca.Text + "','" + Class.functions.ConvertDateTime(msknamsinh.Text)+ "'," +
                "'" + cbogt.Text + "','" +txtdiachi.Text+ "','"+masksdt.Text+"')";
            Class.functions.RunSQL(sql);
            loadDataToGridView();
            ResetValues();
            btnXoa.Enabled = true;
            btnThem.Enabled = true;
            btnSua.Enabled = true;
            btnHuy.Enabled = false;
            btnLuu.Enabled = false;
            txtmanv.Enabled = false;
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            string sql;
            if (tableNhanVien.Rows.Count == 0)
            {
                MessageBox.Show("Không còn dữ liệu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (txtmanv.Text == "")
            {
                MessageBox.Show("Bạn chưa chọn bản ghi nào", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (txttennv.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập tên nhân viên", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txttennv.Focus();
                return;
            }
            if (masksdt.Text == "(   )    -")
            {
                MessageBox.Show("Bạn phải nhập SĐT", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                masksdt.Focus();
                return;
            }
            if (msknamsinh.Text == "  /  /")
            {
                MessageBox.Show("Bạn phải nhập ngày sinh", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                msknamsinh.Focus();
                return;
            }
            if (!Class.functions.IsDate(msknamsinh.Text))
            {
                MessageBox.Show("Bạn phải nhập lại năm sinh theo dạng dd/mm/yyyy", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                msknamsinh.Text = "";
                msknamsinh.Focus();
                return;
            }
            if (cbogt.Text == "")
            {
                MessageBox.Show(" Bạn cần nhập giới tính", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cbogt.Focus();
                return;
            }
            if (cbomaca.Text == "")
            {
                MessageBox.Show(" Bạn cần nhập mã ca", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cbomaca.Focus();
                return;
            }
            sql = "UPDATE NhanVien SET  TenNV='" + txttennv.Text.Trim().ToString() + "',MaCa='" + cbomaca.Text + "',NamSinh='" + Class.functions.ConvertDateTime(msknamsinh.Text) + "'," +
                "GioiTinh='" + cbogt.Text + "',DiaChi='" + txtdiachi.Text + "',SDT='" + masksdt.Text + "' WHERE MaNV='" + txtmanv.Text + "'";
            Class.functions.RunSQL(sql);
            loadDataToGridView();
            ResetValues();
            btnHuy.Enabled = false;
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            string sql;
            if (tableNhanVien.Rows.Count == 0)
            {
                MessageBox.Show("Không còn dữ liệu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (txtmanv.Text == "")
            {
                MessageBox.Show("Bạn chưa chọn bản ghi nào", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (MessageBox.Show("Bạn có muốn xóa không?", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                sql = "DELETE NhanVien WHERE MaNV=N'" + txtmanv.Text + "'";
                Class.functions.RunSqlDel(sql);
                loadDataToGridView();
                ResetValues();
            }
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            ResetValues();
            btnHuy.Enabled = false;
            btnThem.Enabled = true;
            btnXoa.Enabled = true;
            btnSua.Enabled = true;
            btnLuu.Enabled = false;
            txtmanv.Enabled = false;
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

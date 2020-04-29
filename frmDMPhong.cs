using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using btlquanlycuahanginternet.Class;
using System.Data.SqlClient;

namespace btlquanlycuahanginternet
{
    public partial class frmDMPhong : Form
    {
        public frmDMPhong()
        {
            InitializeComponent();
        }

        private void frmDMPhong_Load(object sender, EventArgs e)
        {
            Class.functions.Connect();
            loadDataToGridView();
        }
        private void loadDataToGridView()
        {
            string sql = " select * from Phong";
            DataTable tablePhong= Class.functions.GetDataToTable(sql);
            dataGridView_Phong.DataSource = tablePhong;
        }

        private void dataGridView_Phong_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtMaPhong.Text = dataGridView_Phong.CurrentRow.Cells["MaPhong"].Value.ToString();
            txtTenPhong.Text = dataGridView_Phong.CurrentRow.Cells["TenPhong"].Value.ToString();
            txtSoMay.Text = dataGridView_Phong.CurrentRow.Cells["SoMay"].Value.ToString();
            txtMaPhong.Enabled = false;
        }
        private void ResetValues()
        {
            txtMaPhong.Text = "";
            txtTenPhong.Text = "";
            txtSoMay.Text = "";
        }

        private void txtSoMay_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (((e.KeyChar >= '0') && (e.KeyChar <= '9')) ||
            (e.KeyChar == '.') || (Convert.ToInt32(e.KeyChar) == 8) || (Convert.ToInt32(e.KeyChar) == 13))
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            ResetValues();
            btnThem.Enabled = false;
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
            btnLuu.Enabled = true;
            btnHuy.Enabled = true;
            txtMaPhong.Enabled = true;
            txtMaPhong.Focus();
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            string sql;
            if (txtMaPhong.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập mã phòng", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMaPhong.Focus();
                return;
            }
            if (txtTenPhong.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn cần nhập tên phòng", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTenPhong.Focus();
                return;
            }
            if (txtSoMay.Text == "")
            {
                MessageBox.Show("Bạn cần nhập đơn giá", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtSoMay.Focus();
                return;
            }
            sql = "SELECT MaPhong FROM Phong WHERE MaPhong='" + txtMaPhong.Text + "'";
            if (Class.functions.CheckKey(sql))
            {
                MessageBox.Show("Mã phòng này đã có, bạn phải nhập mã khác", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMaPhong.Focus();
                txtMaPhong.Text = "";
                return;
            }
            sql = "INSERT INTO Phong(MaPhong,TenPhong,SoMay) VALUES('" +txtMaPhong.Text + "','" + txtTenPhong.Text + "','"+txtSoMay.Text+"')";
            Class.functions.RunSQL(sql);
            loadDataToGridView();
            ResetValues();
            btnXoa.Enabled = true;
            btnThem.Enabled = true;
            btnSua.Enabled = true;
            btnHuy.Enabled = false;
            btnLuu.Enabled = false;
            txtMaPhong.Enabled = false;
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            string sql;
            if (txtMaPhong.Text == "")
            {
                MessageBox.Show("Bạn chưa chọn bản ghi nào", "Thông báo",MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (txtTenPhong.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập tên phòng", "Thông báo",MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTenPhong.Focus();
                return;
            }
            if (txtSoMay.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập số máy", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtSoMay.Focus();
                return;
            }
            sql = "UPDATE Phong SET TenPhong='" + txtTenPhong.Text.ToString() +"',SoMay='"+txtSoMay.Text+"' WHERE MaPhong='" + txtMaPhong.Text + "'";
            Class.functions.RunSQL(sql);
            loadDataToGridView();
            ResetValues();
            btnHuy.Enabled = false;

        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            ResetValues();
            btnHuy.Enabled = false;
            btnThem.Enabled = true;
            btnXoa.Enabled = true;
            btnSua.Enabled = true;
            btnLuu.Enabled = false;
            txtMaPhong.Enabled = false;
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            string sql;
            if (txtMaPhong.Text == "")
            {
                MessageBox.Show("Bạn chưa chọn bản ghi nào", "Thông báo",MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (MessageBox.Show("Bạn có muốn xóa không?", "Thông báo",MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                sql = "DELETE Phong WHERE MaPhong='" + txtMaPhong.Text + "'";
                Class.functions.RunSqlDel(sql);
                loadDataToGridView();
                ResetValues();
            }
        }
        private void btnDong_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

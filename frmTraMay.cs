using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using btlquanlycuahanginternet.Class;


namespace btlquanlycuahanginternet
{
    public partial class frmTraMay : Form
    {
        DataTable tableTM;
        public string Message, Message1, Message2, Message3, Message4, Message5;
        public frmTraMay()
        {
            InitializeComponent();
        }
        private void frmTraMay_Load(object sender, EventArgs e)
        {
            Class.functions.Connect();
            Class.functions.FillCombo("select MaNV, TenNV from NhanVien ", cboMaNV, "MaNV", "MaNV");
            cboMaNV.SelectedIndex = -1;
            loadDataToGridView();
            txtGioVao.Text = Message;
            txtMaPhong.Text = Message1;
            txtTenKhach.Text =Message2;
            txtMaMay.Text = Message3;
            txtNgayThue.Text = Message4;
            txtMaSTT.Text = Message5;
            txtMaPhong.ReadOnly = true;
            txtMaMay.ReadOnly = true;
            txtTenKhach.ReadOnly = true;
            txtNgayThue.ReadOnly = true;
            txtThanhToan.ReadOnly = true;
            txtGioVao.ReadOnly = true;
            txtMaSTT.ReadOnly = true;
        }
        private void loadDataToGridView()
        {
            string sql = " select  MaSTT,MaPhong , MaMay , TenKhach ,NgayThue, GioVao, GioRa,MaNV,TongTien from ThueMay  ";
            tableTM = Class.functions.GetDataToTable(sql);
            dataGridView_TraMay.DataSource = tableTM;
        }
        private void dataGridView_TraMay_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtMaSTT.Text = dataGridView_TraMay.CurrentRow.Cells["MaSTT"].Value.ToString();
            txtMaPhong.Text = dataGridView_TraMay.CurrentRow.Cells["MaPhong"].Value.ToString();
            txtMaMay.Text = dataGridView_TraMay.CurrentRow.Cells["MaMay"].Value.ToString();
            txtGioVao.Text = dataGridView_TraMay.CurrentRow.Cells["GioVao"].Value.ToString();
            txtGioRa.Text = dataGridView_TraMay.CurrentRow.Cells["GioRa"].Value.ToString();
            txtTenKhach.Text = dataGridView_TraMay.CurrentRow.Cells["TenKhach"].Value.ToString();
            txtThanhToan.Text = dataGridView_TraMay.CurrentRow.Cells["TongTien"].Value.ToString();
            txtNgayThue.Text = dataGridView_TraMay.CurrentRow.Cells["NgayThue"].Value.ToString();
            cboMaNV.Text = dataGridView_TraMay.CurrentRow.Cells["MaNV"].Value.ToString();
        }
        public void resetvalues()
        {
            txtGioRa.Text = "";
            txtGioVao.Text = "";
            txtMaMay.Text = "";
            txtMaPhong.Text = "";
            txtNgayThue.Text = "";
            txtTenKhach.Text = "";
            txtThanhToan.Text = "0";
            cboMaNV.Text = "";
            txtMaSTT.Text = "";
        }
        private void btnThanhToan_Click(object sender, EventArgs e)
        {
            DateTime endtime = DateTime.Now.AddSeconds(75);
            txtGioRa.Text = endtime.ToString();
            if (txtMaSTT.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập mã Số thứ tự của khách hàng ", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMaSTT.Focus();
                return;
            }
            if (cboMaNV.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập mã nhân viên ", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cboMaNV.Focus();
                return;
            }
            txtGioRa.Text = DateTime.Now.AddSeconds(75).TimeOfDay.ToString();
            double tt;
            DateTime GioRa = DateTime.Now.AddSeconds(75);
            DateTime GioVao = DateTime.Parse(txtGioVao.Text);
            TimeSpan interval = GioRa.Subtract(GioVao);
            MessageBox.Show(" thời gian bạn sử dụng máy tính: " + interval.Hours + " tiếng," + interval.Minutes + " phút", " thông báo"
                , MessageBoxButtons.OK, MessageBoxIcon.Information);
            if (interval.Hours <= 1)
            {
                tt = 6000;
            }
            else
            {
                tt = interval.Hours * 6000 + interval.Minutes * 100;
            }
            txtThanhToan.Text = tt.ToString();
            MessageBox.Show(" số tiền bạn cần thanh toán là: " + tt, " thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        private void btnLuu_Click(object sender, EventArgs e)
        {
            
            string sql;

            sql = " insert into ThueMay(MaSTT,MaPhong,MaMay,TenKhach,NgayThue, GioVao, GioRa,MaNV,TongTien) values ('" + txtMaSTT.Text + "','" + txtMaPhong.Text + "','" + txtMaMay.Text + "','" + txtTenKhach.Text + "','" +
                DateTime.Parse(txtNgayThue.Text) + "','" + DateTime.Parse(txtGioVao.Text) + "','" + DateTime.Parse(txtGioRa.Text) + "','" + cboMaNV.SelectedValue + "','" + txtThanhToan.Text + "')";
            Class.functions.RunSQL(sql);
            resetvalues();
            loadDataToGridView();
        }
        private void btnXoa_Click(object sender, EventArgs e)
        {
            string sql;
            if (txtMaSTT.Text == "")
            {
                MessageBox.Show("Bạn chưa chọn bản ghi nào", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (MessageBox.Show("Bạn có muốn xoá bản ghi này không?", "Thông báo",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                sql = "DELETE ThueMay WHERE MaSTT='" + txtMaSTT.Text + "'";
                Class.functions.RunSqlDel(sql);
                loadDataToGridView();
                resetvalues();
            }
        }
        private void btnDong_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}


using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Quanlythusech
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        public static SqlConnection conn = new SqlConnection();
        public static string connectstring = "Data Source=LAPTOP-6SL7VIMI\\SQLEXPRESS;Initial Catalog=cuahangchothuesach;Integrated Security=True";
        public static void ketnoi()
        {
            conn = new SqlConnection();
            conn.ConnectionString = connectstring;
            if (conn.State != ConnectionState.Open)
            {
                conn.Open();
            }
        }
        public static void Dongketnoi()
        {

            if (conn.State != ConnectionState.Closed)
            {
                conn.Close();
                conn.Dispose();
                conn = null;
            }
        }
        public static DataTable Docbang(string sql)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter mydata = new SqlDataAdapter();
            mydata.SelectCommand = new SqlCommand();
            ketnoi();
            mydata.SelectCommand.Connection = conn;
            mydata.SelectCommand.CommandText = sql;
            mydata.Fill(dt);
            Dongketnoi();
            return dt;
        }
        public static void capnhat(string sql)
        {
            ketnoi();
            SqlCommand sqlcomand = new SqlCommand();
            sqlcomand.Connection = conn;
            sqlcomand.CommandText = sql;
            sqlcomand.ExecuteNonQuery();
            Dongketnoi();
        }
        private void hienthi()
        {
            string sql;
            DataTable tabletacgia;
            sql = "select * from tacgia";
            // dataGridView_Form1.DataSource = tabletacgia;
            dataGridView1.Columns[0].HeaderText = " Mã tác giả";
            dataGridView1.Columns[1].HeaderText = "Tên tác giả";
            dataGridView1.Columns[2].HeaderText = "Ngày sinh";
            dataGridView1.Columns[3].HeaderText = "Địa chỉ";
           
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.EditMode = DataGridViewEditMode.EditProgrammatically;
            // tabletacgia.Dispose();
        }
       

        private void btnThem_Click(object sender, EventArgs e)
        {
            txtMaTG.Enabled = false;
            txtTenTG.Enabled = false;
            txtNgaySinh.Enabled = false;
            txtDiaChi.Enabled = false;
            txtGioiTinh.Enabled = false;
            btnLuu.Enabled = false;
            btnSua.Enabled = true;
            btnThem.Enabled = true;
            btnXoa.Enabled = true;

        }
        private void Form1_Activated(object sender, EventArgs e)
        {
            hienthi();
        }

        private void DataGridView_Form1_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            //hienthi();
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            string sql;
            //Form f = new Form();
            //f.StartPosition = FormStartPosition.CenterScreen;
            txtMaTG.Enabled = false;
            txtTenTG.Enabled = false;
            txtNgaySinh.Enabled = false;
            txtDiaChi.Enabled = false;
            txtGioiTinh.Enabled = false;
            txtMaTG.Text = dataGridView1.CurrentRow.Cells["matacgia"].Value.ToString();
            txtTenTG.Text = dataGridView1.CurrentRow.Cells["tentacgia"].Value.ToString();
            txtNgaySinh.Text = dataGridView1.CurrentRow.Cells["ngaysinh"].Value.ToString();
            txtDiaChi.Text = dataGridView1.CurrentRow.Cells["điachi"].Value.ToString();
            txtGioiTinh.Text = dataGridView1.CurrentRow.Cells["gioitinh"].Value.ToString();

            if (txtTenTG.Text == "")
            {
                MessageBox.Show("Bạn chưa nhập tên tác giả!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (txtTenTG.Text.Trim().Length == 0)
            {
                MessageBox.Show("bạn phải nhập tên tác giả!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTenTG.Focus();
                return;
            }

            // hienthi();
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            string sql;
            if (txtTenTG.Text == "" && txtTenTG.Text == "")
            {
                MessageBox.Show("Bạn không được để trống ", "Thông báo");
                txtTenTG.Focus();
            }
            else
            {
                sql = "select * from tentacgia where Form1=N'" + txtTenTG.Text.Trim() + "'";
                sql = "update tacgia set tentacgia=N'" + txtTenTG.Text.Trim() + "'where matacgia=N'" + txtMaTG.Text.Trim() + "'where ngaysinh=N" + txtNgaySinh.Text.Trim() + "whwwhere diachi=N'" + txtDiaChi.Text.Trim() + "'where gioitinh=N" + txtGioiTinh.Text.Trim();
                capnhat(sql);
                txtMaTG.Enabled = false;
                txtTenTG.Enabled = false;
                txtDiaChi.Enabled = false;
                txtNgaySinh.Enabled = false;
                txtGioiTinh.Enabled = false;
                txtMaTG.Text = "";
                txtTenTG.Text = "";
                txtDiaChi.Text = "";
                txtNgaySinh.Text = "";
                txtGioiTinh.Text = "";
            }
        }
        private void btnXoa_Click(object sender, EventArgs e)
        {
            string sql;
            if (MessageBox.Show("Bạn có muốn xóa không?", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {

                sql = " delete tacgia where matacgia=N'" + txtMaTG.Text + "'";
                capnhat(sql);
                hienthi();
            }
        }
    }
}


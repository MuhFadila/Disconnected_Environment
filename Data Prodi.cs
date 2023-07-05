using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Disconnected_Environment
{
    public partial class Data_Prodi : Form
    {
        string connectionString = "data source=LAPTOP-U3C1SDFR\\MUHAMMADFADILA;database=Activity_6;MultipleActiveResultSets=True;User ID = sa; Password = 123";
        private SqlConnection koneksi;
        public Data_Prodi()
        {
            InitializeComponent();
            koneksi = new SqlConnection(connectionString);
            refreshform();
        }

        private void refreshform()
        {
            textBox1.Text = "";
            textBox1.Enabled = false;
            BtnSave.Enabled = false;
            BtnClear.Enabled = false;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void BtnOpen_Click(object sender, EventArgs e)
        {
            dataGridView();
            BtnOpen.Enabled = false;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            string nmProdi = textBox1.Text;
            if (nmProdi == "")
            {
                MessageBox.Show("Masukkan Nama Prodi", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                koneksi.Open();
                string str = "insert into dbo. Prodi (nama_prodi)" + "values (@id)";
                SqlCommand cmd = new SqlCommand(str, koneksi);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.Add(new SqlParameter("id", nmProdi));
                cmd.ExecuteNonQuery();
                koneksi.Close();
                MessageBox.Show("Data Berhasil Disimpan", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.
                Information);
                dataGridView();
                refreshform();
            }
        }

        private void BtnClear_Click(object sender, EventArgs e)
        {
            refreshform();
        }

        private void dataGridView()
        {
            koneksi.Open();
            string str = "select nama_prodi, id_prodi from dbo.prodi";
            SqlDataAdapter da = new SqlDataAdapter(str, koneksi);
            DataSet ds = new DataSet();
            da.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
            koneksi.Close();
        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            textBox1.Enabled = true;
            BtnSave.Enabled = true;
            BtnClear.Enabled = true;
            label1.Enabled = true;
        }
        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void Close_Click(object sender, EventArgs e)
        {
            Form1 hu = new Form1();
            hu.Show();
            this.Hide();
        }

        private void Data_Prodi_Load(object sender, EventArgs e)
        {

        }
    }
}

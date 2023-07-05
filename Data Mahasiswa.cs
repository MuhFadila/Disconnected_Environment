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
    public partial class Data_Mahasiswa : Form
    {
        private string stringConnection = "data source=LAPTOP-U3C1SDFR\\MUHAMMADFADILA;database=Activity_6;MultipleActiveResultSets=True;User ID = sa; Password = 123";
        private SqlConnection koneksi;
        private string nim, nama, alamat, jk, prodi;
        private DateTime tgl;
        BindingSource customersBindingSource = new BindingSource();
        public Data_Mahasiswa()
        {
            InitializeComponent();
            koneksi = new SqlConnection(stringConnection);
            this.bindingNavigator1.BindingSource = this.customersBindingSource;
            refreshform();
        }

         private void refreshform()
        {
            textBoxNim.Enabled = false;
            textBoxNama.Enabled = false;
            comboBoxJK.Enabled = false;
            textBoxAlamat.Enabled = false;
            dateTimePicker.Enabled = false;
            comboBoxProdi.Enabled = false;
            BtnAdd.Enabled = true;
            BtnSave.Enabled = false;
            BtnClear.Enabled = false;
            clearBinding();
            FormDataMahasiswa_Load();
        }

        private void FormDataMahasiswa_Load()
        {
            koneksi.Open();
            SqlDataAdapter dataAdapter1 = new SqlDataAdapter(new SqlCommand("SELECT nim, nama_mahasiswa, jenis_kelamin, alamat, tgl_lahir, id_prodi FROM mahasiswa", koneksi));
            DataSet ds = new DataSet();
            dataAdapter1.Fill(ds);

            this.customersBindingSource.DataSource = ds.Tables[0];
            this.textBoxNim.DataBindings.Add(
                new Binding("Text", this.customersBindingSource, "nim", true));
            this.textBoxNama.DataBindings.Add(
                new Binding("Text", this.customersBindingSource, "nama_mahasiswa", true));
            this.textBoxAlamat.DataBindings.Add(
                new Binding("Text", this.customersBindingSource, "alamat", true));
            this.comboBoxJK.DataBindings.Add(
                new Binding("Text", this.customersBindingSource, "jenis_kelamin", true));
            this.dateTimePicker.DataBindings.Add(
                new Binding("Text", this.customersBindingSource, "tgl_lahir", true));
            this.comboBoxProdi.DataBindings.Add(
                new Binding("Text", this.customersBindingSource, "id_prodi", true));
            koneksi.Close();
        }

        private void clearBinding()
        {
            this.textBoxNim.DataBindings.Clear();
            this.textBoxNama.DataBindings.Clear();
            this.textBoxAlamat.DataBindings.Clear();
            this.comboBoxJK.DataBindings.Clear();
            this.dateTimePicker.DataBindings.Clear();
            this.comboBoxProdi.DataBindings.Clear();
        }
        private void Data_Mahasiswa_Load(object sender, EventArgs e)
        {

        }

        private void textBoxNim_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBoxNama_TextChanged(object sender, EventArgs e)
        {

        }

        private void comboBoxJK_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void textBoxAlamat_TextChanged(object sender, EventArgs e)
        {

        }

        private void dateTimePicker_ValueChanged(object sender, EventArgs e)
        {

        }

        private void bindingNavigator1_RefreshItems(object sender, EventArgs e)
        {

        }

        private void comboBoxProdi_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
            Form1 fm = new Form1();
            fm.Show();
            this.Hide();
        }
        private void BtnAdd_Click(object sender, EventArgs e)
        {
            textBoxNim.Text = "";
            textBoxNama.Text = "";
            textBoxAlamat.Text = "";
            dateTimePicker.Value = DateTime.Today;
            textBoxNim.Enabled = true;
            textBoxNama.Enabled = true;
            comboBoxJK.Enabled = true;
            textBoxAlamat.Enabled = true;
            textBoxAlamat.Enabled = true;
            dateTimePicker.Enabled = true;
            comboBoxProdi.Enabled = true;
            Prodicbx();
            BtnSave.Enabled = true;
            BtnClear.Enabled = true;
            BtnAdd.Enabled = false;
        }

        private void Prodicbx()
        {
            koneksi.Open();
            string str = "SELECT id_prodi, nama_prodi FROM dbo.prodi";
            SqlCommand cmd = new SqlCommand(str, koneksi);
            SqlDataAdapter da = new SqlDataAdapter(str, koneksi);
            DataSet ds = new DataSet();
            da.Fill(ds);
            cmd.ExecuteReader();
            koneksi.Close();
            comboBoxProdi.DisplayMember = "nama_prodi";
            comboBoxProdi.ValueMember = "id_prodi";
            comboBoxProdi.DataSource = ds.Tables[0];
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            nim = textBoxNim.Text.Trim();
            nama = textBoxNama.Text.Trim();
            alamat = textBoxAlamat.Text.Trim();
            jk = comboBoxJK.SelectedItem.ToString();
            prodi = comboBoxProdi.SelectedValue.ToString();
            tgl = dateTimePicker.Value;

            if (string.IsNullOrEmpty(nim) || string.IsNullOrEmpty(nama) || string.IsNullOrEmpty(alamat) || string.IsNullOrEmpty(jk) || string.IsNullOrEmpty(prodi))
            {
                MessageBox.Show("Please fill in all identity fields!");
            }
            else
            {
                koneksi.Open();
                string query = "INSERT INTO mahasiswa (nim, nama_mahasiswa, alamat, jenis_kelamin, id_prodi, tgl_lahir) VALUES (@nim, @nama_mahasiswa, @alamat, @jenis_kelamin, @id_prodi, @tgl_lahir)";
                SqlCommand command = new SqlCommand(query, koneksi);
                command.Parameters.AddWithValue("@nim", nim);
                command.Parameters.AddWithValue("@nama_mahasiswa", nama);
                command.Parameters.AddWithValue("@alamat", alamat);
                command.Parameters.AddWithValue("@jenis_kelamin", jk);
                command.Parameters.AddWithValue("@id_prodi", prodi);
                command.Parameters.AddWithValue("@tgl_lahir", tgl);
                command.ExecuteNonQuery();
                koneksi.Close();

                MessageBox.Show("Data has been saved to the database.");
            }
            refreshform();
        }


        private void BtnClear_Click(object sender, EventArgs e)
        {
            refreshform();
        }
    }
}

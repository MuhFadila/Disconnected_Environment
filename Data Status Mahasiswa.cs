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
    public partial class Data_Status_Mahasiswa : Form
    {
        string connectionString = "data source=LAPTOP-U3C1SDFR\\MUHAMMADFADILA;database=Activity_6;MultipleActiveResultSets=True;User ID = sa; Password = 123";
        private SqlConnection koneksi;
        public Data_Status_Mahasiswa()
        {
            InitializeComponent();
            koneksi = new SqlConnection(connectionString);
            refreshform();
        }

        private void refreshform()
        {
            comboBox1.Enabled = false;
            comboBox2.Enabled = false;
            comboBox3.Enabled = false;
            comboBox1.SelectedIndex = -1;
            comboBox2.SelectedIndex = -1;
            comboBox3.SelectedIndex = -1;
            label5.Visible = false;
            buttonSAVE.Enabled = false;
            buttonCLEAR.Enabled = false;
            buttonADD.Enabled = true;

        }

        private void cbNama()
        {
            koneksi.Open();
            string str = "select nama_mahasiswa from dbo.mahasiswa where " +
                "not EXISTS(select id_status from dbo.status_mahasiswa where " +
                "status_mahasiswa.nim = mahasiswa.nim)";
            SqlCommand cmd = new SqlCommand(str, koneksi);
            SqlDataAdapter da = new SqlDataAdapter(str, koneksi);
            DataSet ds = new DataSet();
            da.Fill(ds);
            cmd.ExecuteReader();
            koneksi.Close();

            comboBox1.DisplayMember = "nama_mahasiswa";
            comboBox1.ValueMember = "Nim";
            comboBox1.DataSource = ds.Tables[0];
        }

        private void cbTahunMasuk()
        {
            int y = DateTime.Now.Year - 2010;
            string[] type = new string[y];
            int i = 0;
            for (i = 0; i < type.Length; i++)
            {
                if (i == 0)
                {
                    comboBox3.Items.Add("2010");
                }
                else
                {
                    int l = 2010 + i;
                    comboBox3.Items.Add(l.ToString());
                }
            }
        }

        private void dataGridView()
        {
            koneksi.Open();
            string str = "select * from dbo.status_mahasiswa";
            SqlDataAdapter da = new SqlDataAdapter(str, koneksi);
            DataSet ds = new DataSet();
            da.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
            koneksi.Close();
        }

        private void Data_Status_Mahasiswa_Load(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            koneksi.Open();
            string nim = "";
            string strs = "select NIM from dbo.Mahasiswa where nama_mahasiswa = @nm";
            SqlCommand cm = new SqlCommand(strs, koneksi);
            cm.CommandType = CommandType.Text;
            cm.Parameters.Add(new SqlParameter("@nm", comboBox1.Text));
            SqlDataReader dr = cm.ExecuteReader();
            while (dr.Read())
            {
                nim = dr["NIM"].ToString();
            }
            dr.Close();
            koneksi.Close();

            label5.Text = nim;
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void buttonADD_Click(object sender, EventArgs e)
        {
            comboBox3.Enabled = true;
            comboBox1.Enabled = true;
            comboBox2.Enabled = true;
            label5.Visible = true;
            cbTahunMasuk();
            cbNama();
            comboBox3.Enabled = true;
            buttonSAVE.Enabled = true;
            buttonADD.Enabled = false;
        }

        private void buttonCLEAR_Click(object sender, EventArgs e)
        {
            refreshform();
        }

        private void buttonSAVE_Click(object sender, EventArgs e)
        {
            string nim = comboBox1.Text;
            string statusmahasiswa = comboBox2.Text;
            string tahunmasuk = comboBox3.Text;
            int count = 0;
            string tempkodestatus = "";
            string kodestatus = "";
            koneksi.Open();

            string str = "select count (*) from dbo.status_mahasiswa";
            SqlCommand cm = new SqlCommand(str, koneksi);
            count = (int)cm.ExecuteScalar();

            if (count == 0)
            {
                kodestatus = "1";
            }
            else
            {
                string querryString = "select max(id_status) from dbo.status_mahasiswa";
                SqlCommand cmStatusMahasiswaSum = new SqlCommand(str, koneksi);
                int totalStatusMahasiswa = (int)cmStatusMahasiswaSum.ExecuteScalar();
                int finalkodestatusint = totalStatusMahasiswa + 1;
                kodestatus = Convert.ToString(finalkodestatusint);
            }
            string queryString = "insert into dbo.status_mahasiswa (id_status, nim, " +
                "status_mahasiswa, tahun_masuk)" + "values(@ids, @Nim, @sm, @tm)";
            SqlCommand cmd = new SqlCommand(queryString, koneksi);
            cmd.CommandType = CommandType.Text;

            cmd.Parameters.Add(new SqlParameter("ids", kodestatus));
            cmd.Parameters.Add(new SqlParameter("Nim", nim));
            cmd.Parameters.Add(new SqlParameter("sm", statusmahasiswa));
            cmd.Parameters.Add(new SqlParameter("tm", tahunmasuk));
            cmd.ExecuteNonQuery();
            koneksi.Close();

            MessageBox.Show("data berhasil disimpan", "sukses", MessageBoxButtons.OK,
                MessageBoxIcon.Information);
            refreshform();
            dataGridView();
        }

        private void buttonOPEN_Click(object sender, EventArgs e)
        {
            dataGridView();
            buttonOPEN.Enabled = false;
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }
    }
}

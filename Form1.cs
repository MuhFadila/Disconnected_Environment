using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Disconnected_Environment
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void toolStripTextBox1_Click(object sender, EventArgs e)
        {

        }

        private void dataProdiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Data_Prodi fe = new Data_Prodi();
            fe.Show();
            this.Hide();
        }


        private void dataMahasiswaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Data_Mahasiswa fo = new Data_Mahasiswa();
            fo.Show();
            this.Hide();
        }

        private void dataStatusMahasiswaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Data_Status_Mahasiswa fr = new Data_Status_Mahasiswa();
            fr.Show();
            this.Hide();
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Elaborazione_Dati_CSV
{
    public partial class Form1 : Form
    {
        funzioni f;
        string filename;
        string filename1;
        char delim;
        public int dim;

        public Form1()
        {
            f = new funzioni();
            filename = @"camozzi1.csv";
            filename1 = @"camozzi.csv";
            dim = 0;
            delim = ';';
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            f.RandeLog(filename, filename1, delim);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Il numero di campi presenti nel file CSV è: " + f.contaCampi(filename, delim));
        }

        private void button4_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Il campo più lungo e " + f.Long(filename1, delim) + " e ha " + f.Long(filename1, delim).Length + " caratteri");
        }

        private void button5_Click(object sender, EventArgs e)
        {
            f.spaziatura(filename, filename1);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            groupBox1.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string[] temp = new string[f.contaCampi(filename, delim)];
            string[] split = textBox13.Text.Split();
            temp[0] = textBox2.Text;
            temp[1] = textBox9.Text;
            temp[2] = textBox10.Text;
            temp[3] = textBox11.Text;
            temp[4] = textBox12.Text;
            temp[5] = split[0] + "T" + split[1];
            temp[6] = textBox14.Text;
            temp[7] = textBox15.Text;
            temp[8] = textBox16.Text;
            f.NewRec(filename, delim, temp);
            groupBox1.Hide();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            f.TriRec(filename, delim, listView1);
        }

        private void button8_Click(object sender, EventArgs e)
        {
            f.search(filename1, delim, textBox1.Text);
        }

        private void button10_Click(object sender, EventArgs e)
        {
            f.canc(filename, delim, textBox3.Text, textBox4.Text);
        }

        private void button9_Click(object sender, EventArgs e)
        {
            f.mod(filename, delim, textBox7.Text, textBox8.Text, textBox5.Text, textBox6.Text);
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GMM_FIELD
{
    public partial class Информация_о_сфере : Form
    {
        int i;
        bool f;
        DataGridView dataGridView1;
        public Информация_о_сфере(int i, bool f, DataGridView dataGridView1)
        {
            InitializeComponent();
            this.i = i;
            this.f = f;
            this.dataGridView1 = dataGridView1;
            this.Text = this.Text + " №" + (i+1).ToString();
            if(!f)
            {
                textBox1.Text = dataGridView1[1, i].Value.ToString();
                textBox2.Text = dataGridView1[2, i].Value.ToString();
                textBox3.Text = dataGridView1[3, i].Value.ToString();
                textBox4.Text = dataGridView1[4, i].Value.ToString();
                string s = dataGridView1[5, i].Value.ToString();
                s = s.TrimEnd('i');
                string[] s2 = s.Split(' ');
                textBox5.Text = s2[0];
                textBox6.Text = s2[2];
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
                textBox1.BackColor = Color.Red;
            else
            {
                try
                {
                    double x = Convert.ToDouble(textBox1.Text);
                    textBox1.BackColor = Color.White;
                }
                catch (FormatException)
                {
                    textBox1.BackColor = Color.Red;
                }
            }
            if (textBox2.Text == "")
                textBox2.BackColor = Color.Red;
            else
            {
                try
                {
                    double y = Convert.ToDouble(textBox2.Text);
                    textBox2.BackColor = Color.White;
                }
                catch (FormatException)
                {
                    textBox2.BackColor = Color.Red;
                }
            }
            if (textBox3.Text == "")
                textBox3.BackColor = Color.Red;
            else
            {
                try
                {
                    double z = Convert.ToDouble(textBox3.Text);
                    textBox3.BackColor = Color.White;
                }
                catch (FormatException)
                {
                    textBox3.BackColor = Color.Red;
                }
            }
            if (textBox4.Text == "")
                textBox4.BackColor = Color.Red;
            else
            {
                try
                {
                    double r = Convert.ToDouble(textBox4.Text);
                    textBox4.BackColor = Color.White;
                }
                catch (FormatException)
                {
                    textBox4.BackColor = Color.Red;
                }
            }
            if (textBox5.Text == "")
                textBox5.BackColor = Color.Red;
            else
            {
                try
                {
                    double Re = Convert.ToDouble(textBox5.Text);
                    textBox5.BackColor = Color.White;
                }
                catch (FormatException)
                {
                    textBox5.BackColor = Color.Red;
                }
            }
            if (textBox6.Text == "")
                textBox6.BackColor = Color.Red;
            else
            {
                try
                {
                    double Im = Convert.ToDouble(textBox6.Text);
                    textBox6.BackColor = Color.White;
                }
                catch (FormatException)
                {
                    textBox6.BackColor = Color.Red;
                }
            }
            if (textBox1.BackColor == Color.White && textBox2.BackColor == Color.White && textBox3.BackColor == Color.White && textBox4.BackColor == Color.White && textBox5.BackColor == Color.White && textBox6.BackColor == Color.White)
            {
                if(f)
                {
                    dataGridView1.RowCount++;
                    dataGridView1[0, i].Value = i + 1;
                }
                dataGridView1[1, i].Value = Convert.ToDouble(textBox1.Text);
                dataGridView1[2, i].Value = Convert.ToDouble(textBox2.Text);
                dataGridView1[3, i].Value = Convert.ToDouble(textBox3.Text);
                dataGridView1[4, i].Value = Convert.ToDouble(textBox4.Text);
                if (Convert.ToDouble(textBox6.Text) < 0)
                    dataGridView1[5, i].Value = Convert.ToDouble(textBox5.Text) + " - " + Math.Abs(Convert.ToDouble(textBox6.Text)) + "i";
                if (Convert.ToDouble(textBox6.Text) >= 0)
                    dataGridView1[5, i].Value = Convert.ToDouble(textBox5.Text) + " + " + Convert.ToDouble(textBox6.Text) + "i";
                Close();
            }
            else
                MessageBox.Show("Одно или несколько полей заполнены неверно!", "Ошибка!");
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (f)
            {
                dataGridView1.RowCount++;
                dataGridView1[0, i].Value = i + 1;
            }
            MessageBox.Show("Данные о сфере №" + (i+1).ToString() + " будут заданы по умолчанию!", "Предупреждение!");
            dataGridView1[1, i].Value = "0,0";
            dataGridView1[2, i].Value = "0,0";
            dataGridView1[3, i].Value = "0,0";
            dataGridView1[4, i].Value = "0,01";
            dataGridView1[5, i].Value = "1,5155 + 0,0213i";
            Close();
        }
        private void textBox1_MouseClick(object sender, MouseEventArgs e)
        {
            textBox1.BackColor = Color.White;
        }
        private void textBox2_MouseClick(object sender, MouseEventArgs e)
        {
            textBox2.BackColor = Color.White;
        }
        private void textBox3_MouseClick(object sender, MouseEventArgs e)
        {
            textBox3.BackColor = Color.White;
        }
        private void textBox4_MouseClick(object sender, MouseEventArgs e)
        {
            textBox4.BackColor = Color.White;
        }
        private void textBox5_MouseClick(object sender, MouseEventArgs e)
        {
            textBox5.BackColor = Color.White;
        }
        private void textBox6_MouseClick(object sender, MouseEventArgs e)
        {
            textBox6.BackColor = Color.White;
        }
    }
}

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
    public partial class Otobr : Form
    {
        public Otobr()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            string text = textBox1.Text.Trim();
            textBox1.Text = text.ToString();
            oleDbSelectCommand1.CommandText = "SELECT GMM.Код_сферы, GMM.Количество_сфер, GMM.Описание_рассчета, GMM.[Количество_ориентаций(nbeta)],"
                                            + " GMM.[Количество_ориентаций(nthet)], GMM.[Количество_ориентаций(nphai)], GMM.[Ориентация(betami)],"
                                            + " GMM.[Ориентация(betamx)], GMM.[Ориентация(thetmi)], GMM.[Ориентация(thetmx)], GMM.[Ориентация(phaimi)],"
                                            + " GMM.[Ориентация(phaimx)], GMM.Итерация_по_схеме_X, GMM.Итерация_по_схеме_Y, GMM.Максимальное_количество_итераций,"
                                            + " GMM.Порядок_управления_рассеиванием, GMM.Погрешность_ошибки, GMM.Критерий_сходимости, GMM.Индекс_взаимодействия,"
                                            + " GMM.Длина_волны, GMM.Дата_расчета, GMM.[Время расчета] FROM GMM\r\n"
                                            + "WHERE ((GMM.Количество_сфер LIKE ? + '%'));";
            oleDbSelectCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter());
            oleDbSelectCommand1.Parameters[0].Value = text;
            dataSet31.Clear();
            oleDbDataAdapter1.Fill(dataSet31);
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            string kod = Convert.ToString(dataGridView1[0, dataGridView1.CurrentRow.Index].Value);
            new Result("Result\\result_" + kod + "\\field.txt").ShowDialog();
        }

        private void Otobr_Load(object sender, EventArgs e)
        {
            oleDbConnection1.ConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=|DataDirectory|\\GMM_FIELD1.accdb";
            dataSet31.Clear();
            dataGridView1.DataSource = dataSet31;
            dataGridView1.DataMember = "GMM";
            dataGridView1.Columns[0].Visible = false;
            oleDbDataAdapter1.Fill(dataSet31); 
        }
    }
}

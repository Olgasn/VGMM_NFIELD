using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;
using ZedGraph;

namespace GMM_FIELD
{
    public partial class Result : Form
    {
        bool f = true;
        string field;
        public Result(string field)
        {
            InitializeComponent();
            this.field = field;
            comboBox1.SelectedIndex = 0;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Process.Start(field);
        }
        private void button3_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void zedGraphControl1_MouseClick(object sender, MouseEventArgs e)
        {
            CurveItem curve;
            int index;
            GraphPane pane = zedGraphControl1.GraphPane;
            GraphPane.Default.NearestTol = 10;
            bool result = pane.FindNearestPoint(e.Location, out curve, out index);
            if (result)
            {
                double x, y;
                zedGraphControl1.GraphPane.ReverseTransform(e.Location, out x, out y);
                string text = string.Format("{0}: {1};    {2}: {3}", comboBox1.SelectedItem, x,comboBox2.SelectedItem,y);
                label3.Text = text;
                PointPairList point = new PointPairList();
                point.Add(curve[index]);
                LineItem curvePount = pane.AddCurve("",new double[] { curve[index].X },new double[] { curve[index].Y },Color.Red,SymbolType.Plus);
                curvePount.Line.IsVisible = false;
                curvePount.Symbol.Fill.Color = Color.Blue;
                curvePount.Symbol.Fill.Type = FillType.Solid;
                curvePount.Symbol.Size = 7;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            GraphPane pane = zedGraphControl1.GraphPane;
            pane.XAxis.Scale.MinAuto = true;
            pane.XAxis.Scale.MaxAuto = true;
            pane.YAxis.Scale.MinAuto = true;
            pane.YAxis.Scale.MaxAuto = true;
            zedGraphControl1.AxisChange();
            zedGraphControl1.Invalidate();
        }
        public void Draw()
        {
            label3.Text = "";
            if (f)
                comboBox2.SelectedIndex = 6;
            GraphPane pane = zedGraphControl1.GraphPane;
            pane.Chart.Border.Color = Color.Green;
            pane.XAxis.MajorGrid.IsZeroLine = true;
            pane.YAxis.MajorGrid.IsZeroLine = true;
            pane.XAxis.Color = Color.Gray;
            pane.YAxis.Color = Color.Gray;
            pane.Title.FontSpec.FontColor = Color.Gray;
            pane.XAxis.MajorGrid.IsVisible = true;
            pane.YAxis.MajorGrid.IsVisible = true;
            pane.XAxis.Title.Text = comboBox1.SelectedItem.ToString();
            pane.YAxis.Title.Text = comboBox2.SelectedItem.ToString();
            pane.XAxis.Title.FontSpec.FontColor = Color.Gray;
            pane.YAxis.Title.FontSpec.FontColor = Color.Gray;
            pane.Title.Text = "Зависимость параметра " + comboBox2.SelectedItem.ToString() + " от расстояния";
            pane.CurveList.Clear();
            PointPairList list = new PointPairList();
            StreamReader str = new StreamReader(field);
            string GM = str.ReadToEnd();
            GM = GM.Replace("  ", " ");
            string[] spl = GM.Split('\n');
            for (int i = 1; i < spl.Length - 1; i++)
                spl[i] = spl[i].TrimStart(' ');
            double[] x = new double[spl.Length - 2];
            double[] y = new double[spl.Length - 2];
            for (int i = 0; i < spl.Length - 2; i++)
            {
                string[] sl = spl[i + 1].Split(' ');
                x[i] = Convert.ToDouble(sl[comboBox1.SelectedIndex]);
                y[i] = Convert.ToDouble(sl[comboBox2.SelectedIndex + 3]);
                list.Add(x[i], y[i]);
            }
            LineItem myCurve = pane.AddCurve("", list, Color.Blue, SymbolType.None);
            zedGraphControl1.AxisChange();
            zedGraphControl1.Invalidate();
            str.Close();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Draw();
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            f = false;
            Draw();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GMM_FIELD
{
    class besselyd
    {
        double[] besy;
        int n;
        double x;
        public besselyd(double[] besy, int n, double x)
        {
            this.besy = besy;
            this.n = n;
            this.x = x;
        }
        public void f_besselyd(/*out bool fl*/)
        {
            //fl = true;
            double besyn, x2;
            if (x == 0)
            {
                MessageBox.Show("bad argument in sub. besselyd");
                //fl = false; 
                //return;
            }
            if (Math.Abs(x) - 0.1 < 0)
            {
                x2 = x * x;
                besyn = 1 - x2 / 72;
                besyn = 1 - x2 / 42 * besyn;
                besyn = 1 - x2 / 20 * besyn;
                besyn = 1 - x2 / 6 * besyn;
                besy[0] = 1 - x2 / 56;
                besy[0] = 1 - x2 / 30 * besy[0];
                besy[0] = 1 - x2 / 12 * besy[0];
                besy[0] = 1 - 0.5 * x2 * besy[0];
                besy[0] = -besy[0] / x;
            }
            else
            {
                besyn = Math.Sin(x) / x;
                besy[0] = -Math.Cos(x) / x;
            }
            besy[1] = besy[0] / x - besyn;
            Parallel.For(2, n + 1, i =>
            {
                besy[i] = (double)(2 * i - 1) / x * besy[i - 1] - besy[i - 2];
            });
            //for (int i = 2; i <= n; i++)
            //    besy[i] = (double)(2 * i - 1) / x * besy[i - 1] - besy[i - 2];
        }
    }
}

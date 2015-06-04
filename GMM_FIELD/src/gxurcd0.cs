using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GMM_FIELD
{
    class gxurcd0
    {
        public gxurcd0(double[,] cnv, double[] ga0, int m, int n, int v, out int qmax, int na)
        {
            func_lnfacd l = new func_lnfacd();
            if (Math.Abs(m) > n || Math.Abs(m) > v)
            {
                MessageBox.Show("warning: |m|>n or v in gxurcd0");
                qmax = -1;
                return;
            }
            double c1 = 0, c2 = 0, c3 = 0;
            int nq;
            qmax = Math.Min(n, v);
            nq = qmax + 1;
            if (n <= v)
                c1 = cnv[n - 1, v - 1];
            else
                c1 = cnv[v - 1, n - 1];
            c1 = c1 - l.lnfacd((double)(n - m)) - l.lnfacd((double)(v + m));
            ga0[na] = Math.Exp(c1);
            if (qmax < 1)
                return;
            int p = n + v;
            for (int i = 2; i <= nq; i++)
            {
                p = p - 2;
                if (m == 0)
                {
                    c1 = fb(n, v, p + 1);
                    c2 = fb(n, v, p + 2);
                    ga0[na + i - 1] = c2 * ga0[na + i - 2] / c1;
                }
                else
                {
                    c1 = fb(n, v, p + 1);
                    c2 = (double)(4 * m * m) + fb(n, v, p + 2) + fb(n, v, p + 3);
                    if (i == 2)
                        ga0[na + i - 1] = c2 * ga0[na + i - 2] / c1;
                    else
                    {
                        c3 = -fb(n, v, p + 4);
                        ga0[na + i - 1] = (c2 * ga0[na + i - 2] + c3 * ga0[na + i - 3]) / c1;
                    }
                }
            }
        }
        public double fb(int n, int v, int p)
        {
            return (double)(p - (n + v + 1)) * (double)(p + (n + v + 1)) * (double)(p - (n - v)) * (double)(p + (n - v)) / ((double)(2 * p + 1) * (double)(2 * p - 1));
        }
    }
}

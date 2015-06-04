using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace GMM_FIELD
{
    class gau0
    {
        double[,] cnv;
        double[] ga0;
        int[] iga0;
        int nmax, ij, nj, qj;
        public gau0(double[,] cnv, double[] ga0, int[] iga0, int nmax)
        {
            this.cnv = cnv;
            this.ga0 = ga0;
            this.iga0 = iga0;
            this.nmax = nmax;
            for (int m = -nmax; m <= 0; m++)
            {
                int ns = Math.Max(1, Math.Abs(m));
                for (int n = ns; n <= nmax; n++)
                    for (int v = ns; v <= nmax; v++)
                    {
                        ij++;
                        if (Math.Abs(m) > n || Math.Abs(m) > v)
                        {
                            qj = -1;
                        }
                        else
                            qj = Math.Min(n, v);
                        nj = nj + qj + 1;
                    }

            }
        }
        public void f_gau0()
        {
            Thread t1 = new Thread(P1);
            t1.Start(0);
            Thread t2 = new Thread(P2);
            t2.Start(ij);
        }
        public void P1(object ii)
        {
            int na = 0;
            int qmax = 0;
            int i = Convert.ToInt32(ii);
            for (int m = -nmax; m <= 0; m++)
            {
                int ns = Math.Max(1, Math.Abs(m));
                for (int n = ns; n <= nmax; n++)
                {
                    for (int v = ns; v <= nmax; v++)
                    {
                        new gxurcd0(cnv, ga0, -m, n, v, out qmax, na);
                        i = i + 1;
                        iga0[i - 1] = na;
                        na = na + qmax + 1;
                    }
                }
            }
        }
        public void P2(object ii)
        {
            int na = nj;
            int qmax = 0;
            int i = Convert.ToInt32(ii);
            for (int m = 1; m <= nmax; m++)
            {
                int ns = Math.Max(1, Math.Abs(m));
                for (int n = ns; n <= nmax; n++)
                {
                    for (int v = ns; v <= nmax; v++)
                    {
                        new gxurcd0(cnv, ga0, -m, n, v, out qmax, na);
                        i = i + 1;
                        iga0[i - 1] = na;
                        na = na + qmax + 1;
                    }
                }
            }
        }
    }
}

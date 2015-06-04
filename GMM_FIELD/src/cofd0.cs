using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace GMM_FIELD
{
    class cofd0
    {
        double[] fnr, cof0, cofsr;
        int nmax, ij;
        public cofd0(double[] fnr, double[] cof0, double[] cofsr, int nmax)
        {
            this.fnr = fnr;
            this.cof0 = cof0;
            this.cofsr = cofsr;
            this.nmax = nmax;
            for (int m = -nmax; m <= 0; m++)
            {
                int ns = Math.Max(1, Math.Abs(m));
                for (int n = ns; n <= nmax; n++)
                    for (int v = ns; v <= nmax; v++)
                        ij++;

            }
        }
        public void f_cofd0()
        {
            Thread t1 = new Thread(P1);
            t1.Start(0);
            Thread t2 = new Thread(P2);
            t2.Start(ij);
        }
        public void P1(object ii)
        {
            int i = Convert.ToInt32(ii);
            int inm;
            double c = 0, c0 = 0, c1 = 0;
            double sm = -0.5 * Math.Pow((-1), nmax);
            for (int m = -nmax; m <= 0; m++)
            {
                int ns = Math.Max(1, Math.Abs(m));
                sm = -sm;
                for (int n = ns; n <= nmax; n++)
                {
                    inm = n * (n + 1) - m;
                    for (int v = ns; v <= nmax; v++)
                    {
                        i = i + 1;
                        int ivm = v * (v + 1) + m;
                        c = cofsr[inm - 1] + cofsr[ivm - 1];
                        c = sm * Math.Exp(c);
                        c0 = fnr[2 * n + 1] * fnr[2 * v + 1];
                        c1 = fnr[n] * fnr[v] * fnr[n + 1] * fnr[v + 1];
                        c0 = c0 / c1;
                        cof0[i - 1] = c * c0;
                    }
                }
            }
        }
        public void P2(object ii)
        {
            int i = Convert.ToInt32(ii);
            int inm;
            double c = 0, c0 = 0, c1 = 0;
            double sm = -0.5 * Math.Pow((-1), nmax);
            for (int m = 1; m <= nmax; m++)
            {
                int ns = Math.Max(1, Math.Abs(m));
                sm = -sm;
                for (int n = ns; n <= nmax; n++)
                {
                    inm = n * (n + 1) - m;
                    for (int v = ns; v <= nmax; v++)
                    {
                        i = i + 1;
                        int ivm = v * (v + 1) + m;
                        c = cofsr[inm - 1] + cofsr[ivm - 1];
                        c = sm * Math.Exp(c);
                        c0 = fnr[2 * n + 1] * fnr[2 * v + 1];
                        c1 = fnr[n] * fnr[v] * fnr[n + 1] * fnr[v + 1];
                        c0 = c0 / c1;
                        cof0[i - 1] = c * c0;
                    }
                }
            }
        }
    }
}

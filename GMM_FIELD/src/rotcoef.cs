using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace GMM_FIELD
{
    class rotcoef
    {
        int np, nmax, n1;
        double[,] dc;
        double[] bcof, fnr;
        double cbe, ss = 1, sbe;
        int nnn = 1;
        double[] dk0, dk01;
        public rotcoef(int np, double[,] dc, double[] bcof, double[] fnr, double cbe, int nmax)
        {
            this.np = np;
            this.nmax = nmax;
            this.dc = dc;
            this.bcof = bcof;
            this.fnr = fnr;
            this.cbe = cbe;
            sbe = Math.Sqrt((1 + cbe) * (1 - cbe));
            dk0 = new double[4 * np + 1];
            dk01 = new double[4 * np + 1];
            if (nmax % 2 == 0)
                n1 = nmax / 2;
            else
                n1 = (nmax + 1) / 2;
            for (int n = 1; n <= n1; n++)
            {
                nnn = -nnn;
                ss = ss * sbe / 2;
            }
        }
        public void f_rotcoef()
        {
            Thread t1 = new Thread(P1);
            t1.Start();
            Thread t2 = new Thread(P2);
            t2.Start();
        }
        public void P1()
        {
            double cbe2 = 0.5 * (1 + cbe);
            double sbe2 = 0.5 * (1 - cbe);
            int inn = 1;
            dk0[0 + 2 * np] = 1;
            double sben = 1;
            dc[0 + np, 0] = 1;
            dk01[0 + 2 * np] = 0;
            for (int n = 1; n <= n1; n++)
            {
                int nn1 = n * (n + 1);
                inn = -inn;
                sben = sben * sbe / 2;

                dk0[n + 2 * np] = (double)(inn) * sben * bcof[n];
                dk0[-n + 2 * np] = (double)(inn) * dk0[n + 2 * np];
                dk01[n + 2 * np] = 0;
                dk01[-n + 2 * np] = 0;
                dc[0 + np, nn1 + n] = dk0[n + 2 * np];
                dc[0 + np, nn1 - n] = dk0[-n + 2 * np];
                for (int k = -n + 1; k <= n - 1; k++)
                {
                    int kn = nn1 + k;
                    double dkt = dk01[k + 2 * np];
                    dk01[k + 2 * np] = dk0[k + 2 * np];
                    dk0[k + 2 * np] = (cbe * (double)(n + n - 1) * dk01[k + 2 * np] - fnr[n - k - 1] * fnr[n + k - 1] * dkt) / (fnr[n + k] * fnr[n - k]);
                    dc[0 + np, kn] = dk0[k + 2 * np];
                }
                int im = 1;
                for (int m = 1; m <= n; m++)
                {
                    im = -im;
                    double fmn = 1 / fnr[n - m + 1] / fnr[n + m];
                    int m1 = m - 1;
                    double dkm0 = 0;
                    for (int k = -n; k <= n; k++)
                    {
                        int kn = nn1 + k;
                        double dkm1 = dkm0;
                        double dkn1;
                        dkm0 = dc[m1 + np, kn];
                        if (k == n)
                            dkn1 = 0;
                        else
                            dkn1 = dc[m1 + np, kn + 1];
                        dc[m + np, kn] = (fnr[n + k] * fnr[n - k + 1] * cbe2 * dkm1 - fnr[n - k] * fnr[n + k + 1] * sbe2 * dkn1 - (double)(k) * sbe * dc[m1 + np, kn]) * fmn;
                        dc[-m + np, nn1 - k] = (double)(Math.Pow((-1), k) * im) * dc[m + np, kn];
                    }
                }
            }
        }
        public void P2()
        {
            double cbe2 = 0.5 * (1 + cbe);
            double sbe2 = 0.5 * (1 - cbe);
            int inn = nnn;
            dk0[0 + 2 * np] = 1;
            double sben = ss;
            dc[0 + np, 0] = 1;
            dk01[0 + 2 * np] = 0;
            for (int n = n1 + 1; n <= nmax; n++)
            {
                int nn1 = n * (n + 1);
                inn = -inn;
                sben = sben * sbe / 2;
                dk0[n + 2 * np] = (double)(inn) * sben * bcof[n];
                dk0[-n + 2 * np] = (double)(inn) * dk0[n + 2 * np];
                dk01[n + 2 * np] = 0;
                dk01[-n + 2 * np] = 0;
                dc[0 + np, nn1 + n] = dk0[n + 2 * np];
                dc[0 + np, nn1 - n] = dk0[-n + 2 * np];
                for (int k = -n + 1; k <= n - 1; k++)
                {
                    int kn = nn1 + k;
                    double dkt = dk01[k + 2 * np];
                    dk01[k + 2 * np] = dk0[k + 2 * np];
                    dk0[k + 2 * np] = (cbe * (double)(n + n - 1) * dk01[k + 2 * np] - fnr[n - k - 1] * fnr[n + k - 1] * dkt) / (fnr[n + k] * fnr[n - k]);
                    dc[0 + np, kn] = dk0[k + 2 * np];
                }
                int im = 1;
                for (int m = 1; m <= n; m++)
                {
                    im = -im;
                    double fmn = 1 / fnr[n - m + 1] / fnr[n + m];
                    int m1 = m - 1;
                    double dkm0 = 0;
                    for (int k = -n; k <= n; k++)
                    {
                        int kn = nn1 + k;
                        double dkm1 = dkm0;
                        double dkn1;
                        dkm0 = dc[m1 + np, kn];
                        if (k == n)
                            dkn1 = 0;
                        else
                            dkn1 = dc[m1 + np, kn + 1];
                        dc[m + np, kn] = (fnr[n + k] * fnr[n - k + 1] * cbe2 * dkm1 - fnr[n - k] * fnr[n + k + 1] * sbe2 * dkn1 - (double)(k) * sbe * dc[m1 + np, kn]) * fmn;
                        dc[-m + np, nn1 - k] = (double)(Math.Pow((-1), k) * im) * dc[m + np, kn];
                    }
                }
            }
        }
    }
}

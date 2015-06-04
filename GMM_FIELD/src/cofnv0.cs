using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace GMM_FIELD
{
    class cofnv0
    {
        double[,] cnv;
        int nmax, n1;
        public cofnv0(double[,] cnv, int nmax)
        {
            this.cnv = cnv;
            this.nmax = nmax;
            if (nmax % 2 == 0)
            {
                n1 = nmax / 2;
            }
            else
                n1 = (nmax + 1) / 2;
        }
        public void f_cofnv0()
        {
            Thread t1 = new Thread(P1);
            t1.Start();
            Thread t2 = new Thread(P2);
            t2.Start();
        }
        public void P1()
        {
            func_lnfacd l = new func_lnfacd();
            double c1 = 0;
            for (int n = 1; n <= n1; n++)
            {
                for (int v = n; v <= nmax; v++)
                {
                    c1 = l.lnfacd(2 * n) + l.lnfacd(2 * v);
                    c1 = c1 - l.lnfacd(2 * n + 2 * v);
                    c1 = c1 + 2 * l.lnfacd(n + v);
                    c1 = c1 - l.lnfacd(n) - l.lnfacd(v);
                    cnv[n - 1, v - 1] = c1;
                }
            }
        }
        public void P2()
        {
            func_lnfacd l = new func_lnfacd();
            double c1 = 0;
            for (int n = n1 + 1; n <= nmax; n++)
            {
                for (int v = n; v <= nmax; v++)
                {
                    c1 = l.lnfacd(2 * n) + l.lnfacd(2 * v);
                    c1 = c1 - l.lnfacd(2 * n + 2 * v);
                    c1 = c1 + 2 * l.lnfacd(n + v);
                    c1 = c1 - l.lnfacd(n) - l.lnfacd(v);
                    cnv[n - 1, v - 1] = c1;
                }
            }
        }
    }
}

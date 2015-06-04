using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace GMM_FIELD
{
    class cofsrd
    {
        double[] cofsr;
        int nmax;
        int n1, ij = 0;
        public cofsrd(double[] cofsr, int nmax)
        {
            this.cofsr = cofsr;
            this.nmax = nmax;
            if (nmax % 2 == 0)
            {
                n1 = nmax / 2;
            }
            else
                n1 = (nmax + 1) / 2;
            for (int i = 1; i <= n1; i++)
                ij = ij + i * 2 + 1;
        }
        public void f_cofsrd()
        {
            Thread t1 = new Thread(P1);
            t1.Start(0);
            Thread t2 = new Thread(P2);
            t2.Start(ij);
        }
        public void P1(object ii)
        {
            int i = Convert.ToInt32(ii);
            func_lnfacd l = new func_lnfacd();
            double c;
            for (int n = 1; n <= n1; n++)
            {
                for (int m = -n; m <= n; m++)
                {
                    i = i + 1;
                    c = l.lnfacd(n - m) - l.lnfacd(n + m);
                    cofsr[i - 1] = 0.5 * c;
                }
            }
        }
        public void P2(object ii)
        {
            int i = Convert.ToInt32(ii);
            func_lnfacd l = new func_lnfacd();
            double c;
            for (int n = n1 + 1; n <= nmax; n++)
            {
                for (int m = -n; m <= n; m++)
                {
                    i = i + 1;
                    c = l.lnfacd(n - m) - l.lnfacd(n + m);
                    cofsr[i - 1] = 0.5 * c;
                }
            }
        }
    }
}

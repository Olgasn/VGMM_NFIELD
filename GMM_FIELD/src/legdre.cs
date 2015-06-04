using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMM_FIELD
{
    class legdre
    {
        double[] p;
        int nmax;
        double xt;
        public legdre(double[] p, int nmax, double xt)
        {
            this.p = p;
            this.nmax = nmax;
            this.xt = xt;
        }
        public void f_legdre()
        {
            int npp, nm, n2p;
            double en;
            p[0] = 1;
            p[1] = xt;
            npp = 2;
            nm = 0;
            n2p = 3;
            for (int n = 1; n <= nmax - 1; n++)
            {
                p[npp] = (double)(n2p * xt * p[n] - n * p[nm]) / (double)(npp);
                npp = npp + 1;
                nm = nm + 1;
                n2p = n2p + 2;
            }
            Parallel.For(1, nmax, n =>
            {
                en = Math.Sqrt((double)(2 * n + 1) / (double)(n * (n + 1)));
                p[n] = p[n] * en;
            });
        }
    }
}

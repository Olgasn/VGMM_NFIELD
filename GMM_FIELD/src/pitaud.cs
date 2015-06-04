using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GMM_FIELD
{
    class pitaud
    {
        double[] pi, tau, fnr;
        double x;
        int nmp0, nmax;
        public pitaud(double[] pi, double[] tau, double[] fnr, int nmax, double x, int nmp0)
        {
            this.pi = pi;
            this.tau = tau;
            this.fnr = fnr;
            this.x = x;
            this.nmax = nmax;
            this.nmp0 = nmp0;
        }
        public void f_pitaud()
        {

            int n, i1, m, i2;
            double t, tx;
            int nt = (nmax + 1) * (nmax + 4) / 2;
            if (nt > nmp0 || Math.Abs(x) > 1)
            {
                MessageBox.Show("dimension or argument wrong in sub. tipitaud! argument: " + x.ToString());
                System.Environment.Exit(0); 
            }
            double sx = Math.Sqrt(1 - x * x);
            pi[0] = 0;
            pi[1] = Math.Sqrt(0.75);
            pi[2] = 0;
            double t125 = Math.Sqrt(1.25);
            pi[3] = t125 * x;
            pi[4] = t125 * sx;
            int imn = 5;
            for (int i = 3; i <= nmax + 1; i++)
            {
                n = i;
                imn = imn + 1;
                pi[imn - 1] = 0;
                for (int j = 2; j <= n; j++)
                {
                    m = j - 1;
                    imn = imn + 1;
                    i1 = (n - 2) * (n + 1) / 2 + m + 1;
                    if (m == (n - 1))
                        pi[imn - 1] = fnr[n - 1] * fnr[2 * n + 1] / fnr[n + 1] * x * pi[i1 - 1];
                    else
                    {
                        i2 = (n - 3) * n / 2 + m + 1;
                        t = fnr[n] * fnr[2 * n - 3];
                        t = fnr[n - 2] * fnr[n - m - 1] * fnr[n + m - 1] / t;
                        pi[imn - 1] = fnr[2 * n - 1] * x * pi[i1 - 1] - t * pi[i2 - 1];
                        t = fnr[n + 1] * fnr[n - m] * fnr[n + m];
                        t = fnr[n - 1] * fnr[2 * n + 1] / t;
                        pi[imn - 1] = t * pi[imn - 1];
                    }
                }
                imn = imn + 1;
                i1 = (n - 2) * (n + 1) / 2 + n;
                t = fnr[n - 1] * fnr[n + 1];
                t = Math.Sqrt(0.5) * fnr[n] * fnr[2 * n + 1] / t;
                pi[imn - 1] = t * sx * pi[i1 - 1];
            }
            tx = x * sx;
            tau[0] = -Math.Sqrt(1.5) * sx;
            tau[1] = pi[1] * x;
            tau[2] = -Math.Sqrt(7.5) * tx;
            tau[3] = t125 * (2 * x * x - 1);
            tau[4] = t125 * tx;
            imn = 5;
            for (int i = 3; i <= nmax; i++)
            {
                n = i;
                for (int j = 1; j <= n + 1; j++)
                {
                    m = j - 1;
                    imn = imn + 1;
                    if (m == 0)
                    {
                        i1 = (n - 2) * (n + 1) / 2 + 1;
                        i2 = (n - 3) * n / 2 + 1;
                        t = fnr[2 * n - 3];
                        t = fnr[n - 2] * fnr[n] / t;
                        tau[imn - 1] = fnr[2 * n - 1] * x * tau[i1 - 1] - t * tau[i2 - 1];
                        t = fnr[n - 1] * fnr[n + 1];
                        t = fnr[2 * n + 1] / t;
                        tau[imn - 1] = t * tau[imn - 1];
                    }
                    else
                    {
                        i1 = n * (n + 3) / 2 + m + 1;
                        t = fnr[n] * fnr[2 * n + 3];
                        t = fnr[n + 2] * fnr[2 * n + 1] * fnr[n - m + 1] * fnr[n + m + 1] / t;
                        tau[imn - 1] = t * pi[i1 - 1] - (double)(n + 1) * x * pi[imn - 1];
                        tau[imn - 1] = tau[imn - 1] / (double)(m);
                    }
                }
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMM_FIELD
{
    class besseljd
    {
        double[] besj;
        int NC, n1;
        double X;
        public besseljd(double[] besj, int NC, double X)
        {
            this.besj = besj;
            this.NC = NC;
            this.X = X;
            if (NC % 2 == 0)
            {
                n1 = NC / 2;
            }
            else
                n1 = (NC + 1) / 2;
        }
        public void f_besseljd()
        {

            int NX, N = 0;
            double PN, CN, X2;
            for (int K = 1; K <= NC; K++)
                besj[K] = 0;
            if (Math.Abs(X) < 1 / (double)Math.Pow(10, 12))
            {
                besj[0] = 1;
                besj[1] = 1 / (double)3 * X;
                return;
            }
            NX = (int)(1.1 * X) + 10;
            NX = NC + NX;
            PN = X / (double)(2 * NX + 3);
            for (int K = 1; K <= NX - 1; K++)
            {
                N = NX - K + 1;
                CN = (double)(N);
                PN = X / ((double)(2 * N + 1) - PN * X);
                if (N <= NC)
                    besj[N] = PN;
            }
            if (Math.Abs(X) - 0.1 < 0)
            {
                X2 = X * X;
                besj[0] = 1 - X2 / (double)72;
                besj[0] = 1 - X2 / (double)42 * besj[0];
                besj[0] = 1 - X2 / (double)20 * besj[0];
                besj[0] = 1 - X2 / (double)6 * besj[0];
                besj[1] = 1 / (double)45360 - X2 / (double)3991680;
                besj[1] = 1 / (double)840 - X2 * besj[1];
                besj[1] = 1 / (double)30 - X2 * besj[1];
                besj[1] = X * (1 / (double)3 - X2 * besj[1]);
            }
            else
            {
                besj[0] = Math.Sin(X) / (double)X;
                besj[1] = (Math.Sin(X) / (double)X - Math.Cos(X)) / (double)X;
            }
            Parallel.For(2, NC + 1, NN =>
            {
                besj[NN] = besj[NN] * besj[NN - 1];
            });
            //for (int NN = 2; NN <= NC; NN++)
            //    besj[NN] = besj[NN] * besj[NN - 1];
        }
    }
}

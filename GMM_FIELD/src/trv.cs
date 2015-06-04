using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
using System.Threading;

namespace GMM_FIELD
{
    class trv
    {
        public trv(int np, Complex[, ,] atr, Complex[,] Anpt, int nodrj, int nodri)
        {
            Complex[,] ant = new Complex[2, 2 * np];
            int mmax = Math.Min(nodrj, nodri);
            Complex a, b;
            int imn, n1, ml;
            for (int m = -mmax; m <= mmax; m++)
            {
                n1 = Math.Max(1, Math.Abs(m));
                for (int n = n1; n <= nodrj; n++)
                {
                    imn = n * (n + 1) + m;
                    for (int ip = 1; ip <= 2; ip++)
                        ant[ip - 1, n - 1] = Anpt[ip - 1, imn - 1];
                }
                for (int n = n1; n <= nodri; n++)
                {
                    imn = n * (n + 1) + m;
                    a = 0;
                    b = 0;
                    for (int l = n1; l <= nodrj; l++)
                    {
                        ml = l * (l + 1) + m;
                        a = a + atr[0, n - 1, ml - 1] * ant[0, l - 1] + atr[1, n - 1, ml - 1] * ant[1, l - 1];
                        b = b + atr[0, n - 1, ml - 1] * ant[1, l - 1] + atr[1, n - 1, ml - 1] * ant[0, l - 1];
                    }
                    Anpt[0, imn - 1] = a;
                    Anpt[1, imn - 1] = b;
                }
            }
        }
    }
}

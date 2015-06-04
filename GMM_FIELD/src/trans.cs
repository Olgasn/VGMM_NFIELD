using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
using System.Windows.Forms;

namespace GMM_FIELD
{
    class trans
    {
        public trans(int np, Complex[, ,] atr, int nmp, int nL, double[,] r0, int[] nmax, int[] uvmax, double fint, Complex[,] atr0, Complex[,] btr0, Complex[,] ek, double[,] drot, Complex[,] ass, Complex[,] bs, Complex[,] as1, Complex[,] bs1, int[] ind)
        {
            double x0, y0, z0;
            double d, temp;
            int nlarge, itrc, nsmall;
            int ij = 0;
            Complex[,] at1 = new Complex[2, nmp];
            for (int i = 1; i <= nL; i++)
            {
                if (ind[i - 1] <= 0)
                {
                    for (int imn = 1; imn <= uvmax[i - 1]; imn++)
                    {
                        as1[i - 1, imn - 1] = 0;
                        bs1[i - 1, imn - 1] = 0;
                    }
                    for (int j = 1; j <= nL; j++)
                    {
                        if (j != i)
                        {
                            x0 = r0[0, i - 1] - r0[0, j - 1];
                            y0 = r0[1, i - 1] - r0[1, j - 1];
                            z0 = r0[2, i - 1] - r0[2, j - 1];
                            d = Math.Sqrt(x0 * x0 + y0 * y0 + z0 * z0);
                            temp = (r0[3, i - 1] + r0[3, j - 1]) / d;
                            if (temp > fint)
                            {
                                if (i < j)
                                    ij = (j - 1) * (j - 2) / 2 + j - i;
                                else
                                    ij = (i - 1) * (i - 2) / 2 + i - j;
                                nlarge = Math.Max(nmax[i - 1], nmax[j - 1]);
                                itrc = 0;
                                nsmall = Math.Min(nmax[i - 1], nmax[j - 1]);
                                for (int m = -nsmall; m <= nsmall; m++)
                                {
                                    int n1 = Math.Max(1, Math.Abs(m));
                                    for (int n = n1; n <= nlarge; n++)
                                        for (int v = n1; v <= nlarge; v++)
                                        {
                                            itrc = itrc + 1;
                                            int iuv = v * (v + 1) + m;
                                            atr[0, n - 1, iuv - 1] = atr0[itrc - 1, ij - 1];
                                            atr[1, n - 1, iuv - 1] = btr0[itrc - 1, ij - 1];
                                            if (x0 == 0 && y0 == 0)
                                                if (z0 < 0)
                                                {
                                                    double sic = (double)(Math.Pow((-1), (n + v)));
                                                    atr[0, n - 1, iuv - 1] = sic * atr[0, n - 1, iuv - 1];
                                                    atr[1, n - 1, iuv - 1] = -sic * atr[1, n - 1, iuv - 1];
                                                }
                                                else
                                                    if (j < i)
                                                    {
                                                        double sic = (double)(Math.Pow((-1), (n + v)));
                                                        atr[0, n - 1, iuv - 1] = sic * atr[0, n - 1, iuv - 1];
                                                        atr[1, n - 1, iuv - 1] = -sic * atr[1, n - 1, iuv - 1];
                                                    }
                                        }
                                }
                                for (int iuv = 1; iuv <= uvmax[i - 1]; iuv++)
                                {
                                    at1[0, iuv - 1] = ass[j - 1, iuv - 1];
                                    at1[1, iuv - 1] = bs[j - 1, iuv - 1];
                                }
                                if (x0 == 0 && y0 == 0)
                                    new trv(np, atr, at1, nmax[j - 1], nmax[i - 1]);
                                else
                                    new rtr(np, atr, at1, nmax[j - 1], nmax[i - 1], ek, drot);
                                for (int imn = 1; imn <= uvmax[i - 1]; imn++)
                                {
                                    as1[i - 1, imn - 1] = as1[i - 1, imn - 1] + at1[0, imn - 1];
                                    bs1[i - 1, imn - 1] = bs1[i - 1, imn - 1] + at1[1, imn - 1];
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}

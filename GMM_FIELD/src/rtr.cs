using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

namespace GMM_FIELD
{
    class rtr
    {
        public rtr(int np, Complex[, ,] atr, Complex[,] Anpt, int nodrj, int nodri, Complex[,] ekt, double[,] drot)
        {
            Complex[] ek = new Complex[2 * np + 1];
            Complex[,] amt = new Complex[2, 2 * np + 1];
            Complex[,] ant = new Complex[2, 2 * np];
            Complex a, b;
            int kn, n1, imn;
            ek[0 + np] = 1;
            int mmax = Math.Max(nodrj, nodri);
            for (int m = 1; m <= mmax; m++)
            {
                ek[m + np] = ekt[m - 1, 0];
                ek[-m + np] = Complex.Conjugate(ek[m + np]);
            }
            int irc = 0;
            for (int n = 1; n <= nodrj; n++)
            {
                n1 = n * (n + 1);
                for (int m = -n; m <= n; m++)
                {
                    amt[0, m + np] = 0;
                    amt[1, m + np] = 0;
                }
                for (int k = -n; k <= n; k++)
                {
                    kn = n1 + k;
                    a = ek[k + np] * Anpt[0, kn - 1];
                    b = ek[k + np] * Anpt[1, kn - 1];
                    for (int m = -n; m <= n; m++)
                    {
                        irc = irc + 1;
                        amt[0, m + np] = amt[0, m + np] + a * drot[irc - 1, 0];
                        amt[1, m + np] = amt[1, m + np] + b * drot[irc - 1, 0];
                    }
                }
                for (int m = -n; m <= n; m++)
                {
                    imn = n1 + m;
                    Anpt[0, imn - 1] = amt[0, m + np];
                    Anpt[1, imn - 1] = amt[1, m + np];
                }
            }
            mmax = Math.Min(nodrj, nodri);
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
                        int ml = l * (l + 1) + m;
                        a = a + atr[0, n - 1, ml - 1] * ant[0, l - 1] + atr[1, n - 1, ml - 1] * ant[1, l - 1];
                        b = b + atr[0, n - 1, ml - 1] * ant[1, l - 1] + atr[1, n - 1, ml - 1] * ant[0, l - 1];
                    }
                    Anpt[0, imn - 1] = a;
                    Anpt[1, imn - 1] = b;
                }
            }
            int inn = 1;
            irc = 0;
            for (int n = 1; n <= nodri; n++)
            {
                inn = -inn;
                n1 = n * (n + 1);
                for (int m = -n; m <= n; m++)
                {
                    amt[0, m + np] = 0;
                    amt[1, m + np] = 0;
                }
                int sik = -inn;
                for (int k = -n; k <= n; k++)
                {
                    sik = -sik;
                    kn = n1 + k;
                    a = sik * Anpt[0, kn - 1];
                    b = sik * Anpt[1, kn - 1];
                    for (int m = -n; m <= n; m++)
                    {
                        irc = irc + 1;
                        amt[0, m + np] = amt[0, m + np] + a * drot[irc - 1, 0];
                        amt[1, m + np] = amt[1, m + np] + b * drot[irc - 1, 0];
                    }
                }
                sik = -inn;
                for (int m = -n; m <= n; m++)
                {
                    sik = -sik;
                    imn = n1 + m;
                    Anpt[0, imn - 1] = amt[0, m + np] * ek[-m + np] * sik;
                    Anpt[1, imn - 1] = amt[1, m + np] * ek[-m + np] * sik;
                }
            }
        }
    }
}

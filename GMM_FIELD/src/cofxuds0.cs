using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
using System.Windows.Forms;

namespace GMM_FIELD
{
    class cofxuds0
    {
        public cofxuds0(out bool fl,double[] cof0, int[] iga0, double[] ga0, int nmax, int m, int n, int v, double[] sja, double[] sya, out Complex A, out Complex B, out Complex Aj, out Complex Bj)
        {
            fl = true;
            A = 0;
            B = 0;
            Aj = 0;
            Bj = 0;
            if (Math.Abs(m) > n || Math.Abs(m) > v)
            {
                MessageBox.Show("|m|>n or v in subroutine cofxuds0.f");
                fl = false;
                return;
            }
            int qmax = 0;
            double cp = 0;
            double sj, sy, c4;
            int id = gid0(nmax, m, n, v);
            double c = cof0[id - 1];
            int ig = iga0[id - 1];
            int nv2 = v * (v + 1) + n * (n + 1);
            Complex signz = Complex.Pow(new Complex(0, 1), (n + v));
            int pp = n + v + 2;
            qmax = Math.Min(n, v);
            for (int i = 1; i <= qmax + 1; i++)
            {
                pp = pp - 2;
                cp = (double)(nv2 - pp * (pp + 1)) * ga0[ig + i - 1];
                sj = sja[pp];
                sy = sya[pp];
                A = A + (new Complex(sj, sy)) * signz * cp;
                Aj = Aj + sj * signz * cp;
                signz = -signz;
            }
            A = A * c;
            Aj = Aj * c;
            if (m != 0)
            {
                signz = Complex.Pow(new Complex(0, 1), (n + v + 1));
                pp = n + v;
                for (int i = 1; i <= qmax; i++)
                {
                    pp = pp - 2;
                    signz = -signz;
                    if (i == 1)
                    {
                        cp = (double)(2 * pp + 3) * fa(m, pp + 3);
                        cp = cp * ga0[ig] / (double)((pp + 3) * (pp + 2));
                        sj = sja[pp + 1];
                        sy = sya[pp + 1];
                        B = B + (new Complex(sj, sy)) * signz * cp;
                        Bj = Bj + sj * signz * cp;
                    }
                    else
                    {
                        if (i == qmax)
                        {
                            if (pp == 0)
                            {
                                c4 = fa(m, pp + 2);
                                cp = -(double)((pp + 1) * (pp + 2)) * fa(n, v, pp + 2) * ga0[ig + i - 1];
                                cp = cp + (double)((pp + 2) * (pp + 1)) * fa(n, v, pp + 1) * ga0[ig + i];
                                cp = cp * (double)(2 * pp + 3) / c4;
                            }
                            else
                            {
                                nv2 = pp * (pp + 1);
                                cp = (double)(2 * pp + 3) * fa(m, pp + 1);
                                cp = -cp * ga0[ig + i] / (double)(nv2);
                            }
                        }
                        else
                        {
                            c4 = fa(m, pp + 2);
                            cp = -(double)((pp + 1) * (pp + 2)) * fa(n, v, pp + 2) * ga0[ig + i - 1];
                            cp = cp + (double)((pp + 2) * (pp + 1)) * fa(n, v, pp + 1) * ga0[ig + i];
                            cp = cp * (double)(2 * pp + 3) / c4;
                        }
                        sj = sja[pp + 1];
                        sy = sya[pp + 1];
                        B = B + (new Complex(sj, sy)) * signz * cp;
                        Bj = Bj + sj * signz * cp;
                    }
                }
                B = B * c;
                Bj = Bj * c;
            }


        }
        public int gid0(int nmax, int m, int n, int iv)
        {
            int id = 0;
            int nt = nmax * (nmax + 1) * (2 * nmax + 1) / 3 + nmax * nmax;
            int ns = Math.Max(1, Math.Abs(m));
            int nc0 = nmax - Math.Abs(m);
            id = nc0 * (nc0 + 1) * (2 * nc0 + 1) / 6;
            if (m < 0)
                id = id + (n - ns) * (nc0 + 1) + iv - ns + 1;
            if (m == 0)
                id = id + (n - ns) * nmax + iv;
            if (m > 0)
            {
                id = id + (nmax - n) * (nc0 + 1) + nmax - iv;
                id = nt - id;
            }
            return id;
        }
        public double fa(int m, int p)
        {
            return (double)(-2 * m * p * (p - 1));
        }
        public double fa(int n, int v, int p)
        {
            return (double)(p * p - (n + v + 1) * (n + v + 1)) * (double)(p * p - (n - v) * (n - v)) / (double)(4 * p * p - 1);
        } 
    }
}

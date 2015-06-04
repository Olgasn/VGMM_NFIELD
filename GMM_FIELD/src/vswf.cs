using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
using System.IO;

namespace GMM_FIELD
{
    class vswf
    {
        public vswf(int np, double[] fnr, double xt, int nmp0, int nmax, double kr, double sphi, double cphi, Complex[,] Nmn3, Complex[,] Mmn3)
        {
            double[] pi = new double[nmp0];
            double[] tau = new double[nmp0];
            double[] p = new double[np + 1];
            double[] besj = new double[np + 1];
            double[] besy = new double[np + 1];
            pitaud pitaud = new pitaud(pi, tau, fnr, nmax, xt, nmp0);
            pitaud.f_pitaud();
            legdre legdre = new legdre(p, nmax, xt);
            legdre.f_legdre();
            besseljd besseljd = new besseljd(besj, nmax, kr);
            besseljd.f_besseljd();
            //bool fl = true;
            besselyd besselyd = new besselyd(besy, nmax, kr);
            besselyd.f_besselyd(/*out fl*/);
            Complex eiphi = new Complex(cphi, sphi);
            Complex cplxi = new Complex(0, 1);
            Complex hankln, psinpr, eimphi;
            double pimn, taumn, pmn;
            int imn = 0;
            for (int n = 1; n <= nmax; n++)
            {
                hankln = new Complex(besj[n], besy[n]);
                psinpr = new Complex(kr * besj[n - 1] - n * besj[n], kr * besy[n - 1] - n * besy[n]);
                for (int m = 0; m <= n; m++)
                {
                    eimphi = Complex.Pow(eiphi, m);
                    imn = imn + 1;
                    pimn = pi[imn - 1];
                    taumn = tau[imn - 1];
                    if (m == 0)
                        pmn = p[n];
                    else
                        pmn = pimn * Math.Sqrt(1 - xt * xt) / m;
                    Mmn3[0, imn - 1] = -taumn * hankln * eimphi;
                    Mmn3[1, imn - 1] = cplxi * pimn * hankln * eimphi;
                    Nmn3[0, imn - 1] = n * (n + 1) * pmn * hankln * eimphi / kr;
                    Nmn3[1, imn - 1] = cplxi * pimn * psinpr * eimphi / kr;
                    Nmn3[2, imn - 1] = taumn * psinpr * eimphi / kr;
                }
            }
        }
    }
}

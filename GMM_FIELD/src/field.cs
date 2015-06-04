using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
using System.IO;

namespace GMM_FIELD
{
    class field
    {
        public field(int[] ngrd, double[] grdmin, int np, int nmp0, double[] fnr, /*StreamWriter OUT,*/ int nL, double[,] r0, double k, int[] nmax, Complex[,] ass, Complex[,] bs)
        {
            double xt = 0, sphi = 0, cphi = 0;
            double[] grdstp = new double[3];
            double x = 0, y = 0, z = 0;
            Complex cplxi = new Complex(0, 1);
            Complex cplx0 = new Complex(0, 0);
            Complex eiphi;
            Complex[] etot = new Complex[3];
            Complex cimn;
            Complex[] escati = new Complex[3];
            Complex[] escatc = new Complex[3];
            Complex[] Emn = new Complex[nmp0];
            Complex[,] Nmn3 = new Complex[3, nmp0];
            Complex[,] Mmn3 = new Complex[2, nmp0];
            new normlz(Emn, nmax[0]);
            double r = 0;
            int imn = 0;
            int jj = 0;
            for (int i = 1; i <= 3; i++)
                grdstp[i - 1] = -grdmin[i - 1] * 2 / (ngrd[i - 1] - 1);
            StreamWriter grid_w = new StreamWriter("grid.txt");
            grid_w.WriteLine("number of grid points (nx, ny, nz):   " + ngrd[0].ToString() + "     " + ngrd[1].ToString() + "     " + ngrd[2].ToString());
            grid_w.WriteLine("grid corner (x0, y0, z0):   " + grdmin[0].ToString() + "     " + grdmin[1].ToString() + "     " + grdmin[2].ToString());
            grid_w.Write("grid step (dx, dy, dz):   " + grdstp[0].ToString() + "     " + grdstp[1].ToString() + "     " + grdstp[2].ToString());
            grid_w.Close();
            StreamWriter field_w = new StreamWriter("field.txt");
            field_w.WriteLine("    X       Y       Z       Ex.r       Ex.i       Ey.r       Ey.i       Ez.r       Ez.i       |E|^2");
            func_vnorm vn = new func_vnorm();
            z = grdmin[2];
            for (int iz = 1; iz <= ngrd[2]; iz++)
            {
                //OUT.WriteLine(iz + "        /       " + ngrd[2]);
                y = grdmin[1];
                for (int iy = 1; iy <= ngrd[1]; iy++)
                {
                    x = grdmin[0];
                    for (int ix = 1; ix <= ngrd[0]; ix++)
                    {
                        etot[0] = new Complex(Math.Cos(k * z), Math.Sin(k * z));
                        etot[1] = cplx0;
                        etot[2] = cplx0;
                        for (int i = 1; i <= nL; i++)
                        {
                            new carsphd(out xt, x - r0[0, i - 1], y - r0[1, i - 1], z - r0[2, i - 1], out r, out sphi, out cphi);
                            eiphi = new Complex(cphi, sphi);
                            if (r < r0[3, i - 1])
                            {
                                etot[0] = cplx0;
                                etot[1] = cplx0;
                                etot[2] = cplx0;
                            }
                            else
                            {
                                new vswf(np, fnr, xt, nmp0, nmax[i - 1], k * r, sphi, cphi, Nmn3, Mmn3);
                                escati[0] = cplx0;
                                escati[1] = cplx0;
                                escati[2] = cplx0;
                                imn = 1;
                                jj = 0;
                                for (int n = 1; n <= nmax[i - 1]; n++)
                                {
                                    imn = imn + n;
                                    for (int m = -n; m <= -1; m++)
                                    {
                                        jj++;
                                        cimn = cplxi * ass[i - 1, jj - 1] * Emn[imn - 1] * Math.Pow((-1), m) * Complex.Pow(eiphi, (2 * m));
                                        escati[0] = escati[0] + cimn * Nmn3[0, imn - 1];
                                        escati[1] = escati[1] - cimn * Nmn3[1, imn - 1];
                                        escati[2] = escati[2] + cimn * Nmn3[2, imn - 1];
                                        cimn = cplxi * bs[i - 1, jj - 1] * Emn[imn - 1] * Math.Pow((-1), m) * Complex.Pow(eiphi, (2 * m));
                                        escati[1] = escati[1] + cimn * Mmn3[0, imn - 1];
                                        escati[2] = escati[2] - cimn * Mmn3[1, imn - 1];
                                        imn--;
                                    }
                                    for (int m = 0; m <= n; m++)
                                    {
                                        jj++;
                                        cimn = cplxi * Emn[imn - 1] * ass[i - 1, jj - 1];
                                        escati[0] = escati[0] + cimn * Nmn3[0, imn - 1];
                                        escati[1] = escati[1] + cimn * Nmn3[1, imn - 1];
                                        escati[2] = escati[2] + cimn * Nmn3[2, imn - 1];
                                        cimn = cplxi * Emn[imn - 1] * bs[i - 1, jj - 1];
                                        escati[1] = escati[1] + cimn * Mmn3[0, imn - 1];
                                        escati[2] = escati[2] + cimn * Mmn3[1, imn - 1];
                                        imn++;
                                    }
                                }
                                new sphcrtv(xt, sphi, cphi, escati, escatc);
                                etot[0] = etot[0] + escatc[0];
                                etot[1] = etot[1] + escatc[1];
                                etot[2] = etot[2] + escatc[2];
                            }
                        }
                        field_w.Write("{0,13:e5} {1,13:e5} {2,13:e5} {3,13:e5} {4,13:e5} {5,13:e5} ",x,y,z,etot[0].Real,etot[0].Imaginary,etot[1].Real);
                        field_w.WriteLine("{0,13:e5} {1,13:e5} {2,13:e5} {3,13:e5}",etot[1].Imaginary,etot[2].Real,etot[2].Imaginary,vn.vnorm(etot));
                        x = x + grdstp[0];
                    }
                    y = y + grdstp[1];
                }
                z = z + grdstp[2];
            }

            field_w.Close();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
using System.IO;

namespace GMM_FIELD
{
    class solver
    {
        public solver(int nLp, int nL, int[] ind, int[] nmax, Complex[,] p0, Complex[,] q0, double factor1, double factor2, double iram, /*StreamWriter OUT,*/ int[] uvmax, int np, Complex[, ,] atr, int nmp, double[,] r0, double fint, Complex[,] atr0, Complex[,] btr0, Complex[,] ek, double[,] drot, Complex[,] aMie, Complex[,] bMie, Complex[,] ass, Complex[,] bs, Complex[,] as1, Complex[,] bs1, double factor, double small, double MXINT, double nram, Complex A2, Complex B2)
        {
            Complex[,] as0 = new Complex[nLp, nmp], bs0 = new Complex[nLp, nmp];
            Complex[,] asp = new Complex[nLp, nmp], bsp = new Complex[nLp, nmp];
            Complex[,] asc = new Complex[nLp, nmp], bsc = new Complex[nLp, nmp];
            Complex[,] ast = new Complex[nLp, nmp], bst = new Complex[nLp, nmp];
            Complex[,] asv = new Complex[nLp, nmp], bsv = new Complex[nLp, nmp];
            double[] c0i = new double[nLp], c1i = new double[nLp];
            double temp = 0;
            double cext0 = 0;
            double cext1 = 0;
            int imn, n;
            Complex A, B, A0, B0, Aj, Bj, Aj2, Bj2;
            bool flag = true;
            for (int i = 1; i <= nL; i++)
            {
                ind[i - 1] = 0;
                c0i[i - 1] = 0;
                for (n = 1; n <= nmax[i - 1]; n++)
                {
                    imn = n * n + n + 1;
                    c0i[i - 1] = c0i[i - 1] + (p0[i - 1, imn - 1] * Complex.Conjugate(p0[i - 1, imn - 1])).Real;
                    c0i[i - 1] = c0i[i - 1] + (q0[i - 1, imn - 1] * Complex.Conjugate(q0[i - 1, imn - 1])).Real;
                    c0i[i - 1] = c0i[i - 1] + (p0[i - 1, imn - 3] * Complex.Conjugate(p0[i - 1, imn - 3])).Real;
                    c0i[i - 1] = c0i[i - 1] + (q0[i - 1, imn - 3] * Complex.Conjugate(q0[i - 1, imn - 3])).Real;
                }
            }
            double niter = 1;
            bool flag2 = true;
            if (factor1 < 0.001 || factor2 < 0.001)
                flag2 = false;
            if (flag2)
            {
                //if (iram == 1)
                //    OUT.WriteLine("Starting iteration solution process");
                for (int i = 1; i <= nL; i++)
                    for (imn = 1; imn <= uvmax[i - 1]; imn++)
                    {
                        as0[i - 1, imn - 1] = p0[i - 1, imn - 1];
                        bs0[i - 1, imn - 1] = q0[i - 1, imn - 1];
                    }
                while (flag2)
                {
                    new trans(np, atr, nmp, nL, r0, nmax, uvmax, fint, atr0, btr0, ek, drot, as0, bs0, as1, bs1, ind);
                    for (int i = 1; i <= nL; i++)
                        if (ind[i - 1] <= 0)
                        {
                            c1i[i - 1] = 0;
                            for (imn = 1; imn <= uvmax[i - 1]; imn++)
                            {
                                n = (int)Math.Sqrt((double)imn);
                                as0[i - 1, imn - 1] = p0[i - 1, imn - 1] - aMie[i - 1, n - 1] * as1[i - 1, imn - 1];
                                bs0[i - 1, imn - 1] = q0[i - 1, imn - 1] - bMie[i - 1, n - 1] * bs1[i - 1, imn - 1];
                                A = as0[i - 1, imn - 1] - ass[i - 1, imn - 1];
                                B = bs0[i - 1, imn - 1] - bs[i - 1, imn - 1];
                                c1i[i - 1] = c1i[i - 1] + (A * Complex.Conjugate(A)).Real;
                                c1i[i - 1] = c1i[i - 1] + (B * Complex.Conjugate(B)).Real;
                                as0[i - 1, imn - 1] = ass[i - 1, imn - 1] + factor * A;
                                bs0[i - 1, imn - 1] = bs[i - 1, imn - 1] + factor * B;
                                ass[i - 1, imn - 1] = as0[i - 1, imn - 1];
                                bs[i - 1, imn - 1] = bs0[i - 1, imn - 1];
                            }
                        }
                    cext0 = 0;
                    cext1 = 0;
                    for (int i = 1; i <= nL; i++)
                        if (ind[i - 1] <= 0)
                        {
                            cext0 = cext0 + c0i[i - 1];
                            cext1 = cext1 + c1i[i - 1];
                            temp = c1i[i - 1] / c0i[i - 1];
                            if (temp < small)
                                ind[i - 1] = 1;
                        }
                    temp = cext1 / cext0;
                    if (temp < small)
                    {
                        flag = false;
                        flag2 = false;
                    }
                    if (flag)
                    {
                        //if (iram == 1 || iram == nram)
                        //    OUT.WriteLine("iteration #  " + niter + "   " + temp);
                        if (niter > MXINT)
                        {
                            //OUT.WriteLine("*** Maximum iterations exceeded ***");
                            //OUT.WriteLine("*** Switched to Bi-CGSTAB method***");
                            for (int i = 1; i <= nL; i++)
                            {
                                ind[i - 1] = 0;
                                for (imn = 1; imn <= uvmax[i - 1]; imn++)
                                {
                                    ass[i - 1, imn - 1] = p0[i - 1, imn - 1];
                                    bs[i - 1, imn - 1] = q0[i - 1, imn - 1];
                                }
                            }
                            niter = 1;
                            flag2 = false;
                        }
                        niter = niter + 1;
                    }
                }
            }
            if (flag)
            {
                //if (iram == 1)
                //    OUT.WriteLine("Starting Bi-CGSTAB solution process");
                new trans(np, atr, nmp, nL, r0, nmax, uvmax, fint, atr0, btr0, ek, drot, ass, bs, as1, bs1, ind);
                for (int i = 1; i <= nL; i++)
                {
                    c1i[i - 1] = 0;
                    for (imn = 1; imn <= uvmax[i - 1]; imn++)
                    {
                        n = (int)Math.Sqrt((double)imn);
                        as1[i - 1, imn - 1] = -aMie[i - 1, n - 1] * as1[i - 1, imn - 1];
                        bs1[i - 1, imn - 1] = -bMie[i - 1, n - 1] * bs1[i - 1, imn - 1];
                        c1i[i - 1] = c1i[i - 1] + (as1[i - 1, imn - 1] * Complex.Conjugate(as1[i - 1, imn - 1])).Real;
                        c1i[i - 1] = c1i[i - 1] + (bs1[i - 1, imn - 1] * Complex.Conjugate(bs1[i - 1, imn - 1])).Real;
                    }
                }
                temp = 0;
                for (int i = 1; i <= nL; i++)
                {
                    cext0 = c1i[i - 1] / c0i[i - 1];
                    if (cext0 < small)
                        ind[i - 1] = 1;
                    if (cext0 > temp)
                        temp = cext0;
                }
                if (temp < small)
                    flag = false;
                if (flag)
                {
                    A0 = 0;
                    for (int i = 1; i <= nL; i++)
                        if (ind[i - 1] <= 0)
                            for (imn = 1; imn <= uvmax[i - 1]; imn++)
                            {
                                asp[i - 1, imn - 1] = as1[i - 1, imn - 1];
                                bsp[i - 1, imn - 1] = bs1[i - 1, imn - 1];
                                as0[i - 1, imn - 1] = as1[i - 1, imn - 1];
                                bs0[i - 1, imn - 1] = bs1[i - 1, imn - 1];
                                A0 = A0 + as1[i - 1, imn - 1] * as1[i - 1, imn - 1];
                                A0 = A0 + bs1[i - 1, imn - 1] * bs1[i - 1, imn - 1];
                            }
                    bool flag3 = true;
                    while (flag3)
                    {
                        new trans(np, atr, nmp, nL, r0, nmax, uvmax, fint, atr0, btr0, ek, drot, asp, bsp, asv, bsv, ind);
                        for (int i = 1; i <= nL; i++)
                            if (ind[i - 1] <= 0)
                                for (imn = 1; imn <= uvmax[i - 1]; imn++)
                                {
                                    n = (int)Math.Sqrt((double)imn);
                                    asv[i - 1, imn - 1] = aMie[i - 1, n - 1] * asv[i - 1, imn - 1] + asp[i - 1, imn - 1];
                                    bsv[i - 1, imn - 1] = bMie[i - 1, n - 1] * bsv[i - 1, imn - 1] + bsp[i - 1, imn - 1];
                                }
                        A = 0;
                        for (int i = 1; i <= nL; i++)
                            if (ind[i - 1] <= 0)
                                for (imn = 1; imn <= uvmax[i - 1]; imn++)
                                {
                                    A = A + asv[i - 1, imn - 1] * as1[i - 1, imn - 1];
                                    A = A + bsv[i - 1, imn - 1] * bs1[i - 1, imn - 1];
                                }
                        Aj = A0 / A;
                        for (int i = 1; i <= nL; i++)
                            if (ind[i - 1] <= 0)
                                for (imn = 1; imn <= uvmax[i - 1]; imn++)
                                {
                                    asc[i - 1, imn - 1] = as0[i - 1, imn - 1] - Aj * asv[i - 1, imn - 1];
                                    bsc[i - 1, imn - 1] = bs0[i - 1, imn - 1] - Aj * bsv[i - 1, imn - 1];
                                }
                        new trans(np, atr, nmp, nL, r0, nmax, uvmax, fint, atr0, btr0, ek, drot, asc, bsc, ast, bst, ind);
                        for (int i = 1; i <= nL; i++)
                            if (ind[i - 1] <= 0)
                                for (imn = 1; imn <= uvmax[i - 1]; imn++)
                                {
                                    n = (int)Math.Sqrt((double)imn);
                                    ast[i - 1, imn - 1] = aMie[i - 1, n - 1] * ast[i - 1, imn - 1] + asc[i - 1, imn - 1];
                                    bst[i - 1, imn - 1] = bMie[i - 1, n - 1] * bst[i - 1, imn - 1] + bsc[i - 1, imn - 1];

                                }
                        for (int i = 1; i <= nL; i++)
                            if (ind[i - 1] <= 0)
                                for (imn = 1; imn <= uvmax[i - 1]; imn++)
                                {
                                    A2 = A2 + ast[i - 1, imn - 1] * asc[i - 1, imn - 1];
                                    A2 = A2 + bst[i - 1, imn - 1] * bsc[i - 1, imn - 1];
                                    B2 = B2 + ast[i - 1, imn - 1] * ast[i - 1, imn - 1];
                                    B2 = B2 + bst[i - 1, imn - 1] * bst[i - 1, imn - 1];
                                }
                        Bj = A2 / B2;
                        for (int i = 1; i <= nL; i++)
                            if (ind[i - 1] <= 0)
                            {
                                c1i[i - 1] = 0;
                                for (imn = 1; imn <= uvmax[i - 1]; imn++)
                                {
                                    Aj2 = Aj * asp[i - 1, imn - 1] + Bj * asc[i - 1, imn - 1];
                                    Bj2 = Aj * bsp[i - 1, imn - 1] + Bj * bsc[i - 1, imn - 1];
                                    c1i[i - 1] = c1i[i - 1] + (Aj2 * Complex.Conjugate(Aj2)).Real;
                                    c1i[i - 1] = c1i[i - 1] + (Bj2 * Complex.Conjugate(Bj2)).Real;
                                    ass[i - 1, imn - 1] = ass[i - 1, imn - 1] + Aj2;
                                    bs[i - 1, imn - 1] = bs[i - 1, imn - 1] + Bj2;
                                }
                            }
                        cext0 = 0;
                        cext1 = 0;
                        for (int i = 1; i <= nL; i++)
                            if (ind[i - 1] <= 0)
                            {
                                cext0 = cext0 + c0i[i - 1];
                                cext1 = cext1 + c1i[i - 1];
                            }
                        temp = cext1 / cext0;
                        if (temp < small)
                        {
                            flag = false;
                            flag3 = false;
                        }
                        if (flag)
                        {
                            if (niter > MXINT)
                            {
                                //OUT.WriteLine("Caution:");
                                //OUT.WriteLine("*** Maximum iterations exceeded ***");
                                flag = false;
                                flag3 = false;
                            }
                            if (flag)
                            {
                                //if (iram == 1 || iram == nram)
                                //    OUT.WriteLine("iteration #  " + niter + "   " + temp);
                                for (int i = 1; i <= nL; i++)
                                    if (ind[i - 1] <= 0)
                                        for (imn = 1; imn <= uvmax[i - 1]; imn++)
                                        {
                                            as0[i - 1, imn - 1] = asc[i - 1, imn - 1] - Bj * ast[i - 1, imn - 1];
                                            bs0[i - 1, imn - 1] = bsc[i - 1, imn - 1] - Bj * bst[i - 1, imn - 1];
                                        }
                                A2 = 0;
                                for (int i = 1; i <= nL; i++)
                                {
                                    ast[i - 1, 0] = new Complex(0, 0);
                                    if (ind[i - 1] <= 0)
                                    {
                                        for (imn = 1; imn <= uvmax[i - 1]; imn++)
                                        {
                                            ast[i - 1, 0] = ast[i - 1, 0] + as0[i - 1, imn - 1] * as1[i - 1, imn - 1];
                                            ast[i - 1, 0] = ast[i - 1, 0] + bs0[i - 1, imn - 1] * bs1[i - 1, imn - 1];
                                        }
                                        A2 = A2 + ast[i - 1, 0];
                                    }
                                }
                                B0 = A2 / A0;
                                B0 = B0 * Aj / Bj;
                                for (int i = 1; i <= nL; i++)
                                    if (ind[i - 1] <= 0)
                                        for (imn = 1; imn <= uvmax[i - 1]; imn++)
                                        {
                                            asp[i - 1, imn - 1] = as0[i - 1, imn - 1] + B0 * (asp[i - 1, imn - 1] - Bj * asv[i - 1, imn - 1]);
                                            bsp[i - 1, imn - 1] = bs0[i - 1, imn - 1] + B0 * (bsp[i - 1, imn - 1] - Bj * bsv[i - 1, imn - 1]);
                                        }
                                A0 = 0;
                                for (int i = 1; i <= nL; i++)
                                    if (ind[i - 1] <= 0)
                                    {
                                        cext0 = c1i[i - 1] / c0i[i - 1];
                                        if (cext0 < small)
                                            ind[i - 1] = 1;
                                        else
                                            A0 = A0 + ast[i - 1, 0];
                                    }
                                niter = niter + 1;
                            }
                        }
                    }
                }
            }
        }
    }
}

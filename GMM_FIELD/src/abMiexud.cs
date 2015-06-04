using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
using System.IO;
using System.Windows.Forms;

namespace GMM_FIELD
{
    class abMiexud
    {
        public abMiexud(double X, Complex REFREL, int NP, int NMAX, out int NM, Complex[] AN, Complex[] BN, int NADD, double[] RSR, double[] RSI, double[] RSX, double[] px, double EPS)
        {
            double[] AR = new double[NP], AI = new double[NP], BR = new double[NP], BI = new double[NP];
            double XM = 0, YM = 0, SNX = 0, CNM1X = 0, CNX = 0, CNM2X = 0;
            double ALN, BEN, PZD, SNM1X;
            int M = 0;
            int N = int.MaxValue;
            double CN;
            if (EPS > 1 || EPS < 0)
                EPS = Math.Pow(1, -20);
            if (NADD != 0)
                EPS = 0;
            double CTC = EPS;
            XM = REFREL.Real;
            YM = REFREL.Imaginary;
            double XMX = X * XM;
            double YMX = X * YM;
            double RP2 = XMX * XMX + YMX * YMX;
            int NSTOP = (int)(X + 4 * Math.Pow(X, 0.3333));
            NSTOP = NSTOP + 2;
            NM = NSTOP + NADD;
            double XN = Math.Sqrt(XM * XM + YM * YM) * X;
            int NX = (int)(1.1 * XN + 10);
            if ((NX - NM) < 10)
                NX = NM + 10;
            double PNX = X / (double)(2 * NX + 3);
            double PNR = XMX / (double)(2 * NX + 3);
            double PNI = YMX / (double)(2 * NX + 3);
            int k = 1;
            bool flag1 = true;
            while (k <= NX && flag1)
            {
                N = NX - k + 1;
                CN = (double)(N);
                ALN = (2 * CN + 1) * XMX / RP2 - PNR;
                BEN = (2 * CN + 1) * YMX / RP2 + PNI;
                RSR[N - 1] = -CN * XMX / RP2 + ALN;
                RSI[N - 1] = CN * YMX / RP2 - BEN;
                PZD = ALN * ALN + BEN * BEN;
                PNR = ALN / PZD;
                PNI = BEN / PZD;
                RSX[N - 1] = (CN + 1) / X - PNX;
                if (N != 1)
                {
                    PNX = X / (2 * CN + 1 - PNX * X);
                    px[N - 1] = PNX;
                }
                else
                    flag1 = false;
                k++;
            }
            SNM1X = Math.Sin(X);
            CNM1X = Math.Cos(X);
            if ((X - 0.1) < 0)
                SNX = Math.Pow(X, 2) / 3 - Math.Pow(X, 4) / 30 + Math.Pow(X, 6) / 840 - Math.Pow(X, 8) / 45360;
            else
                SNX = SNM1X / X - CNM1X;
            CNX = CNM1X / X + SNM1X;
            N = 1;
            flag1 = true;
            while (N <= NX && flag1)
            {
                px[N - 1] = SNX;
                double C = (double)(N);
                double DCNX = CNM1X - C * CNX / X;
                double DSNX = RSX[N - 1] * SNX;
                double ANNR = RSR[N - 1] * SNX - XM * DSNX;
                double ANNI = RSI[N - 1] * SNX - YM * DSNX;
                double TA1 = RSR[N - 1] * SNX - RSI[N - 1] * CNX;
                double TA2 = RSI[N - 1] * SNX + RSR[N - 1] * CNX;
                double ANDR = TA1 - XM * DSNX + YM * DCNX;
                double ANDI = TA2 - XM * DCNX - YM * DSNX;
                double AND = ANDR * ANDR + ANDI * ANDI;
                double BNNR = (XM * RSR[N - 1] - YM * RSI[N - 1]) * SNX - DSNX;
                double BNNI = (XM * RSI[N - 1] + YM * RSR[N - 1]) * SNX;
                double TB1 = RSR[N - 1] * SNX - RSI[N - 1] * CNX;
                double TB2 = RSR[N - 1] * CNX + RSI[N - 1] * SNX;
                double BNDR = XM * TB1 - YM * TB2 - DSNX;
                double BNDI = XM * TB2 + YM * TB1 - DCNX;
                double BND = BNDR * BNDR + BNDI * BNDI;
                AR[N - 1] = (ANNR * ANDR + ANNI * ANDI) / AND;
                AI[N - 1] = (ANNI * ANDR - ANNR * ANDI) / AND;
                BR[N - 1] = (BNNR * BNDR + BNNI * BNDI) / BND;
                BI[N - 1] = (BNNI * BNDR - BNNR * BNDI) / BND;
                double TI = AR[N - 1] * AR[N - 1] + AI[N - 1] * AI[N - 1] + BR[N - 1] * BR[N - 1] + BI[N - 1] * BI[N - 1];
                TI = TI / (AR[0] * AR[0] + AI[0] * AI[0] + BR[0] * BR[0] + BI[0] * BI[0]);
                if (TI - CTC < 0)
                    flag1 = false;
                else
                {
                    if (NM - N <= 0)
                        flag1 = false;
                    else
                    {
                        if (N - NX >= 0)
                            flag1 = false;
                        else
                        {
                            M = N + 1;
                            SNX = px[M - 1] * SNX;
                            CNM2X = CNM1X;
                            CNM1X = CNX;
                            CNX = (2 * C + 1) * CNM1X / X - CNM2X;
                            N++;
                        }
                    }
                }
            }
            NM = N;
            for (int i = 1; i <= NM; i++)
            {
                AN[i - 1] = new Complex(AR[i - 1], -AI[i - 1]);
                BN[i - 1] = new Complex(BR[i - 1], -BI[i - 1]);
            }
        }
    }
}

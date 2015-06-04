using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMM_FIELD
{
    class ran1
    {
        public double ran1d(int idum)
        {
            int NTAB = 32;
            int IM = 2147483647;
            int IR = 2836;
            int IA = 16807;
            int IQ = 127773;
            double AM = 1 / (double)IM, EPS = Math.Pow(1.2, -7), RNMX = 1 - EPS;
            int NDIV = 1 + (IM - 1) / NTAB;
            int iy = 0;
            int k, j;
            int[] iv = new int[NTAB];
            if (idum <= 0 || iy == 0)
            {
                idum = Math.Max(-idum, 1);
                for (j = NTAB + 8; j >= 1; j--)
                {
                    k = idum / IQ;
                    idum = IA * (idum - k * IQ) - IR * k;
                    if (idum < 0)
                        idum = idum + IM;
                    if (j <= NTAB)
                        iv[j - 1] = idum;
                }
                iy = iv[0];
            }
            k = idum / IQ;
            idum = IA * (idum - k * IQ) - IR * k;
            if (idum < 0)
                idum = idum + IM;
            j = 1 + iy / NDIV;
            iy = iv[j - 1];
            iv[j - 1] = idum;
            return Math.Min(AM * iy, RNMX);
        }
    }
}

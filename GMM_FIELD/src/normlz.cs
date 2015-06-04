using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

namespace GMM_FIELD
{
    class normlz
    {
        public normlz(Complex[] Emn, int nmax)
        {
            Complex cplxi = new Complex(0, 1);
            double en;
            int imn = 0;
            Complex cin = new Complex(1, 0);
            for (int n = 1; n <= nmax; n++)
            {
                cin = cin * cplxi;
                en = Math.Sqrt((2 * n + 1) / (double)(n * (n + 1)));
                for (int m = 0; m <= n; m++)
                {
                    imn = imn + 1;
                    Emn[imn - 1] = new Complex(1, 0) * cin;
                }
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMM_FIELD
{
    class carsphd
    {
        public carsphd(out double xt, double x, double y, double z, out double r, out double sphi, out double cphi)
        {
            r = Math.Sqrt(x * x + y * y + z * z);
            if (r == 0)
            {
                xt = 1;
                sphi = 0;
                cphi = 1;
                return;
            }
            xt = z / r;
            if (x == 0 && y == 0)
            {
                sphi = 0;
                cphi = 1;
                return;
            }
            sphi = Math.Sqrt(x * x + y * y);
            cphi = x / sphi;
            sphi = y / sphi;
        }
    }
}

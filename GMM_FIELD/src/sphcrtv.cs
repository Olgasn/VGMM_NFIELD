using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

namespace GMM_FIELD
{
    class sphcrtv
    {
        public sphcrtv(double xt, double sphi, double cphi, Complex[] vsph, Complex[] vcrt)
        {
            double st = 0;
            st = Math.Sqrt(1 - xt * xt);
            vcrt[0] = vsph[0] * cphi * st - vsph[1] * sphi + vsph[2] * cphi * xt;
            vcrt[1] = vsph[0] * sphi * st + vsph[1] * cphi + vsph[2] * sphi * xt;
            vcrt[2] = vsph[0] * xt - vsph[2] * st;
        }
    }
}

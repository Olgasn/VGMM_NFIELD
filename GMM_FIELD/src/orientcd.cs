using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace GMM_FIELD
{
    class orientcd
    {
        Thread Beta, Theta, Phi;
        int NBETA, NTHETA, NPHI;
        double BETAMI, BETAMX, THETMI, THETMX, PHIMIN, PHIMAX;
        double[] BETA, THETA, PHI;
        double delta;
        public orientcd(double BETAMI, double BETAMX, double THETMI, double THETMX, double PHIMIN, double PHIMAX, int NBETA, int NTHETA, int NPHI, double[] BETA, double[] THETA, double[] PHI)
        {
            this.BETAMI = BETAMI;
            this.BETAMX = BETAMX;
            this.THETMI = THETMI;
            this.THETMX = THETMX;
            this.PHIMIN = PHIMIN;
            this.PHIMAX = PHIMAX;
            this.NBETA = NBETA;
            this.NTHETA = NTHETA;
            this.NPHI = NPHI;
            this.BETA = BETA;
            this.THETA = THETA;
            this.PHI = PHI;

            Beta = new Thread(Thread_Beta);
            Theta = new Thread(Thread_Theta);
            Phi = new Thread(Thread_Phi);
            Beta.Start();
            Theta.Start();
            Phi.Start();
        }

        public void Thread_Beta()
        {
            BETA[0] = BETAMI;
            if (NBETA > 1)
            {
                delta = (BETAMX - BETAMI) / (double)(NBETA - 1);
                for (int j = 2; j <= NBETA; j++)
                    BETA[j - 1] = BETA[0] + delta * (double)(j - 1);
            }
        }
        public void Thread_Theta()
        {
            if (NPHI == 1 && NTHETA > 1)
            {
                delta = (Math.Cos(THETMX) - Math.Cos(THETMI)) / (double)(NTHETA - 1);
                THETA[0] = THETMI;
            }
            else
            {
                delta = (Math.Cos(THETMX) - Math.Cos(THETMI)) / (double)(NTHETA);
                THETA[0] = Math.Acos(Math.Cos(THETMI) + 0.5 * delta);
            }
            if (NTHETA > 1)
            {
                for (int j = 2; j <= NTHETA; j++)
                    THETA[j - 1] = Math.Acos(Math.Cos(THETA[0]) + delta * (double)(j - 1));
            }
        }
        public void Thread_Phi()
        {
            PHI[0] = PHIMIN;
            if (NPHI > 1)
            {
                delta = (PHIMAX - PHIMIN) / (double)(NPHI - 1);
                for (int j = 2; j <= NPHI; j++)
                    PHI[j - 1] = PHI[0] + delta * (double)(j - 1);
            }
        }
    }
}

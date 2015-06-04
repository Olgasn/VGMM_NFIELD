using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMM_FIELD
{
    class func_lnfacd
    {
        public double lnfacd(double z)
        {
            double[] c0 = new double[11] { 0.16427423239836267*Math.Pow(10,5), -0.48589401600331902*Math.Pow(10,5),
                                            0.55557391003815523*Math.Pow(10,5), -0.30964901015912058*Math.Pow(10,5),
                                            0.87287202992571788*Math.Pow(10,4), -0.11714474574532352*Math.Pow(10,4),
                                            0.63103078123601037*Math.Pow(10,2), -0.93060589791758878,
                                            0.13919002438227877*Math.Pow(10,-2),-0.45006835613027859*Math.Pow(10,-8),
                                            0.13069587914063262*Math.Pow(10,-9)};
            double a = 1;
            double cp = 2.5066282746310005;
            double b = z + 10.5;
            b = (z + 0.5) * Math.Log(b, Math.E) - b;
            for (int i = 1; i <= 11; i++)
            {
                z = z + 1;
                a = a + c0[i - 1] / z;
            }
            return b + Math.Log(cp * a, Math.E);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

namespace GMM_FIELD
{
    class mueller
    {
        public mueller(Complex s1, Complex s2, Complex s3, Complex s4, double[,] s)
        {
            double s1s, s2s, s3s, s4s;
            Complex s2s3c, s1s4c, s2s4c, s1s3c, s1s2c, s3s4c, s2s1c, s4s3c, s2cs4, s3cs1;
            s1s = Math.Pow(Complex.Abs(s1), 2);
            s2s = Math.Pow(Complex.Abs(s2), 2);
            s3s = Math.Pow(Complex.Abs(s3), 2);
            s4s = Math.Pow(Complex.Abs(s4), 2);
            s2s3c = s2 * Complex.Conjugate(s3);
            s1s4c = s1 * Complex.Conjugate(s4);
            s2s4c = s2 * Complex.Conjugate(s4);
            s1s3c = s1 * Complex.Conjugate(s3);
            s1s2c = s1 * Complex.Conjugate(s2);
            s3s4c = s3 * Complex.Conjugate(s4);
            s2s1c = s2 * Complex.Conjugate(s1);
            s4s3c = s4 * Complex.Conjugate(s3);
            s2cs4 = Complex.Conjugate(s2) * s4;
            s3cs1 = Complex.Conjugate(s3) * s1;
            s[0, 0] = 0.5 * (s1s + s2s + s3s + s4s);
            s[0, 1] = 0.5 * (s2s - s1s + s4s - s3s);
            s[0, 2] = (s2s3c + s1s4c).Real;
            s[0, 3] = (s2s3c - s1s4c).Imaginary;
            s[1, 0] = 0.5 * (s2s - s1s - s4s + s3s);
            s[1, 1] = 0.5 * (s2s + s1s - s4s - s3s);
            s[1, 2] = (s2s3c - s1s4c).Real;
            s[1, 3] = (s2s3c + s1s4c).Imaginary;
            s[2, 0] = (s2s4c + s1s3c).Real;
            s[2, 1] = (s2s4c - s1s3c).Real;
            s[2, 2] = (s1s2c + s3s4c).Real;
            s[2, 3] = (s2s1c + s4s3c).Imaginary;
            s[3, 0] = (s2cs4 + s3cs1).Imaginary;
            s[3, 1] = (s2cs4 - s3cs1).Imaginary;
            s[3, 2] = (s1s2c - s3s4c).Imaginary;
            s[3, 3] = (s1s2c - s3s4c).Real;
        }
    }
}

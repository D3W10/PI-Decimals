using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace PI_Decimals
{
    public static class BigMath
    {
        public static BigInteger GetPi(int digits, int iterations)
        {
            return 16 * ArcTan1OverX(5, digits).ElementAt(iterations) - 4 * ArcTan1OverX(239, digits).ElementAt(iterations);
        }

        public static IEnumerable<BigInteger> ArcTan1OverX(int x, int digits)
        {
            var mag = BigInteger.Pow(10, digits);
            var sum = BigInteger.Zero;
            bool sign = true;

            for (int i = 1; true; i += 2)
            {
                var cur = mag / (BigInteger.Pow(x, i) * i);

                if (sign)
                    sum += cur;
                else
                    sum -= cur;

                yield return sum;
                sign = !sign;
            }
        }
    }
}
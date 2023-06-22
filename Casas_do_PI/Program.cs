using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace Casas_do_PI
{
    internal class Program
    {
        static void Main()
        {
            Console.Write("A calcular as casas de PI. Por favor aguarde...");
            string pi = BigMath.GetPi(1500, 3000).ToString();
            pi = pi.Remove(0, 1);
            char[] piSplit = pi.ToCharArray();
            Console.Clear();

            while (true)
            {
                Console.Write("Insira a posição da casa decimal do PI: ");
                try
                {
                    int posicao = Convert.ToInt32(Console.ReadLine());
                    if (posicao < 1 || posicao > 1500)
                    {
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.WriteLine("A posição deve estar entre 1 e 1500!");
                    }
                    else
                    {
                        Console.Write("Valor: ");
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.WriteLine(piSplit[posicao - 1]);
                    }
                }
                catch (FormatException)
                {
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine("Deve ser inserido um número!");
                }
                Console.ResetColor();
                Console.WriteLine("\n");
            }
        }
    }

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
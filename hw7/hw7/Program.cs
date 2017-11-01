using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using System.Numerics;

namespace hw7
{
    class Program
    {
        static void Main(string[] args)
        {
            string line;
            while ((line = Console.ReadLine()) != null)
            {
                string[] split = line.Split(new[] {' '});
                if (split[0] == "gcd")
                {
                    long gcd1 = Int64.Parse(split[1]);
                    long gcd2 = Int64.Parse(split[2]);
                    var answer = gcd(gcd1,gcd2);
                    Console.WriteLine(answer);
                }
                else if (split[0] == "exp")
                {
                    long exp1 = Int64.Parse(split[1]);
                    long exp2 = Int64.Parse(split[2]);
                    long exp3 = Int64.Parse(split[3]);
                    var answer = exp(exp1, exp2, exp3);
                    Console.WriteLine(answer);
                }
                else if (split[0] == "inverse")
                {
                    long inverse1 = Int64.Parse(split[1]);
                    long inverse2 = Int64.Parse(split[2]);
                    var answer = inverse(inverse1, inverse2);

                    if(answer == -1)
                    {
                        Console.WriteLine("none");
                    }
                    else
                        Console.WriteLine(answer);
                }
                else if (split[0] == "isprime")
                {
                    long isprime1 = Int64.Parse(split[1]);
                    var answer = isprime(isprime1);
                    Console.WriteLine(answer);
                }
                else if (split[0] == "key")
                {
                    long key1 = Int64.Parse(split[1]);
                    long key2 = Int64.Parse(split[2]);
                    var answer = key(key1, key2);
                    Console.WriteLine(answer);
                }
            }
        }

        private static string key(long key1, long key2)
        {
            return keyRSA(key1, key2);
        }

        private static string keyRSA(long p, long q)
        {
            long N = p * q;
            long Euler = (p - 1) * (q - 1);
            long e = 0;
            for (int i = 2; i <= Euler; i++)
            {
                if (gcd(i, Euler) == 1)
                {
                    e = i;
                    break;
                }
            }

            long d = ModInverse(e, Euler);
            return N + " " + e + " " + d;
        }

        private static long ModInverse(long a, long n)
        {
            long i = n, v = 0, d = 1;
            while (a > 0)
            {
                long t = i / a, x = a;
                a = i % x;
                i = x;
                x = d;
                d = v - t * x;
                v = x;
            }
            v %= n;
            if (v < 0) v = (v + n) % n;
            return v;
        }

        private static string isprime(long isprime1)
        {
            if(isprime1 % 2 == 0 || isprime1 < 2)
            {
                return "no";
            }
            else if (isprime1 == 2)
            {
                return "yes";
            }

            for (int i = 3; i * i <= isprime1; i+=2)
            {
                if(isprime1 % i == 0)
                {
                    return "no";
                }
            }
            return "yes";

        }

        private static long inverse(long inverse1, long inverse2)
        {
            long x = 0;
            long y = 0;

            long gcd = gcdHelper(inverse1, inverse2, ref x, ref y);

            if (gcd != 1)
                return -1;
            else
            {
                // m is added to handle negative x
                long res = (x % inverse2 + inverse2) % inverse2;
                return res;
            }
        }

        private static long gcdHelper(long a, long b, ref long x, ref long y)
        {
            // Base Case
            if (a == 0)
            {
                x = 0;
                y = 1;
                return b;
            }

            long x1 = 0;
            long y1 = 0; // To store results of recursive call
            long gcd = gcdHelper(b % a, a, ref x1, ref y1);

            // Update x and y using results of recursive
            // call
            x = y1 - (b / a) * x1;
            y = x1;

            return gcd;
        }


        private static int exp(long exp1, long exp2, long exp3)
        {
            long x = 1;
            long y = exp1;
            while (exp2 > 0)
            {
                if (exp2 % 2 == 1)
                {
                    x = (x * y) % exp3;
                }
                y = (y * y) % exp3;
                exp2 /= 2;
            }
            return (int)(x % exp3);
        }

        private static long gcd(long gcd1, long gcd2)
        {
            if (gcd2 == 0)
            {
                return gcd1;
            }
            return gcd(gcd2, gcd1 % gcd2);
        }
    }
}

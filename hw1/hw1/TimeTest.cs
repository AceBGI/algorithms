using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hw1
{
    class TimeTest
    {
        /// <summary>
        /// The number of repetitions used in versions 2 and 3
        /// </summary>
        public const int REPTS = 1000;

        /// <summary>
        /// The size of the array in versions 1 through 5
        /// </summary>
        public const int SIZE = 1023;

        /// <summary>
        /// Returns the number of milliseconds that have elapsed on the Stopwatch.
        /// </summary>
        public static double msecs(Stopwatch sw)
        {
            return (((double)sw.ElapsedTicks) / Stopwatch.Frequency) * 1000;
        }

        public static double TimeAnaga(int size)
        {
            // Construct anagrams
            List<string> input = new List<string>();
            input.Add("6 4");
            input.Add("tape");
            input.Add("rate");
            input.Add("seat");
            input.Add("pate");
            input.Add("east");
            input.Add("pest");

            // Create a stopwatch
            Stopwatch sw = new Stopwatch();

            // Make a single measurement of REPTS operations
            sw.Start();
            for (int i = 0; i < REPTS; i++)
            {
                Program.Anaga(input);
            }
            sw.Stop();
            double totalAverage = msecs(sw) / REPTS;

            // Create a new stopwatch
            sw = new Stopwatch();

            // Repeat, but don't actually do the binary search
            sw.Start();
            for (int i = 0; i < REPTS; i++)
            {
                //BinarySearch(data, s);
            }
            sw.Stop();
            double overheadAverage = msecs(sw) / REPTS;

            // Display the raw data as a sanity check
            Console.WriteLine("Total avg:    " + totalAverage.ToString("G2"));
            Console.WriteLine("Overhead avg: " + overheadAverage.ToString("G2"));

            // Return the difference
            return totalAverage - overheadAverage;
        }
    }
}

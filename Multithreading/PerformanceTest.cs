using System;
using System.Diagnostics;
using System.Threading;

namespace Multithreading
{
    // Copied from http://www.dotnetperls.com/interlocked
    public static class PerformanceTest
    {
        private static readonly object Locker = new object();
        static int _test;
        private const int Max = 10000000;

        public static void Test()
        {
            // Performance of lock()
            var s1 = Stopwatch.StartNew();
            for (int i = 0; i < Max; i++)
            {
                lock (Locker)
                {
                    _test++;
                }
            }
            s1.Stop();

            // Performance of Interlock
            var s2 = Stopwatch.StartNew();
            for (int i = 0; i < Max; i++)
            {
                Interlocked.Increment(ref _test);
            }
            s2.Stop();

            // show me the money
            Console.WriteLine(_test);
            Console.WriteLine(((double)(s1.Elapsed.TotalMilliseconds * 1000000) /
                Max).ToString("0.00 ns"));
            Console.WriteLine(((double)(s2.Elapsed.TotalMilliseconds * 1000000) /
                Max).ToString("0.00 ns"));
            Console.Read();
        }
    }
}

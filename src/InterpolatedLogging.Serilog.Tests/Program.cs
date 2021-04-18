using Serilog.Events;
using System;

namespace InterpolatedLoggingTests.Serilog
{
    class Program
    {
        static void Main(string[] args)
        {
            new BenchmarkTests().BenchmarkTest1Parameter(LogEventLevel.Debug);
            Console.WriteLine("Press any key to continue"); Console.ReadLine();

            new BenchmarkTests().BenchmarkTest1Parameter(LogEventLevel.Fatal);
            Console.WriteLine("Press any key to continue"); Console.ReadLine();

            new BenchmarkTests().BenchmarkTest4Parameters(LogEventLevel.Debug);
            Console.WriteLine("Press any key to continue"); Console.ReadLine();

            new BenchmarkTests().BenchmarkTest4Parameters(LogEventLevel.Fatal);
            Console.WriteLine("Press any key to exit"); Console.ReadLine();
        }
    }
}

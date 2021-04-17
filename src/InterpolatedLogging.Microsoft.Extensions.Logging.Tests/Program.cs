using System;

namespace InterpolatedLoggingTests.MicrosoftExtensionsLogging
{
    class Program
    {
        static void Main(string[] args)
        {
            new BenchmarkTests().BenchmarkTest1Parameter(Microsoft.Extensions.Logging.LogLevel.Debug);
            Console.WriteLine("Press any key to continue"); Console.ReadLine();

            new BenchmarkTests().BenchmarkTest1Parameter(Microsoft.Extensions.Logging.LogLevel.Critical);
            Console.WriteLine("Press any key to continue"); Console.ReadLine();

            new BenchmarkTests().BenchmarkTest4Parameters(Microsoft.Extensions.Logging.LogLevel.Debug);
            Console.WriteLine("Press any key to continue"); Console.ReadLine();

            new BenchmarkTests().BenchmarkTest4Parameters(Microsoft.Extensions.Logging.LogLevel.Critical);
            Console.WriteLine("Press any key to exit"); Console.ReadLine();
        }
    }
}

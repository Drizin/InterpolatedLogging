using NUnit.Framework;
using Serilog;
using Serilog.Core;
using Serilog.Events;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Threading;
using static InterpolatedLogging.NamedProperties;

namespace InterpolatedLoggingTests.Serilog
{
    public class BenchmarkTests
    {

        static ILogger CreateLogger(LogEventLevel logLevel, bool console = true)
        {
            var builder = new LoggerConfiguration()
                .MinimumLevel.Is(logLevel);
            if (console)
                builder.WriteTo.Console();
            //else
            //    builder.WriteTo...
            return builder.CreateLogger();
        }

        [TestCase(LogEventLevel.Debug)]
        [TestCase(LogEventLevel.Fatal)]
        public void BenchmarkTest1Parameter(LogEventLevel logLevel)
        {
            int iterations = (logLevel == LogEventLevel.Debug ? 100000 : 10000000); // printing makes it very slow, so we have to reduce the iterations
            Console.WriteLine($"Running test {nameof(BenchmarkTest1Parameter)} with level {logLevel} ({iterations} iterations)...");

            var logger = CreateLogger(logLevel);

            // Pure Serilog
            Console.WriteLine("Pure Serilog..."); Thread.Sleep(2000);
            var sw = Stopwatch.StartNew();
            for (int i = 0; i < iterations; i++)
            {
                logger.Debug("Could not open socket to `{hostName}`", Environment.MachineName);
            }
            sw.Stop();
            long elapsedMilliseconds1 = sw.ElapsedMilliseconds;


            // InterpolatedLogging
            Console.WriteLine("InterpolatedLogging..."); Thread.Sleep(2000);
            sw = Stopwatch.StartNew();
            for (int i = 0; i < iterations; i++)
            {
                logger.InterpolatedDebug($"Could not open socket to `{Environment.MachineName:hostName}`");
            }
            sw.Stop();
            long elapsedMilliseconds2 = sw.ElapsedMilliseconds;

            Thread.Sleep(3000); // let loggers flush their buffers
            Console.WriteLine($"Pure Serilog (1 argument): {elapsedMilliseconds1}ms");
            Console.WriteLine($"InterpolatedLogging (1 argument): {elapsedMilliseconds2}ms");
        }

        [Test]
        [TestCase(LogEventLevel.Debug)]
        [TestCase(LogEventLevel.Fatal)]
        public void BenchmarkTest4Parameters(LogEventLevel logLevel)
        {
            int iterations = (logLevel == LogEventLevel.Debug ? 100000 : 10000000); // printing makes it very slow, so we have to reduce the iterations
            Console.WriteLine($"Running test {nameof(BenchmarkTest4Parameters)} with level {logLevel} ({iterations} iterations)...");

            var logger = CreateLogger(logLevel);

            // Pure Serilog
            Console.WriteLine("Pure Serilog..."); Thread.Sleep(2000);
            var sw = Stopwatch.StartNew();
            for (int i = 0; i < iterations; i++)
            {
                logger.Debug("Could not open socket to `{hostName}` at {Timestamp} with amount {Amount} - flag is {Flag}", Environment.MachineName, DateTime.Now, 10m, true);
            }
            sw.Stop();
            long elapsedMilliseconds1 = sw.ElapsedMilliseconds;

            // InterpolatedLogging
            Console.WriteLine("InterpolatedLogging..."); Thread.Sleep(2000);
            sw = Stopwatch.StartNew();
            for (int i = 0; i < iterations; i++)
            {
                logger.InterpolatedDebug($"Could not open socket to `{Environment.MachineName:hostName}` at {DateTime.Now:Timestamp} with amount {10m:Amount} - flag is {true:Flag}");
            }
            sw.Stop();
            long elapsedMilliseconds2 = sw.ElapsedMilliseconds;


            Thread.Sleep(3000); // let loggers flush their buffers
            Console.WriteLine($"Pure Serilog (4 arguments): {elapsedMilliseconds1}ms");
            Console.WriteLine($"InterpolatedLogging (4 arguments): {elapsedMilliseconds2}ms");
        }



    }

}

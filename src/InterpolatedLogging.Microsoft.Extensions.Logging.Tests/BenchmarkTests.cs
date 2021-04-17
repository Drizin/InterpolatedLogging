using Microsoft.Extensions.Logging;
using NUnit.Framework;
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

namespace InterpolatedLoggingTests.MicrosoftExtensionsLogging
{
    public class BenchmarkTests
    {

        static ILogger CreateLogger(LogLevel logLevel, bool console = true)
        {
            var fac = LoggerFactory.Create((builder) => 
            {
                builder.SetMinimumLevel(logLevel);
                if (console)
                    builder.AddConsole();
                else
                    builder.AddEventLog();
            });
            var logger = new Logger<int>(fac);
            return logger;
        }

        [TestCase(LogLevel.Debug)]
        [TestCase(LogLevel.Critical)]
        public void BenchmarkTest1Parameter(LogLevel logLevel)
        {
            int iterations = (logLevel == LogLevel.Debug ? 100000 : 10000000); // printing makes it very slow, so we have to reduce the iterations
            Console.WriteLine($"Running test {nameof(BenchmarkTest1Parameter)} with level {logLevel} ({iterations} iterations)...");

            var logger = CreateLogger(logLevel);

            // Microsoft Logging Generator ( https://github.com/geeknoid/LoggingGenerator )
            Console.WriteLine("Microsoft Logging Generator..."); Thread.Sleep(2000);
            var sw = Stopwatch.StartNew();
            for (int i = 0; i < iterations; i++)
            {
                Log.CouldNotOpenSocket(logger, Environment.MachineName);
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
            Console.WriteLine($"Microsoft Logging Generator (1 argument): {elapsedMilliseconds1}ms");
            Console.WriteLine($"InterpolatedLogging (1 argument): {elapsedMilliseconds2}ms");
        }

        [Test]
        [TestCase(LogLevel.Debug)]
        [TestCase(LogLevel.Critical)]
        public void BenchmarkTest4Parameters(LogLevel logLevel)
        {
            int iterations = (logLevel == LogLevel.Debug ? 100000 : 10000000); // printing makes it very slow, so we have to reduce the iterations
            Console.WriteLine($"Running test {nameof(BenchmarkTest4Parameters)} with level {logLevel} ({iterations} iterations)...");

            var logger = CreateLogger(logLevel);

            // Microsoft Logging Generator ( https://github.com/geeknoid/LoggingGenerator )
            Console.WriteLine("Microsoft Logging Generator..."); Thread.Sleep(2000);
            var sw = Stopwatch.StartNew();
            for (int i = 0; i < iterations; i++)
            {
                Log.FourArgumentsLog(logger, Environment.MachineName, DateTime.Now, 10, true);
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
            Console.WriteLine($"Microsoft Logging Generator (4 arguments): {elapsedMilliseconds1}ms");
            Console.WriteLine($"InterpolatedLogging (4 arguments): {elapsedMilliseconds2}ms");
        }



    }

    static partial class Log
    {
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Extensions.Logging.Generators", "1.0.0.0")]
        private static readonly global::System.Action<global::Microsoft.Extensions.Logging.ILogger, string, global::System.Exception> __CouldNotOpenSocketCallback =
            global::Microsoft.Extensions.Logging.LoggerMessage.Define<string>(global::Microsoft.Extensions.Logging.LogLevel.Debug, new global::Microsoft.Extensions.Logging.EventId(0, nameof(CouldNotOpenSocket)), "Could not open socket to `{hostName}`");

        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Extensions.Logging.Generators", "1.0.0.0")]
        public static void CouldNotOpenSocket(global::Microsoft.Extensions.Logging.ILogger logger, string hostName)
        {
            if (logger.IsEnabled(global::Microsoft.Extensions.Logging.LogLevel.Debug))
            {
                __CouldNotOpenSocketCallback(logger, hostName, null);
            }
        }



        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Extensions.Logging.Generators", "1.0.0.0")]
        private static readonly global::System.Action<global::Microsoft.Extensions.Logging.ILogger, string, DateTime, decimal, bool, global::System.Exception> _FourArgumentsCallback =
            global::Microsoft.Extensions.Logging.LoggerMessage.Define<string, DateTime, decimal, bool>(global::Microsoft.Extensions.Logging.LogLevel.Debug, new global::Microsoft.Extensions.Logging.EventId(0, nameof(CouldNotOpenSocket)), "Could not open socket to `{hostName}` at {Timestamp} with amount {Amount} - flag is {Flag}");

        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Extensions.Logging.Generators", "1.0.0.0")]
        public static void FourArgumentsLog(global::Microsoft.Extensions.Logging.ILogger logger, string hostName, DateTime now, decimal amount, bool flag)
        {
            if (logger.IsEnabled(global::Microsoft.Extensions.Logging.LogLevel.Debug))
            {
                _FourArgumentsCallback(logger, hostName, now, amount, flag, null);
            }
        }

    }
}

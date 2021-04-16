using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;
using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using static InterpolatedLogging.NamedProperties;

namespace InterpolatedLogging.Tests
{
    public class BenchmarkTests
    {


        [Test]
        public void BenchmarkTest1()
        {
            System.Diagnostics.Debug.WriteLine($"---------");
            int iteractions = 100000;

            var fac = Microsoft.Extensions.Logging.LoggerFactory.Create((builder) => { builder.SetMinimumLevel(LogLevel.Debug).AddConsole(); });
            var logger = new Microsoft.Extensions.Logging.Logger<int>(fac);

            // Microsoft Logging Generator ( https://github.com/geeknoid/LoggingGenerator )
            var sw = Stopwatch.StartNew();
            for (int i = 0; i < iteractions; i++)
            {
                Log.CouldNotOpenSocket(logger, Environment.MachineName);
            }
            System.Diagnostics.Debug.WriteLine($"Microsoft Logging Generator (1 argument): {sw.ElapsedMilliseconds}ms");

            // InterpolatedLogging
            sw = Stopwatch.StartNew();
            for (int i = 0; i < iteractions; i++)
            {
                logger.InterpolatedDebug($"Could not open socket to `{Environment.MachineName:hostName}`");
            }
            System.Diagnostics.Debug.WriteLine($"InterpolatedLogging (1 argument): {sw.ElapsedMilliseconds}ms");
        }

        [Test]
        public void BenchmarkTest2()
        {
            System.Diagnostics.Debug.WriteLine($"---------");
            int iteractions = 100000;

            var fac = Microsoft.Extensions.Logging.LoggerFactory.Create((builder) => { builder.SetMinimumLevel(LogLevel.Debug).AddConsole(); });
            var logger = new Microsoft.Extensions.Logging.Logger<int>(fac);

            // Microsoft Logging Generator ( https://github.com/geeknoid/LoggingGenerator )
            var sw = Stopwatch.StartNew();
            for (int i = 0; i < iteractions; i++)
            {
                Log.FourArgumentsLog(logger, Environment.MachineName, DateTime.Now, 10, true);
            }
            System.Diagnostics.Debug.WriteLine($"Microsoft Logging Generator (4 arguments): {sw.ElapsedMilliseconds}ms");

            // InterpolatedLogging
            sw = Stopwatch.StartNew();
            for (int i = 0; i < iteractions; i++)
            {
                logger.InterpolatedDebug($"Could not open socket to `{Environment.MachineName:hostName}` at {DateTime.Now:Timestamp} with amount {10m:Amount} - flag is {true:Flag}");
            }
            System.Diagnostics.Debug.WriteLine($"InterpolatedLogging (4 arguments): {sw.ElapsedMilliseconds}ms");
        }



    }

    static partial class Log
    {
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Extensions.Logging.Generators", "1.0.0.0")]
        private static readonly global::System.Action<global::Microsoft.Extensions.Logging.ILogger, string, global::System.Exception> __CouldNotOpenSocketCallback =
            global::Microsoft.Extensions.Logging.LoggerMessage.Define<string>(global::Microsoft.Extensions.Logging.LogLevel.Critical, new global::Microsoft.Extensions.Logging.EventId(0, nameof(CouldNotOpenSocket)), "Could not open socket to `{hostName}`");

        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Extensions.Logging.Generators", "1.0.0.0")]
        public static void CouldNotOpenSocket(Microsoft.Extensions.Logging.ILogger logger, string hostName)
        {
            if (logger.IsEnabled(global::Microsoft.Extensions.Logging.LogLevel.Critical))
            {
                __CouldNotOpenSocketCallback(logger, hostName, null);
            }
        }



        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Extensions.Logging.Generators", "1.0.0.0")]
        private static readonly global::System.Action<global::Microsoft.Extensions.Logging.ILogger, string, DateTime, decimal, bool, global::System.Exception> _FourArgumentsCallback =
            global::Microsoft.Extensions.Logging.LoggerMessage.Define<string, DateTime, decimal, bool>(global::Microsoft.Extensions.Logging.LogLevel.Critical, new global::Microsoft.Extensions.Logging.EventId(0, nameof(CouldNotOpenSocket)), "Could not open socket to `{hostName}` at {Timestamp} with amount {Amount} - flag is {Flag}");

        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Extensions.Logging.Generators", "1.0.0.0")]
        public static void FourArgumentsLog(Microsoft.Extensions.Logging.ILogger logger, string hostName, DateTime now, decimal amount, bool flag)
        {
            if (logger.IsEnabled(global::Microsoft.Extensions.Logging.LogLevel.Critical))
            {
                _FourArgumentsCallback(logger, hostName, now, amount, flag, null);
            }
        }

    }
}

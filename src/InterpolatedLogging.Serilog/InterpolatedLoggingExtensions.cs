using InterpolatedLogging;
using Serilog.Events;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Serilog
{
    /// <summary>
    /// Extend Serilog ILogger with facades using Interpolated strings
    /// </summary>
    public static class InterpolatedLoggingExtensions
    {
        #region Facade Extensions
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        public static void InterpolatedVerbose(this ILogger logger, FormattableString message)
        {
            if (!logger.IsEnabled(LogEventLevel.Verbose))
                return;
            StructuredLogMessage msg = new StructuredLogMessage(message);
            logger.Verbose(msg.MessageTemplate, msg.Properties);
        }
        public static void InterpolatedVerbose(this ILogger logger, Exception exception, FormattableString message)
        {
            if (!logger.IsEnabled(LogEventLevel.Verbose))
                return;
            StructuredLogMessage msg = new StructuredLogMessage(message);
            logger.Verbose(exception, msg.MessageTemplate, msg.Properties);
        }
        public static void InterpolatedDebug(this ILogger logger, FormattableString message)
        {
            if (!logger.IsEnabled(LogEventLevel.Debug))
                return;
            StructuredLogMessage msg = new StructuredLogMessage(message);
            logger.Debug(msg.MessageTemplate, msg.Properties);
        }
        public static void InterpolatedDebug(this ILogger logger, Exception exception, FormattableString message)
        {
            if (!logger.IsEnabled(LogEventLevel.Debug))
                return;
            StructuredLogMessage msg = new StructuredLogMessage(message);
            logger.Debug(exception, msg.MessageTemplate, msg.Properties);
        }
        public static void InterpolatedInformation(this ILogger logger, FormattableString message)
        {
            if (!logger.IsEnabled(LogEventLevel.Information))
                return;
            StructuredLogMessage msg = new StructuredLogMessage(message);
            logger.Information(msg.MessageTemplate, msg.Properties);
        }
        public static void InterpolatedInformation(this ILogger logger, Exception exception, FormattableString message)
        {
            if (!logger.IsEnabled(LogEventLevel.Information))
                return;
            StructuredLogMessage msg = new StructuredLogMessage(message);
            logger.Information(exception, msg.MessageTemplate, msg.Properties);
        }
        public static void InterpolatedWarning(this ILogger logger, FormattableString message)
        {
            if (!logger.IsEnabled(LogEventLevel.Warning))
                return;
            StructuredLogMessage msg = new StructuredLogMessage(message);
            logger.Warning(msg.MessageTemplate, msg.Properties);
        }
        public static void InterpolatedWarning(this ILogger logger, Exception exception, FormattableString message)
        {
            if (!logger.IsEnabled(LogEventLevel.Warning))
                return;
            StructuredLogMessage msg = new StructuredLogMessage(message);
            logger.Warning(exception, msg.MessageTemplate, msg.Properties);
        }
        public static void InterpolatedError(this ILogger logger, FormattableString message)
        {
            if (!logger.IsEnabled(LogEventLevel.Error))
                return;
            StructuredLogMessage msg = new StructuredLogMessage(message);
            logger.Error(msg.MessageTemplate, msg.Properties);
        }
        public static void InterpolatedError(this ILogger logger, Exception exception, FormattableString message)
        {
            if (!logger.IsEnabled(LogEventLevel.Error))
                return;
            StructuredLogMessage msg = new StructuredLogMessage(message);
            logger.Error(exception, msg.MessageTemplate, msg.Properties);
        }
        public static void InterpolatedFatal(this ILogger logger, FormattableString message)
        {
            if (!logger.IsEnabled(LogEventLevel.Fatal))
                return;
            StructuredLogMessage msg = new StructuredLogMessage(message);
            logger.Fatal(msg.MessageTemplate, msg.Properties);
        }
        public static void InterpolatedFatal(this ILogger logger, Exception exception, FormattableString message)
        {
            if (!logger.IsEnabled(LogEventLevel.Fatal))
                return;
            StructuredLogMessage msg = new StructuredLogMessage(message);
            logger.Fatal(exception, msg.MessageTemplate, msg.Properties);
        }
        public static void InterpolatedWrite(this ILogger logger, LogEventLevel level, FormattableString message)
        {
            if (!logger.IsEnabled(level))
                return;
            StructuredLogMessage msg = new StructuredLogMessage(message);
            logger.Write(level, msg.MessageTemplate, msg.Properties);
        }
        public static void InterpolatedWrite(this ILogger logger, LogEventLevel level, Exception exception, FormattableString message)
        {
            if (!logger.IsEnabled(level))
                return;
            StructuredLogMessage msg = new StructuredLogMessage(message);
            logger.Write(level, exception, msg.MessageTemplate, msg.Properties);
        }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
        #endregion

    }
}

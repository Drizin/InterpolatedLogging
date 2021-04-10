using InterpolatedLogging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Serilog
{
    /// <summary>
    /// Extend Serilog ILogger with facades using Interpolated strings
    /// </summary>
    public static class SerilogExtensions
    {
        #region Serilog facades
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        public static void InterpolatedVerbose(this ILogger logger, FormattableString message)
        {
            if (!logger.IsEnabled(Events.LogEventLevel.Verbose))
                return;
            StructuredLogMessage msg = new StructuredLogMessage(message);
            logger.Verbose(msg.MessageTemplate, msg.Properties);
        }
        public static void InterpolatedVerbose(this ILogger logger, Exception exception, FormattableString message)
        {
            if (!logger.IsEnabled(Events.LogEventLevel.Verbose))
                return;
            StructuredLogMessage msg = new StructuredLogMessage(message);
            logger.Verbose(exception, msg.MessageTemplate, msg.Properties);
        }
        public static void InterpolatedDebug(this ILogger logger, FormattableString message)
        {
            if (!logger.IsEnabled(Events.LogEventLevel.Debug))
                return;
            StructuredLogMessage msg = new StructuredLogMessage(message);
            logger.Debug(msg.MessageTemplate, msg.Properties);
        }
        public static void InterpolatedDebug(this ILogger logger, Exception exception, FormattableString message)
        {
            if (!logger.IsEnabled(Events.LogEventLevel.Debug))
                return;
            StructuredLogMessage msg = new StructuredLogMessage(message);
            logger.Debug(exception, msg.MessageTemplate, msg.Properties);
        }
        public static void InterpolatedInformation(this ILogger logger, FormattableString message)
        {
            if (!logger.IsEnabled(Events.LogEventLevel.Information))
                return;
            StructuredLogMessage msg = new StructuredLogMessage(message);
            logger.Information(msg.MessageTemplate, msg.Properties);
        }
        public static void InterpolatedInformation(this ILogger logger, Exception exception, FormattableString message)
        {
            if (!logger.IsEnabled(Events.LogEventLevel.Information))
                return;
            StructuredLogMessage msg = new StructuredLogMessage(message);
            logger.Information(exception, msg.MessageTemplate, msg.Properties);
        }
        public static void InterpolatedWarning(this ILogger logger, FormattableString message)
        {
            if (!logger.IsEnabled(Events.LogEventLevel.Warning))
                return;
            StructuredLogMessage msg = new StructuredLogMessage(message);
            logger.Warning(msg.MessageTemplate, msg.Properties);
        }
        public static void InterpolatedWarning(this ILogger logger, Exception exception, FormattableString message)
        {
            if (!logger.IsEnabled(Events.LogEventLevel.Warning))
                return;
            StructuredLogMessage msg = new StructuredLogMessage(message);
            logger.Warning(exception, msg.MessageTemplate, msg.Properties);
        }
        public static void InterpolatedError(this ILogger logger, FormattableString message)
        {
            if (!logger.IsEnabled(Events.LogEventLevel.Error))
                return;
            StructuredLogMessage msg = new StructuredLogMessage(message);
            logger.Error(msg.MessageTemplate, msg.Properties);
        }
        public static void InterpolatedError(this ILogger logger, Exception exception, FormattableString message)
        {
            if (!logger.IsEnabled(Events.LogEventLevel.Error))
                return;
            StructuredLogMessage msg = new StructuredLogMessage(message);
            logger.Error(exception, msg.MessageTemplate, msg.Properties);
        }
        public static void InterpolatedFatal(this ILogger logger, FormattableString message)
        {
            if (!logger.IsEnabled(Events.LogEventLevel.Fatal))
                return;
            StructuredLogMessage msg = new StructuredLogMessage(message);
            logger.Fatal(msg.MessageTemplate, msg.Properties);
        }
        public static void InterpolatedFatal(this ILogger logger, Exception exception, FormattableString message)
        {
            if (!logger.IsEnabled(Events.LogEventLevel.Fatal))
                return;
            StructuredLogMessage msg = new StructuredLogMessage(message);
            logger.Fatal(exception, msg.MessageTemplate, msg.Properties);
        }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
        #endregion

    }
}

using InterpolatedLogging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace NLog
{
    /// <summary>
    /// Extend NLog ILogger with facades using Interpolated strings
    /// </summary>
    public static class InterpolatedLoggingExtensions
    {
        #region Facade Extensions
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        public static void InterpolatedTrace(this ILogger logger, FormattableString message)
        {
            if (!logger.IsEnabled(LogLevel.Trace))
                return;
            StructuredLogMessage msg = new StructuredLogMessage(message);
            logger.Trace(msg.MessageTemplate, msg.Properties);
        }
        public static void InterpolatedTrace(this ILogger logger, Exception exception, FormattableString message)
        {
            if (!logger.IsEnabled(LogLevel.Trace))
                return;
            StructuredLogMessage msg = new StructuredLogMessage(message);
            logger.Trace(exception, msg.MessageTemplate, msg.Properties);
        }
        public static void InterpolatedDebug(this ILogger logger, FormattableString message)
        {
            if (!logger.IsEnabled(LogLevel.Debug))
                return;
            StructuredLogMessage msg = new StructuredLogMessage(message);
            logger.Debug(msg.MessageTemplate, msg.Properties);
        }
        public static void InterpolatedDebug(this ILogger logger, Exception exception, FormattableString message)
        {
            if (!logger.IsEnabled(LogLevel.Debug))
                return;
            StructuredLogMessage msg = new StructuredLogMessage(message);
            logger.Debug(exception, msg.MessageTemplate, msg.Properties);
        }
        public static void InterpolatedInfo(this ILogger logger, FormattableString message)
        {
            if (!logger.IsEnabled(LogLevel.Info))
                return;
            StructuredLogMessage msg = new StructuredLogMessage(message);
            logger.Info(msg.MessageTemplate, msg.Properties);
        }
        public static void InterpolatedInfo(this ILogger logger, Exception exception, FormattableString message)
        {
            if (!logger.IsEnabled(LogLevel.Info))
                return;
            StructuredLogMessage msg = new StructuredLogMessage(message);
            logger.Info(exception, msg.MessageTemplate, msg.Properties);
        }
        public static void InterpolatedWarn(this ILogger logger, FormattableString message)
        {
            if (!logger.IsEnabled(LogLevel.Warn))
                return;
            StructuredLogMessage msg = new StructuredLogMessage(message);
            logger.Warn(msg.MessageTemplate, msg.Properties);
        }
        public static void InterpolatedWarn(this ILogger logger, Exception exception, FormattableString message)
        {
            if (!logger.IsEnabled(LogLevel.Warn))
                return;
            StructuredLogMessage msg = new StructuredLogMessage(message);
            logger.Warn(exception, msg.MessageTemplate, msg.Properties);
        }
        public static void InterpolatedError(this ILogger logger, FormattableString message)
        {
            if (!logger.IsEnabled(LogLevel.Error))
                return;
            StructuredLogMessage msg = new StructuredLogMessage(message);
            logger.Error(msg.MessageTemplate, msg.Properties);
        }
        public static void InterpolatedError(this ILogger logger, Exception exception, FormattableString message)
        {
            if (!logger.IsEnabled(LogLevel.Error))
                return;
            StructuredLogMessage msg = new StructuredLogMessage(message);
            logger.Error(exception, msg.MessageTemplate, msg.Properties);
        }
        public static void InterpolatedFatal(this ILogger logger, FormattableString message)
        {
            if (!logger.IsEnabled(LogLevel.Fatal))
                return;
            StructuredLogMessage msg = new StructuredLogMessage(message);
            logger.Fatal(msg.MessageTemplate, msg.Properties);
        }
        public static void InterpolatedFatal(this ILogger logger, Exception exception, FormattableString message)
        {
            if (!logger.IsEnabled(LogLevel.Fatal))
                return;
            StructuredLogMessage msg = new StructuredLogMessage(message);
            logger.Fatal(exception, msg.MessageTemplate, msg.Properties);
        }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
        #endregion

    }
}

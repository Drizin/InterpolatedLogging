using InterpolatedLogging;
using Microsoft.Extensions.Logging;
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
        public static void InterpolatedTrace(this ILogger logger, FormattableString message)
        {
            if (!logger.IsEnabled(LogLevel.Trace))
                return;
            StructuredLogMessage msg = new StructuredLogMessage(message);
            logger.LogTrace(msg.MessageTemplate, msg.Properties);
        }
        public static void InterpolatedTrace(this ILogger logger, Exception exception, FormattableString message)
        {
            if (!logger.IsEnabled(LogLevel.Trace))
                return;
            StructuredLogMessage msg = new StructuredLogMessage(message);
            logger.LogTrace(exception, msg.MessageTemplate, msg.Properties);
        }
        public static void InterpolatedDebug(this ILogger logger, FormattableString message)
        {
            if (!logger.IsEnabled(LogLevel.Debug))
                return;
            StructuredLogMessage msg = new StructuredLogMessage(message);
            logger.LogDebug(msg.MessageTemplate, msg.Properties);
        }
        public static void InterpolatedDebug(this ILogger logger, Exception exception, FormattableString message)
        {
            if (!logger.IsEnabled(LogLevel.Debug))
                return;
            StructuredLogMessage msg = new StructuredLogMessage(message);
            logger.LogDebug(exception, msg.MessageTemplate, msg.Properties);
        }
        public static void InterpolatedInformation(this ILogger logger, FormattableString message)
        {
            if (!logger.IsEnabled(LogLevel.Information))
                return;
            StructuredLogMessage msg = new StructuredLogMessage(message);
            logger.LogInformation(msg.MessageTemplate, msg.Properties);
        }
        public static void InterpolatedInformation(this ILogger logger, Exception exception, FormattableString message)
        {
            if (!logger.IsEnabled(LogLevel.Information))
                return;
            StructuredLogMessage msg = new StructuredLogMessage(message);
            logger.LogInformation(exception, msg.MessageTemplate, msg.Properties);
        }
        public static void InterpolatedWarning(this ILogger logger, FormattableString message)
        {
            if (!logger.IsEnabled(LogLevel.Warning))
                return;
            StructuredLogMessage msg = new StructuredLogMessage(message);
            logger.LogWarning(msg.MessageTemplate, msg.Properties);
        }
        public static void InterpolatedWarning(this ILogger logger, Exception exception, FormattableString message)
        {
            if (!logger.IsEnabled(LogLevel.Warning))
                return;
            StructuredLogMessage msg = new StructuredLogMessage(message);
            logger.LogWarning(exception, msg.MessageTemplate, msg.Properties);
        }
        public static void InterpolatedError(this ILogger logger, FormattableString message)
        {
            if (!logger.IsEnabled(LogLevel.Error))
                return;
            StructuredLogMessage msg = new StructuredLogMessage(message);
            logger.LogError(msg.MessageTemplate, msg.Properties);
        }
        public static void InterpolatedError(this ILogger logger, Exception exception, FormattableString message)
        {
            if (!logger.IsEnabled(LogLevel.Error))
                return;
            StructuredLogMessage msg = new StructuredLogMessage(message);
            logger.LogError(exception, msg.MessageTemplate, msg.Properties);
        }
        public static void InterpolatedCritical(this ILogger logger, FormattableString message)
        {
            if (!logger.IsEnabled(LogLevel.Critical))
                return;
            StructuredLogMessage msg = new StructuredLogMessage(message);
            logger.LogCritical(msg.MessageTemplate, msg.Properties);
        }
        public static void InterpolatedCritical(this ILogger logger, Exception exception, FormattableString message)
        {
            if (!logger.IsEnabled(LogLevel.Critical))
                return;
            StructuredLogMessage msg = new StructuredLogMessage(message);
            logger.LogCritical(exception, msg.MessageTemplate, msg.Properties);
        }
        public static void InterpolatedLog(this ILogger logger, LogLevel logLevel, FormattableString message)
        {
            if (!logger.IsEnabled(logLevel))
                return;
            StructuredLogMessage msg = new StructuredLogMessage(message);
            logger.Log(logLevel, msg.MessageTemplate, msg.Properties);
        }
        public static void InterpolatedLog(this ILogger logger, LogLevel logLevel, Exception exception, FormattableString message)
        {
            if (!logger.IsEnabled(logLevel))
                return;
            StructuredLogMessage msg = new StructuredLogMessage(message);
            logger.Log(logLevel, exception, msg.MessageTemplate, msg.Properties);
        }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
        #endregion

    }
}

// Copyright (c) 2025 MeiHasCrashed
// License: AGPL-3.0-or-later (see /License.md)

using System.Text;
using Dalamud.Plugin.Services;
using Microsoft.Extensions.Logging;
using Serilog.Events;

namespace Postmoogle.Core.Logging;

public class DalamudLogger(string name, IPluginLog logger) : ILogger
{

    private static readonly Dictionary<LogLevel, LogEventLevel> LevelMap = new()
    {
        { LogLevel.Trace, LogEventLevel.Verbose },
        { LogLevel.Debug, LogEventLevel.Debug },
        { LogLevel.Information, LogEventLevel.Information },
        { LogLevel.Warning, LogEventLevel.Warning },
        { LogLevel.Error, LogEventLevel.Error },
        { LogLevel.Critical, LogEventLevel.Fatal }
    };

    public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception?, string> formatter)
    {
        if (!IsEnabled(logLevel)) return;
        var sb = new StringBuilder();
        sb.Append($"[{name}] {state} {exception?.Message}");
        if (!string.IsNullOrWhiteSpace(exception?.StackTrace))
        {
            sb.AppendLine(exception.StackTrace);
        }
        var innerException = exception?.InnerException;
        while (innerException != null)
        {
            sb.AppendLine($"Inner Exception: {innerException}: {innerException.Message}");
            if (!string.IsNullOrWhiteSpace(innerException.StackTrace))
            {
                sb.AppendLine(innerException.StackTrace);
            }
            innerException = innerException.InnerException;
        }

        logger.Write(LevelMap[logLevel], null, sb.ToString());
    }

    // For now, we always enable the logger, and just hand the information over to dalamud.
    public bool IsEnabled(LogLevel logLevel) => true;

    public IDisposable? BeginScope<TState>(TState state) where TState : notnull => null!;
}

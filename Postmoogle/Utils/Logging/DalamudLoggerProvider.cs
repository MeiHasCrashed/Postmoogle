// Copyright (c) 2025 MeiHasCrashed
// License: AGPL-3.0-or-later (see /License.md)

using System.Collections.Concurrent;
using Dalamud.Plugin.Services;
using Microsoft.Extensions.Logging;

namespace Postmoogle.Utils.Logging;

[ProviderAlias("Dalamud")]
public class DalamudLoggerProvider(IPluginLog logger) : ILoggerProvider
{
    private readonly ConcurrentDictionary<string, DalamudLogger> _loggers = new(StringComparer.OrdinalIgnoreCase);

    public ILogger CreateLogger(string categoryName) =>
        _loggers.GetOrAdd(categoryName, new DalamudLogger(categoryName, logger));

    public void Dispose()
    {
        GC.SuppressFinalize(this);
        _loggers.Clear();
    }
}

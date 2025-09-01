// Copyright (c) 2025 MeiHasCrashed
// License: AGPL-3.0-or-later (see /License.md)

using Dalamud.Plugin.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;

namespace Postmoogle.Utils.Logging;

public static class DalamudLoggingExtensions
{
    public static ILoggingBuilder AddDalamudLogging(this ILoggingBuilder builder, IPluginLog logger)
    {
        builder.Services.TryAddEnumerable(
            ServiceDescriptor.Singleton<ILoggerProvider, DalamudLoggerProvider>(_ =>
                new DalamudLoggerProvider(logger)));

        return builder;
    }
}

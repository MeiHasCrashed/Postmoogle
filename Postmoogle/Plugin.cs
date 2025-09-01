// Copyright (c) 2025 MeiHasCrashed
// License: AGPL-3.0-or-later (see /License.md)

using System.Diagnostics.CodeAnalysis;
using Dalamud.Plugin;
using Dalamud.Plugin.Services;
using JetBrains.Annotations;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Postmoogle.Utils.Logging;

namespace Postmoogle;

[UsedImplicitly]
public class Plugin : IDalamudPlugin
{

    private readonly IHost _pluginHost;

    public Plugin(IDalamudPluginInterface pluginInterface, IPluginLog pluginLog)
    {
        var builder = Host.CreateEmptyApplicationBuilder(new HostApplicationBuilderSettings
        {
            ApplicationName = "Postmoogle",
            ContentRootPath = pluginInterface.ConfigDirectory.FullName,
            DisableDefaults = true,
            EnvironmentName = pluginInterface.IsDev ? "Development" : "Production"
        });

        builder.Logging
            .ClearProviders()
            .SetMinimumLevel(LogLevel.Trace)
            .AddDalamudLogging(pluginLog);

        builder.Services
            .AddHostedService<PostmooglePlugin>();

        _pluginHost = builder.Build();
        _ = _pluginHost.StartAsync();
    }

    [SuppressMessage("Usage", "CA1816:Dispose methods should call SuppressFinalize")]
    public void Dispose()
    {
        _pluginHost.StopAsync().GetAwaiter().GetResult();
        _pluginHost.Dispose();
    }
}

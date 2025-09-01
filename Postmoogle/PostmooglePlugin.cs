// Copyright (c) 2025 MeiHasCrashed
// License: AGPL-3.0-or-later (see /License.md)

using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Postmoogle;

public class PostmooglePlugin(ILogger<PostmooglePlugin> logger) : IHostedService
{
    public Task StartAsync(CancellationToken cancellationToken)
    {
        logger.LogInformation("Starting Postmoogle plugin");
        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        logger.LogInformation("Stopping Postmoogle plugin");
        return Task.CompletedTask;
    }
}

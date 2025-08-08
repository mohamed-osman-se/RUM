using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using RUM.Data;

namespace RUM.Services
{
    public class MessageCleanupService : BackgroundService
    {
        private readonly ILogger<MessageCleanupService> _logger;
        private readonly IServiceProvider _services;

        public MessageCleanupService(ILogger<MessageCleanupService> logger, IServiceProvider services)
        {
            _logger = logger;
            _services = services;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var timer = new PeriodicTimer(TimeSpan.FromMinutes(5));
            _logger.LogInformation("Message Cleanup Service started at {Time}", DateTime.UtcNow);

            while (await timer.WaitForNextTickAsync(stoppingToken))
            {
                try
                {
                    using (var scope = _services.CreateScope())
                    {
                        var _context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                        var threshold = DateTime.UtcNow.AddHours(-24);

                        var oldMessages = await _context.messages
                            .Where(msg => msg.CreatedAt <= threshold)
                            .ToListAsync(stoppingToken);

                        if (oldMessages.Any())
                        {
                            int count = oldMessages.Count;
                            _context.messages.RemoveRange(oldMessages);
                            await _context.SaveChangesAsync(stoppingToken);

                            _logger.LogInformation("{Count} old messages deleted successfully at {Time}", count, DateTime.UtcNow);
                        }
                        else
                        {
                            _logger.LogInformation("No old messages to delete at {Time}", DateTime.UtcNow);
                        }
                    }
                }
                catch (OperationCanceledException)
                {
                    _logger.LogInformation("Message Cleanup Service is stopping gracefully.");
                    break;
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "An error occurred while cleaning up messages at {Time}", DateTime.UtcNow);
                }
            }
        }
    }
}

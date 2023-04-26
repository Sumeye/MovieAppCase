using MovieAppCase.API.RabbitMQ;
using MovieAppCase.Core.Services;
using MovieAppCase.Api.Crawler;

namespace MovieCrawler
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly int _delay;
        public IServiceProvider Services { get; }
        public Worker(
            ILogger<Worker> logger,
            IServiceProvider services
            )
        {
            Services = services;
            _logger = logger;
            _delay = 60 * 60 * 1000; // Once an hour

        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            try
            {

                while (!stoppingToken.IsCancellationRequested)
                {
                    _logger.LogInformation("Start  ExecuteMovieCrawler()  at: {time}", DateTimeOffset.Now);


                    using (var scope = Services.CreateScope())
                    {
                        var scopedProcessingService =
                            scope.ServiceProvider
                                .GetRequiredService<IMovieCrawlerService>();

                        await scopedProcessingService.ExecuteMovieCrawler();
                    }
                    _logger.LogInformation("Finish  ExecuteMovieCrawler()  at: {time}", DateTimeOffset.Now);

                    await Task.Delay(_delay, stoppingToken);
                }
            }
            catch (TaskCanceledException e)
            {
                _logger.LogError(e, "TaskCanceledException");
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Exception");
            }
        }

        public override async Task StopAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation(
                "Consume Scoped Service Hosted Service is stopping.");

            await base.StopAsync(stoppingToken);
        }
    }
}
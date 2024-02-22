using Core.StorageSevice.PeakVentures.Services;

namespace Worker.StorageService.PeakVentures
{
    public class WorkerApp : BackgroundService
    {
        private readonly ILogger<WorkerApp> _logger;
        private readonly UserDataConsumerService _userDataConsumerService;

        public WorkerApp(ILogger<WorkerApp> logger, UserDataConsumerService userDataConsumerService
            )
        {
            _logger = logger;
            _userDataConsumerService = userDataConsumerService;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);

                _userDataConsumerService.StartConsumerLoop();

                await Task.Delay(1000, stoppingToken);
            }
        }
    }
}
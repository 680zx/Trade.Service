using Contracts;
using Entities;
using Trade.Service.Algorithms;

namespace Trade.Service.Workers
{
    // MaShmPd
    // Ma   - moving average
    // S    - shooting star
    // h    - hammer
    // m    - hanged man
    // Pd   - price density
    internal class MaShmPdWorker : BackgroundService
    {
        private readonly ILogger<MaShmPdWorker> _logger;
        private MaShmPdAlgorithm _maShmPdAlgorithm;
        private IAlgorithm<DataBarsWithDom> _maShmPdAlgorihm;
        private TimeSpan _delay;

        public MaShmPdWorker(ILogger<MaShmPdWorker> logger, IAlgorithm<DataBarsWithDom> maShmPdAlgorihm,
            TimeSpan delay)
        {
            _logger = logger;
            _maShmPdAlgorihm = maShmPdAlgorihm;
            _delay = delay;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                //_logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                await Task.Delay(_delay, stoppingToken);
            }
        }
    }
}

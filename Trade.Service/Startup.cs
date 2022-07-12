using QuantConnect.Lean.Engine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trade.Service
{
    public class Startup
    {
        private ILogger<Startup> _logger;

        private AlgorithmManager _algorithmManager;

        public Startup(ILogger<Startup> logger)
        {
            _logger = logger;
        }

        public void Run()
        {
            try
            {
                _algorithmManager = new AlgorithmManager(true);
                _algorithmManager.Run()
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
            }
        }
    }
}

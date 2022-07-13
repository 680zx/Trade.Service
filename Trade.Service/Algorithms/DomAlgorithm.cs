using QuantConnect;
using QuantConnect.Algorithm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trade.Service.Algorithms
{
    public class DomAlgorithm : QCAlgorithm
    {
        private Symbol? _btcUsdt = null;

        public DomAlgorithm()
        {           
        }

        public override void Initialize()
        {
            SetStartDate(2022, 6, 1);
            SetEndDate(2022, 7, 1);
            
            SetAccountCurrency("USDT");
            SetCash(100);

            SetBrokerageModel(QuantConnect.Brokerages.BrokerageName.Binance, QuantConnect.AccountType.Margin);
            UniverseSettings.Leverage = 10;
            UniverseSettings.Resolution = QuantConnect.Resolution.Minute;

            _btcUsdt = AddCrypto("BTCUSDT", Resolution.Minute, Market.Binance).Symbol;
        }
    }
}

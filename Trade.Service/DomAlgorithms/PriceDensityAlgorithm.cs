using Contracts;
using Entities;

namespace Trade.Service.DomAlgorithms
{
    internal class PriceDensityAlgorithm : IDomAlgorithm
    {
        public decimal DomAccuracy { get; set; }

        public MarketMovement GetValue(DepthOfMarket data)
        {
            var domAccuracyDegree = Math.Log10((double) DomAccuracy);
            var bestBuyRoundedCoinPrice = 0m;
            var bestSellRoundedCoinPrice = 0m;

            // round fractional part of number
            if (domAccuracyDegree < 0)
            {
                bestBuyRoundedCoinPrice = Math.Round(data.BestBuyPrice, (int) Math.Abs(domAccuracyDegree));
                bestSellRoundedCoinPrice = Math.Round(data.BestSellPrice, (int) Math.Abs(domAccuracyDegree));
            }

            // round integer path of number
            if (domAccuracyDegree >= 0)
            {
                var divisorPrecision = (decimal) Math.Pow(10, domAccuracyDegree);
                bestBuyRoundedCoinPrice = Math.Round(data.BestBuyPrice / divisorPrecision, 0) * divisorPrecision;
                bestSellRoundedCoinPrice = Math.Round(data.BestSellPrice / divisorPrecision, 0) * divisorPrecision;
            }

            if (bestBuyRoundedCoinPrice <= data.MidPrice)
                throw new ArgumentException("Wrong data -> Best buy price less or equal to Mid price");

            if (bestSellRoundedCoinPrice >= data.MidPrice)
                throw new ArgumentException("Wrong data -> Best sell price greater or equal to Mid price");

            var bestBuyToMidPriceDiff = Math.Abs(bestBuyRoundedCoinPrice - data.MidPrice);
            var bestSellToMidPriceDiff = Math.Abs(bestSellRoundedCoinPrice - data.MidPrice);

            var bestBuyPriceDensity = data.BestBuyVolume / bestBuyToMidPriceDiff;
            var bestSellPriceDensity = data.BestSellVolume / bestSellToMidPriceDiff;

            var priceDirectionCoef = bestBuyPriceDensity / bestSellPriceDensity;

            return priceDirectionCoef > 1 ? MarketMovement.Up : MarketMovement.Down;
        }
    }
}

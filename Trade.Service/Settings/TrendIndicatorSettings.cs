using Contracts;

namespace Trade.Service.Settings
{
    internal class TrendIndicatorSettings : IIndicatorSettings
    {
        public int WindowLength { get; set; }
    }
}

namespace Entities
{
    public class DataBar
    {
        public decimal HighPrice { get; set; }
        public decimal LowPrice { get; set; }
        public decimal OpenPrice { get; set; }
        public decimal ClosePrice { get; set; }
        public decimal UpperShadow { get; set; }
        public decimal LowerShadow { get; set; }
        public decimal RealBody { get => Math.Abs(ClosePrice - OpenPrice); }
        public decimal TotalLength { get => HighPrice - LowPrice; }

        public decimal EmaValue { get; set; }
    }
}
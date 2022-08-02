namespace Contracts
{
    public interface IIndicatorSettings : ISettings
    {
        public int WindowLength { get; set; }
    }
}

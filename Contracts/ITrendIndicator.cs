using Entities;

namespace Contracts
{
    public interface ITrendIndicator<T> : IIndicator<T, IList<DataBar>>
    {
        public int WindowLength { get; set; }
    }
}

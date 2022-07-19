using Entities;

namespace Contracts
{
    // Dom -> Depth of Market | стакан заявок
    public interface IDomIndicator : IIndicator<MarketMovement, DepthOfMarket>
    {
    }
}

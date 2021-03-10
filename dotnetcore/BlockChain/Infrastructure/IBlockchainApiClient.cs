using System.Collections.Generic;
using BlockChain.Domain.Model;

namespace BlockChain.Infrastructure
{
    public interface IBlockchainApiClient
    {
        ICollection<IList<Block>> FindChains(IEnumerable<Node> nodes);
    }
}
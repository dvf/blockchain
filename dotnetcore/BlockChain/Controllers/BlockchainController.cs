using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BlockChain.Domain.Model;
using BlockChain.Domain;
using BlockChain.ViewModels;
using BlockChain.Infrastructure;

namespace BlockChain.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BlockchainController : ControllerBase
    {
        private readonly Blockchain blockchain;
        private readonly IBlockchainApiClient apiClient;

        public BlockchainController(Blockchain blockchain, IBlockchainApiClient apiClient)
        {
            if (blockchain == null)
                throw new ArgumentNullException("Blockchain must not be null");
            if (apiClient == null)
                throw new ArgumentNullException("ApiClient must not be null");
            this.blockchain = blockchain;
            this.apiClient = apiClient;
        }

        // GET blockchain/chain
        [HttpGet("chain")]
        public dynamic Get()
        {
            return new
            {
                chain = blockchain.Chain,
                length = blockchain.Chain.Count
            };
        }

        // POST blockchain/transactions/new
        [HttpPost("transactions/new")]
        public dynamic CreateTransaction(CreateTransaction vm)
        {
            var index = blockchain.NewTransaction(
                vm.Sender, 
                vm.Recipient, 
                vm.Amount);

            return Ok(new
            {
                message = $"Transaction will be added to block {index}"
            });
        }

        // POST blockchain/mine
        [HttpPost("mine")]
        public dynamic Mine()
        {
            var block = blockchain.Mine();
            return Ok(new
            {
                message = "New block forged",
                index = block.Index,
                transactions = block.Transactions,
                proof = block.Proof.Value,
                previous_hash = block.PreviousHash.Value
            });
        }

        // POST blockchain/nodes/register
        [HttpPost("nodes/register")]
        public dynamic Register(RegisterAddress vm)
        {
            if (!Uri.TryCreate(vm.Address, UriKind.Absolute, out var uri))
                return BadRequest("Address is not well formed URI");

            blockchain.Register(uri);

            return Ok(new
            {
                message = "New node has been added",
                total_nodes = blockchain.Nodes
            });
        }

        // POST blockchain/nodes/resolve
        [HttpPost("nodes/resolve")]
        public dynamic Resolve()
        {

            var otherChains = apiClient.FindChains(blockchain.Nodes);
            var replaced = blockchain.ResolveConflicts(otherChains);
            if (replaced)
            {
                return new
                {
                    message = "Our chain was replaced",
                    new_chain = blockchain.Chain
                };
            }

            return new
            {
                message = "Our chain is authoritative",
                chain = blockchain.Chain
            };
        }
    }
}

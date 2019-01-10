using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlockChainDemo
{
    internal class ResolveStatus
    {
        public ResolveStatus()
        {
            ResolveResults = new List<NodeResolveResult>();
        }
        public bool Status { get; set; }
        public List<NodeResolveResult> ResolveResults { get; set; }

    }
}

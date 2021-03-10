using System;
using System.Collections.Generic;

namespace BlockChain.Infrastructure.DTO
{
    public class Blockchain
    {
        public long Length { get; set; }
        public List<Block> Chain { get; set; }
    }
}
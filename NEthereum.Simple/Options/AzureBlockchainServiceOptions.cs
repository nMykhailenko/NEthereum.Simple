using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NEthereum.Simple.Options
{
    public class AzureBlockchainServiceOptions
    {
        public string BlockchainRpcEndpoint { get; set; }
        public string AccountAddress { get; set; }
        public string AccountPassword { get; set; }
    }
}

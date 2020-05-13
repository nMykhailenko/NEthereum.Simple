using Nethereum.ABI.FunctionEncoding.Attributes;
using Nethereum.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NEthereum.Simple.BLL.Services.Ethereum.ContractsDeployment
{
    public class AdvDeployment : ContractDeploymentMessage
    {
        public AdvDeployment() : base(string.Empty)
        {

        }
        public AdvDeployment(string byteCode) : base(byteCode)
        {

        }

        [Parameter("string", "billId", 1)]
        public string BillId { get; set; }

        [Parameter("string", "productLotNumber", 2)]
        public string ProductLotNumber { get; set; }

        [Parameter("string", "nameOfClient", 3)]
        public string NameOfClient { get; set; }

        [Parameter("string", "quantity", 4)]
        public string Quantity { get; set; }
    }
}

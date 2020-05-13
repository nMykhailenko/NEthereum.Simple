using Nethereum.ABI.FunctionEncoding.Attributes;
using Nethereum.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace NEthereum.Simple.BLL.Services.Ethereum.AdvEntities.Queries
{
    [Function("ProductLotNumber", "string")]
    public class ProductLotNumberFunction : FunctionMessage
    {
    }
}

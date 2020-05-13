using Nethereum.ABI.FunctionEncoding.Attributes;
using Nethereum.Contracts;

namespace NEthereum.Simple.BLL.Services.Ethereum.AdvEntities.Queries
{
    [Function("State", "uint8")]
    public class StateFunction : FunctionMessage
    {
    }
}

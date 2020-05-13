using Nethereum.Contracts;
using NEthereum.Simple.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NEthereum.Simple.BLL.Services.Ethereum.Contracts
{
    public interface IEthereumService<TContractDeployment> where TContractDeployment : ContractDeploymentMessage, new()
    {
        Task<string> DeployAsync<TModelDTO>(TModelDTO modelDTO, SmartContract smartContract) where TModelDTO : class;
        Task<TOutput> QueryAsync<TFunction, TOutput>(string contractAddress) where TFunction : FunctionMessage, new();
        Task CommandAsync<TInput>(TInput body, int contractId, string functionName);
        Task CommandAsync<TInput>(TInput body, Entity contractEntity, string functionName);
    }
}

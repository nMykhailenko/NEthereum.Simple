using AutoMapper;
using Microsoft.Extensions.Options;
using Nethereum.ABI.Model;
using Nethereum.Contracts;
using Nethereum.Web3;
using Nethereum.Web3.Accounts;
using NEthereum.Simple.BLL.Extensions;
using NEthereum.Simple.BLL.Services.Ethereum.Contracts;
using NEthereum.Simple.DAL;
using NEthereum.Simple.DAL.Models;
using NEthereum.Simple.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace NEthereum.Simple.BLL.Services.Ethereum
{
    public class EthereumService<TContractDeployment> : IEthereumService<TContractDeployment>
        where TContractDeployment : ContractDeploymentMessage, new()
    {
        private readonly IMapper _mapper;
        private readonly ApplicationDbContext _context;
        private readonly AzureBlockchainServiceOptions _blockchainServiceOptions;
        public EthereumService(
            IMapper mapper,
            ApplicationDbContext context,
            IOptions<AzureBlockchainServiceOptions> blockchainServiceOptions)
        {
            _mapper = mapper;
            _context = context;
            _blockchainServiceOptions = blockchainServiceOptions.Value;
        }

        public async Task<string> DeployAsync<TModelDTO>(TModelDTO modelDTO, SmartContract smartContract) where TModelDTO : class
        {
            var account = new Account(_blockchainServiceOptions.AccountAddress);
            var web3 = new Web3(account, _blockchainServiceOptions.BlockchainRpcEndpoint);

            await web3.Personal.UnlockAccount
                .SendRequestAsync(_blockchainServiceOptions.AccountAddress, _blockchainServiceOptions.AccountPassword, 120);

            var contractDeployment = _mapper.Map<TContractDeployment>((modelDTO, smartContract.ByteCode));
            var deploymentHandler = web3.Eth.GetContractDeploymentHandler<TContractDeployment>();
            var deploymentReceipt = await deploymentHandler.SendRequestAndWaitForReceiptAsync(contractDeployment);

            return deploymentReceipt.ContractAddress;
        }

        public async Task<TOutput> QueryAsync<TFunction, TOutput>(string contractAddress) where TFunction : FunctionMessage, new()
        {
            var account = new Account(_blockchainServiceOptions.AccountAddress);
            var web3 = new Web3(account, _blockchainServiceOptions.BlockchainRpcEndpoint);

            await web3.Personal.UnlockAccount.SendRequestAsync(_blockchainServiceOptions.AccountAddress, _blockchainServiceOptions.AccountPassword, 120);

            var contractHandler = web3.Eth.GetContractHandler(contractAddress);
            var result = await contractHandler.QueryAsync<TFunction, TOutput>();

            return result;
        }

        public async Task CommandAsync<TInput>(TInput body, int contractId, string functionName)
        {
            var contractEntity = await _context.Entities.FindAsync(contractId);
            if (contractEntity == null)
                throw new ArgumentNullException($"Entity with ID {contractId} not found.");

            await CommandAsync(body, contractEntity, functionName);
        }

        public async Task CommandAsync<TInput>(TInput body, Entity contractEntity, string functionName)
        {
            var web3 = new Web3(_blockchainServiceOptions.BlockchainRpcEndpoint);
            var contract = web3.Eth.GetContract(contractEntity.Abi, contractEntity.ContractAddress);
            await web3.Personal.UnlockAccount.SendRequestAsync(_blockchainServiceOptions.AccountAddress, _blockchainServiceOptions.AccountPassword, 120);

            var functionABI = contract.ContractBuilder.ContractABI.Functions.FirstOrDefault(f => f.Name == functionName);
            if (functionABI == null)
                throw new ArgumentNullException($"{functionName} for contract not found.");

            var functionParameters = functionABI.InputParameters ?? new Parameter[] { };
            var bodyProperties = body.GetPropertiesInfo();

            if (!ValidateModelParameters(bodyProperties, functionParameters))
                throw new ArgumentNullException($"Parameters do not match.");

            var arguments = functionParameters.GetArguments(body);
            var function = contract.GetFunction(functionName);

            var estimated = await web3.TransactionManager.EstimateGasAsync(function.CreateCallInput(arguments));
            var transactionInput = function.CreateTransactionInput(_blockchainServiceOptions.AccountAddress, arguments);

            web3.TransactionManager.DefaultGas = estimated.Value;
            web3.TransactionManager.DefaultGasPrice = 0;

            var transactionRseceipt = await web3.TransactionManager.SendTransactionAndWaitForReceiptAsync(transactionInput, null);
        }

        private bool ValidateModelParameters(IEnumerable<PropertyInfo> properties, IEnumerable<Parameter> parameters)
        {
            foreach (var parameter in parameters)
            {
                var property = properties.FirstOrDefault(x => x.Name.ToLowerInvariant() == parameter.Name.ToLowerInvariant());
                if (property == null) return false;
            }

            return true;
        }
    }
}

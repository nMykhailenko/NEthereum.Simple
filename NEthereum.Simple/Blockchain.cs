﻿using Nethereum.ABI.FunctionEncoding;
using Nethereum.ABI.Model;
using Nethereum.Contracts;
using Nethereum.Hex.HexTypes;
using Nethereum.RPC.Eth.DTOs;
using Nethereum.RPC.Eth.Transactions;
using Nethereum.Web3.Accounts.Managed;
using NEthereum.Simple.Models;
using NEthereum.Simple.Models.Base;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Threading.Tasks;

namespace NEthereum.Simple
{
    public class Blockchain
    {
        public async Task<BlockModel> GetAsync(int id)
        {
            BlockModelBase blockModel = new BlockModelBase { id = id };
            var modelJson = JsonConvert.SerializeObject(blockModel);

            var model = await CreateResponse(modelJson, "getMaterial");
            BlockModel result = new BlockModel()
            {
                idBioRefMaterial = model[0].Result.ToString(),
                name = model[1].Result.ToString(),
                unit = Convert.ToInt32(model[2].Result.ToString()),
                isBio = Convert.ToBoolean(model[3].Result),
                dateCreated = Convert.ToInt32(model[4].Result.ToString()),
            };

            return result;
        }

        public async Task<BlockModel> CreateAsync(BlockModel blockModel)
        {
            var stringJson = JsonConvert.SerializeObject(blockModel);
            var model = await CreateResponse(stringJson, "addMaterial", true);

            BlockModel result = new BlockModel()
            {
                idBioRefMaterial = model[0].Result.ToString(),
                name = model[1].Result.ToString(),
                unit = Convert.ToInt32(model[2].Result.ToString()),
                isBio = Convert.ToBoolean(model[3].Result),
                dateCreated = Convert.ToInt32(model[4].Result.ToString()),
                id = Convert.ToInt32(model[5].Result.ToString())
            };

            return result;
        }

        public async Task<List<ParameterOutput>> CreateResponse(string jsonBody, string functionName, bool isTransaction = false)
        {
            // Get request body

            if (string.IsNullOrWhiteSpace(jsonBody)) jsonBody = @"{}";
            var body = JsonConvert.DeserializeObject<JObject>(jsonBody);

            // Get parameters
            var inputParameters = body.Values();
            var arguments = new object[inputParameters.Count()];
            var i = 0;
            foreach (var p in inputParameters.Values())
            {
                arguments[i++] = p.Value<string>();
            }

            var web3 = new Nethereum.Web3.Web3(SmartContract.BlockchainRpcEndpoint);
            var contract = web3.Eth.GetContract(SmartContract.Abi, SmartContract.ContractAddress);

            var users = await web3.Personal.UnlockAccount.SendRequestAsync("0x4de8efa641546c0f2176a2b66f04dd17451b2542", "Qwerty!1Qwerty!1", 120);

            var functionABI = contract.ContractBuilder.ContractABI.Functions
                .FirstOrDefault(f => f.Name == functionName);

            if (functionABI == null)
                return null; //"Function not found!"

            var functionParameters = functionABI.InputParameters;
            if (functionParameters?.Count() != inputParameters.Count())
                return null; //"Parameters do not match!"

            Function function = contract.GetFunction(functionName);

            Type returnType = GetFunctionReturnType(functionABI);
            //var transactionInput = function.CreateTransactionInput("0x4de8efa641546c0f2176a2b66f04dd17451b2542", arguments);
            //var estimated = web3.TransactionManager.EstimateGasAsync(function.CreateCallInput(arguments));
            //transactionInput.GasPrice = gasPrice;
            IEthCall ethCall = contract.Eth.Transactions.Call;
            var result = await ethCall.SendRequestAsync(function.CreateCallInput(arguments), contract.Eth.DefaultBlock);

            FunctionBase functionBase = function;
            PropertyInfo builderBaseProperty = functionBase.GetType().GetProperty("FunctionBuilderBase", BindingFlags.Instance | BindingFlags.NonPublic);
            if (builderBaseProperty != null)
            {
                FunctionBuilderBase builderBase = (FunctionBuilderBase)builderBaseProperty.GetValue(functionBase);
                PropertyInfo funcCallDecoderProperty = builderBase.GetType()
                    .GetProperty("FunctionCallDecoder", BindingFlags.Instance | BindingFlags.NonPublic);
                if (funcCallDecoderProperty != null)
                {
                    ParameterDecoder decoder = (ParameterDecoder)funcCallDecoderProperty.GetValue(builderBase);
                    var results = decoder.DecodeDefaultData(result, functionABI.OutputParameters);

                    if (results.Count == 1)
                    {
                        return null;
                    }

                    return results;
                }
            }

            return null;
        }

        private Type GetFunctionReturnType(FunctionABI functionABI)
        {
            if (functionABI == null)
                return typeof(object);

            Parameter[] parameters = functionABI.OutputParameters;

            if (parameters == null || parameters.Length == 0)
                return typeof(object);

            if (parameters.Length == 1)
                return parameters[0].ABIType.GetDefaultDecodingType();

            Type taskType = Type.GetType("System.Tuple`" + parameters.Length);
            List<Type> typeArgs = new List<Type>();

            foreach (var param in parameters)
            {
                typeArgs.Add(param.ABIType.GetDefaultDecodingType());
            }

            if (taskType != null)
            {
                Type genericType = taskType.MakeGenericType(typeArgs.ToArray());

                return genericType;
            }

            return null;
        }
    }
}
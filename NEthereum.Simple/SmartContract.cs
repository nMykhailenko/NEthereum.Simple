using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NEthereum.Simple
{
    public class SmartContract
    {
        public static string Abi => "[{\"anonymous\":false,\"inputs\":[{\"indexed\":false,\"name\":\"id\",\"type\":\"uint256\"},{\"indexed\":false,\"name\":\"idBioRefMaterial\",\"type\":\"string\"}],\"name\":\"MaterialAdded\",\"type\":\"event\",\"signature\":\"0xf5b4def5973d014960ead91323c90ae13e6dd9fc1b7eed2de3a4dccf2422ecbe\"},{\"constant\":false,\"inputs\":[{\"name\":\"id\",\"type\":\"uint256\"},{\"name\":\"idBioRefMaterial\",\"type\":\"string\"},{\"name\":\"name\",\"type\":\"string\"},{\"name\":\"unit\",\"type\":\"uint16\"},{\"name\":\"isBio\",\"type\":\"uint8\"},{\"name\":\"dateCreated\",\"type\":\"uint256\"}],\"name\":\"addMaterial\",\"outputs\":[{\"name\":\"materialId\",\"type\":\"uint256\"},{\"name\":\"idBioRef\",\"type\":\"string\"}],\"payable\":false,\"stateMutability\":\"nonpayable\",\"type\":\"function\",\"signature\":\"0xd262722e\"},{\"constant\":true,\"inputs\":[{\"name\":\"id\",\"type\":\"uint256\"}],\"name\":\"getMaterial\",\"outputs\":[{\"name\":\"idBioRefMaterial\",\"type\":\"string\"},{\"name\":\"name\",\"type\":\"string\"},{\"name\":\"unit\",\"type\":\"uint16\"},{\"name\":\"isBio\",\"type\":\"bool\"},{\"name\":\"dateCreated\",\"type\":\"uint256\"}],\"payable\":false,\"stateMutability\":\"view\",\"type\":\"function\",\"signature\":\"0xda6d8909\"}]";
        public static string ContractAddress = "0xe121e9DddABe663D17383D9231323b866e23c43b";
        public static string BlockchainRpcEndpoint = "https://sequenceadmin.blockchain.azure.com:3200/Q9dl1YutZfNSxpSY3Cmwun9O";
    }
}

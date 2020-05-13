using NEthereum.Simple.DAL.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NEthereum.Simple.DAL.Models
{
    public class SmartContract
    {
        public int Id { get; set; }
        public string Abi { get; set; }
        public string ByteCode { get; set; }
        public ContractTypeEnum Type { get; set; }
        public string ApiVersion { get; set; } = "v1";
    }
}

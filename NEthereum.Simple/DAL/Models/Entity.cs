using NEthereum.Simple.DAL.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NEthereum.Simple.DAL.Models
{
    public class Entity
    {
        public int Id { get; set; }
        public ContractTypeEnum Type { get; set; }
        public bool IsOutOfStock { get; set; } = false;
        public string ContractAddress { get; set; }
        public string Abi { get; set; }
    }
}

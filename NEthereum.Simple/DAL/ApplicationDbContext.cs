using Microsoft.EntityFrameworkCore;
using NEthereum.Simple.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NEthereum.Simple.DAL
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public virtual DbSet<Entity> Entities { get; set; }
        public virtual DbSet<SmartContract> SmartContracts { get; set; }
    }
}

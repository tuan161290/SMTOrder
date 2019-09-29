using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WiSolSMTRepo.Model;

namespace WiSolSMTRepo
{
    public class SMTDbContext : DbContext
    {
        public SMTDbContext(DbContextOptions<SMTDbContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
       
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>().HasData(new Product { ProductID = 1, Name = "L7E0" }, new Product() { ProductID = 2, Name = "L7E1" });
            modelBuilder.Entity<LineInfo>().HasData(
                new LineInfo { LineInfoID = 1, Name = "SMT-A" },
                new LineInfo() { LineInfoID = 2, Name = "SMT-B" },
                new LineInfo { LineInfoID = 3, Name = "SMT-C" },
                new LineInfo() { LineInfoID = 4, Name = "SMT-D" },
                new LineInfo { LineInfoID = 5, Name = "SMT-E" },
                new LineInfo() { LineInfoID = 6, Name = "SMT-F" },
                new LineInfo { LineInfoID = 7, Name = "SMT-G" },
                new LineInfo() { LineInfoID = 8, Name = "SMT-H" },
                new LineInfo { LineInfoID = 9, Name = "SMT-I" });
            base.OnModelCreating(modelBuilder);
        }
        public DbSet<LineInfo> LineInfos { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<PlanInfo> Plans { get; set; }
        public DbSet<Product> Products { get; set; }
    }
}

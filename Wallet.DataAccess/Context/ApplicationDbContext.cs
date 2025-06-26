using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wallet.Core.Entitites.Models;

namespace Wallet.DataAccess.Context
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser>
    {
        public ApplicationDbContext()
        {

        }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
        
        public DbSet<User> Users { get; set; }
        public DbSet<UserInventory> UserInventory { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<TransactionDetails> TransactionDetails { get; set; }
        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasKey(u => u.UserId);
            modelBuilder.Entity<Product>().HasKey(u => u.ProductId);
            modelBuilder.Entity<Transaction>().HasKey(u => u.TransactionId);
            modelBuilder.Entity<TransactionDetails>().HasKey(u => u.TransactionDetailsId);
            modelBuilder.Entity<UserInventory>().HasKey(u => u.InventoryId);

            modelBuilder.Entity<UserInventory>()
                .HasOne(u => u.User)
                .WithMany(u => u.Inventories)
                .HasForeignKey(u => u.UserId);

            modelBuilder.Entity<Transaction>()
                .HasOne(u => u.User)
                .WithMany(u => u.Transactions)
                .HasForeignKey(u => u.UserId);

            modelBuilder.Entity<TransactionDetails>()
                .HasOne(u => u.Transaction)
                .WithMany(u => u.TransactionDetails)
                .HasForeignKey(u => u.TransactionId);

            modelBuilder.Entity<TransactionDetails>()
                .HasOne(u => u.Product)
                .WithMany(u => u.TransactionDetails)
                .HasForeignKey(u => u.ProductId);

            modelBuilder.Entity<UserInventory>()
                .HasOne(u => u.Product)
                .WithMany(u => u.UserInventories)
                .HasForeignKey(u => u.ProductId);

            modelBuilder.Entity<Product>()
                .Property(u => u.Price)
                .HasPrecision(18, 2);

            modelBuilder.Entity<Transaction>()
                .Property(u => u.TotalPrice)
                .HasPrecision(18, 2);

            modelBuilder.Entity<TransactionDetails>()
                .Property(u => u.UnitPrice)
                .HasPrecision(18, 2);

            modelBuilder.Entity<TransactionDetails>()
                .Property(u => u.LineTotal)
                .HasPrecision(18, 2);

            modelBuilder.Entity<User>()
                .Property(u => u.Balance)
                .HasPrecision(18, 2);
        }
    }
}

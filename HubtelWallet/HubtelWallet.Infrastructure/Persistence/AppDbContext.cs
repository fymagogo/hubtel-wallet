using HubtelWallet.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace HubtelWallet.Infrastructure.Persistence
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Wallet> Wallets { get; set; }
        public DbSet<MomoWallet> MomoWallets { get; set; }
        public DbSet<VisaWallet> VisaWallets { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Wallet>()
                .HasDiscriminator(b => b.WalletType)
                .HasValue<Wallet>(WalletType.unknown)
                .HasValue<MomoWallet>(WalletType.momo)
                .HasValue<VisaWallet>(WalletType.card);

            modelBuilder.Entity<Wallet>(entity => {
                entity.HasIndex(x => x.CustomerId);
                entity.HasIndex(x => x.Owner);
            });

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.HasIndex(x => x.PhoneNumber).IsUnique();
            });

            modelBuilder.Entity<MomoWallet>(entity =>
            {
                entity.HasIndex(x => x.AccountNumber).IsUnique();
            });

            modelBuilder.Entity<VisaWallet>(entity =>
            {
                entity.HasIndex(x => x.MaskedVisaNumber).IsUnique();
            });



            modelBuilder.Entity<Wallet>()
                .Property(x => x.AccountNumber)
                .HasMaxLength(20);
        }

        protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
        {
            configurationBuilder.Properties<Enum>().HaveConversion<string>();
        }
    }
}

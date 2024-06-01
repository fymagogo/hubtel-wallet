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

            modelBuilder.Entity<VisaWallet>(entity =>
            {
                entity.HasIndex(x => x.MaskedVisaNumber).IsUnique();
            });

            modelBuilder.Entity<Wallet>()
                .Property(x => x.AccountNumber)
                .HasMaxLength(20);
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            UpdateTimestamps();
            return await base.SaveChangesAsync(cancellationToken);
        }

        private void UpdateTimestamps()
        {
            var entities = ChangeTracker.Entries()
                .Where(e => e.State == EntityState.Added || e.State == EntityState.Modified)
                .Select(e => e.Entity)
                .OfType<BaseEntity>(); // Assuming YourEntity is the entity type with the UpdatedAt property

            foreach (var entity in entities)
            {
                entity.UpdatedAt = DateTime.UtcNow;
            }
        }


        protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
        {
            configurationBuilder.Properties<Enum>().HaveConversion<string>();
        }
    }
}

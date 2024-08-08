using Microsoft.EntityFrameworkCore;
using SuperMarketWarehouse.Core.Entities;

namespace SuperMarketWarehouse.Infrastructure.Data
{
    public class SuperMarketWarehouseContext : DbContext
    {
        public SuperMarketWarehouseContext(DbContextOptions<SuperMarketWarehouseContext> options) 
            : base(options)
        {
        }

        public DbSet<Gudang> Gudangs { get; set; }
        public DbSet<Barang> Barangs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Gudang>(entity =>
            {
                entity.ToTable("Gudangs");
                entity.HasKey(g => g.Id);
                entity.Property(g => g.NamaGudang)
                    .IsRequired()
                    .HasMaxLength(100);
                entity.Property(g => g.Location)
                    .HasMaxLength(200);

                entity.HasMany(g => g.Barangs)
                    .WithOne(b => b.Gudang)
                    .HasForeignKey(b => b.GudangId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Barang>(entity =>
            {
                entity.ToTable("Barangs");
                entity.HasKey(b => b.Id);
                entity.Property(b => b.NamaBarang)
                    .IsRequired()
                    .HasMaxLength(100);
                entity.Property(b => b.Harga)
                    .IsRequired()
                    .HasColumnType("decimal(18,2)");
                entity.Property(b => b.Jumlah)
                    .IsRequired();
                entity.Property(b => b.ExpiryDate)
                    .IsRequired();
            });
        }
    }
}

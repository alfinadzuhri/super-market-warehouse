using Microsoft.EntityFrameworkCore;
using SuperMarketWarehouse.Core.Entities;
using SuperMarketWarehouse.Core.Interfaces;
using SuperMarketWarehouse.Infrastructure.Data;

namespace SuperMarketWarehouse.Infrastructure.Repositories
{
    public class BarangRepository : IBarangRepository
    {
        private readonly SuperMarketWarehouseContext _context;

        public BarangRepository(SuperMarketWarehouseContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Barang>> GetAllAsync()
        {
            return await _context.Barangs.ToListAsync();
        }

        public async Task<Barang> GetByIdAsync(int id)
        {
            return await _context.Barangs.FindAsync(id);
        }

        public async Task AddAsync(Barang barang)
        {
            await _context.Barangs.AddAsync(barang);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Barang barang)
        {
            _context.Barangs.Update(barang);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var barang = await _context.Barangs.FindAsync(id);
            if (barang != null)
            {
                _context.Barangs.Remove(barang);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Barang>> FilterAsync(string gudangName, DateTime? expiryDate)
        {
            var query = _context.Barangs.AsQueryable();

            if (!string.IsNullOrEmpty(gudangName))
            {
                query = query.Include(b => b.Gudang).Where(b => b.Gudang.NamaGudang == gudangName);
            }

            if (expiryDate.HasValue)
            {
                query = query.Where(b => b.ExpiryDate <= expiryDate.Value);
            }

            return await query.ToListAsync();
        }
    }
}

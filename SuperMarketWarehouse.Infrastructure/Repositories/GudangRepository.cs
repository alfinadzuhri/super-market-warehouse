using Microsoft.EntityFrameworkCore;
using SuperMarketWarehouse.Core.Entities;
using SuperMarketWarehouse.Core.Interfaces;
using SuperMarketWarehouse.Infrastructure.Data;

namespace SuperMarketWarehouse.Infrastructure.Repositories
{
    public class GudangRepository : IGudangRepository
    {
        private readonly SuperMarketWarehouseContext _context;

        public GudangRepository(SuperMarketWarehouseContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Gudang>> GetAllAsync()
        {
            return await _context.Gudangs.ToListAsync();
        }

        public async Task<Gudang> GetByIdAsync(int id)
        {
            return await _context.Gudangs.FindAsync(id);
        }

        public async Task AddAsync(Gudang gudang)
        {
            await _context.Gudangs.AddAsync(gudang);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Gudang gudang)
        {
            _context.Gudangs.Update(gudang);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var gudang = await _context.Gudangs.FindAsync(id);
            if (gudang != null)
            {
                _context.Gudangs.Remove(gudang);
                await _context.SaveChangesAsync();
            }
        }
    }
}

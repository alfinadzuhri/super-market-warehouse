using SuperMarketWarehouse.Core.Entities;

namespace SuperMarketWarehouse.Core.Interfaces
{
    public interface IGudangRepository
    {
        Task<IEnumerable<Gudang>> GetAllAsync();
        Task<Gudang> GetByIdAsync(int id);
        Task AddAsync(Gudang gudang);
        Task UpdateAsync(Gudang gudang);
        Task DeleteAsync(int id);
    }
}

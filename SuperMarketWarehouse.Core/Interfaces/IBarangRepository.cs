using SuperMarketWarehouse.Core.Entities;

namespace SuperMarketWarehouse.Core.Interfaces
{
    public interface IBarangRepository
    {
        Task<IEnumerable<Barang>> GetAllAsync();
        Task<Barang> GetByIdAsync(int id);
        Task AddAsync(Barang barang);
        Task UpdateAsync(Barang barang);
        Task DeleteAsync(int id);
        Task<IEnumerable<Barang>> FilterAsync(string gudangName, DateTime? expiryDate);
    }
}

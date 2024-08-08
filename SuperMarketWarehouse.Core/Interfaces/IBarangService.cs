using SuperMarketWarehouse.Core.Entities;

namespace SuperMarketWarehouse.Core.Interfaces
{
    public interface IBarangService
    {
        Task<IEnumerable<Barang>> GetAllBarangsAsync();
        Task<Barang> GetBarangByIdAsync(int id);
        Task CreateBarangAsync(Barang barang);
        Task<bool> UpdateBarangAsync(Barang barang);
        Task<bool> DeleteBarangAsync(int id);
        Task<IEnumerable<Barang>> FilterBarangsAsync(string gudangName, DateTime? expiryDate);
    }
}

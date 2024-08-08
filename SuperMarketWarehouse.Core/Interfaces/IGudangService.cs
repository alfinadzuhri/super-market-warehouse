using SuperMarketWarehouse.Core.Entities;

namespace SuperMarketWarehouse.Core.Interfaces
{
    public interface IGudangService
    {
        Task<IEnumerable<Gudang>> GetAllGudangsAsync();
        Task<Gudang> GetGudangByIdAsync(int id);
        Task CreateGudangAsync(Gudang gudang);
        Task<bool> UpdateGudangAsync(Gudang gudang);
        Task<bool> DeleteGudangAsync(int id);
    }
}

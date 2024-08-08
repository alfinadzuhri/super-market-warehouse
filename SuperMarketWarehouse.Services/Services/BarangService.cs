using SuperMarketWarehouse.Core.Entities;
using SuperMarketWarehouse.Core.Interfaces;

namespace SuperMarketWarehouse.Services
{
    public class BarangService : IBarangService
    {
        private readonly IBarangRepository _barangRepository;

        public BarangService(IBarangRepository barangRepository)
        {
            _barangRepository = barangRepository;
        }

        public async Task<IEnumerable<Barang>> GetAllBarangsAsync()
        {
            return await _barangRepository.GetAllAsync();
        }

        public async Task<Barang> GetBarangByIdAsync(int id)
        {
            return await _barangRepository.GetByIdAsync(id);
        }

        public async Task CreateBarangAsync(Barang barang)
        {
            await _barangRepository.AddAsync(barang);
        }

        public async Task<bool> UpdateBarangAsync(Barang barang)
        {
            var existingBarang = await _barangRepository.GetByIdAsync(barang.Id);
            if (existingBarang == null)
            {
                return false;
            }

            await _barangRepository.UpdateAsync(barang);
            return true;
        }

        public async Task<bool> DeleteBarangAsync(int id)
        {
            var barang = await _barangRepository.GetByIdAsync(id);
            if (barang == null)
            {
                return false;
            }

            await _barangRepository.DeleteAsync(id);
            return true;
        }

        public async Task<IEnumerable<Barang>> FilterBarangsAsync(string gudangName, DateTime? expiryDate)
        {
            return await _barangRepository.FilterAsync(gudangName, expiryDate);
        }
    }
}

using SuperMarketWarehouse.Core.Entities;
using SuperMarketWarehouse.Core.Interfaces;

namespace SuperMarketWarehouse.Services
{
    public class GudangService : IGudangService
    {
        private readonly IGudangRepository _gudangRepository;

        public GudangService(IGudangRepository gudangRepository)
        {
            _gudangRepository = gudangRepository;
        }

        public async Task<IEnumerable<Gudang>> GetAllGudangsAsync()
        {
            return await _gudangRepository.GetAllAsync();
        }

        public async Task<Gudang> GetGudangByIdAsync(int id)
        {
            return await _gudangRepository.GetByIdAsync(id);
        }

        public async Task CreateGudangAsync(Gudang gudang)
        {
            await _gudangRepository.AddAsync(gudang);
        }

        public async Task<bool> UpdateGudangAsync(Gudang gudang)
        {
            var existingGudang = await _gudangRepository.GetByIdAsync(gudang.Id);
            if (existingGudang == null)
            {
                return false;
            }

            await _gudangRepository.UpdateAsync(gudang);
            return true;
        }

        public async Task<bool> DeleteGudangAsync(int id)
        {
            var gudang = await _gudangRepository.GetByIdAsync(id);
            if (gudang == null)
            {
                return false;
            }

            await _gudangRepository.DeleteAsync(id);
            return true;
        }
    }
}

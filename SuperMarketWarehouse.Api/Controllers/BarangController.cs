using Microsoft.AspNetCore.Mvc;
using SuperMarketWarehouse.Core.Entities;
using SuperMarketWarehouse.Core.Interfaces;

namespace SuperMarketWarehouse.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BarangController : ControllerBase
    {
        private readonly IBarangService _barangService;

        public BarangController(IBarangService barangService)
        {
            _barangService = barangService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var barangs = await _barangService.GetAllBarangsAsync();
            return Ok(barangs);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var barang = await _barangService.GetBarangByIdAsync(id);
            if (barang == null)
            {
                return NotFound();
            }
            return Ok(barang);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Barang barang)
        {
            if (barang == null)
            {
                return BadRequest();
            }

            await _barangService.CreateBarangAsync(barang);
            return CreatedAtAction(nameof(GetById), new { id = barang.Id }, barang);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Barang barang)
        {
            if (barang == null || id != barang.Id)
            {
                return BadRequest();
            }

            var updated = await _barangService.UpdateBarangAsync(barang);
            if (!updated)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _barangService.DeleteBarangAsync(id);
            if (!deleted)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpGet("filter")]
        public async Task<IActionResult> Filter([FromQuery] string gudangName, [FromQuery] DateTime? expiryDate)
        {
            var barangs = await _barangService.FilterBarangsAsync(gudangName, expiryDate);
            return Ok(barangs);
        }
    }
}

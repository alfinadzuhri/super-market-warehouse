using Microsoft.AspNetCore.Mvc;
using SuperMarketWarehouse.Core.Entities;
using SuperMarketWarehouse.Core.Interfaces;

namespace SuperMarketWarehouse.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GudangController : ControllerBase
    {
        private readonly IGudangService _gudangService;

        public GudangController(IGudangService gudangService)
        {
            _gudangService = gudangService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var gudangs = await _gudangService.GetAllGudangsAsync();
            return Ok(gudangs);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var gudang = await _gudangService.GetGudangByIdAsync(id);
            if (gudang == null)
            {
                return NotFound();
            }
            return Ok(gudang);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Gudang gudang)
        {
            if (gudang == null)
            {
                return BadRequest();
            }

            await _gudangService.CreateGudangAsync(gudang);
            return CreatedAtAction(nameof(GetById), new { id = gudang.Id }, gudang);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Gudang gudang)
        {
            if (gudang == null || id != gudang.Id)
            {
                return BadRequest();
            }

            var updated = await _gudangService.UpdateGudangAsync(gudang);
            if (!updated)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _gudangService.DeleteGudangAsync(id);
            if (!deleted)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}

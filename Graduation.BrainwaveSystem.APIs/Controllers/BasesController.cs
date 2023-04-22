using Graduation.BrainwaveSystem.Models.DTOs;
using Graduation.BrainwaveSystem.Models.Entities;
using Graduation.BrainwaveSystem.Services.Base;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Graduation.BrainwaveSystem.APIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BasesController<T> : ControllerBase
    {
        private readonly IBaseService<T> _service;

        public BasesController(IBaseService<T> service)
        {
            _service = service;
        }

        // GET: api/Devices
        [HttpGet]
        public async Task<ActionResult<IEnumerable<T>>> Get()
        {
            var results = await _service.GetAll();
            return Ok(results);
        }

        // GET: api/Devices/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Device>> Get(Guid id)
        {
            var result = await _service.GetById(id);
            return Ok(result);
        }

        // DELETE: api/Devices/5/delete-forever
        [HttpDelete("{id}/delete-forever")]
        public async Task<IActionResult> DeleteForever(Guid id)
        {
            var result = await _service.DeleteForever(id);
            if (result == 0)
                return Problem("Delete failed!");

            return Ok("Deleted forever " + result + (result > 1 ? " records." : " record.") + " in database.");
        }
    }
}

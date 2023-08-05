using Graduation.BrainwaveSystem.Models.DTOs;
using Graduation.BrainwaveSystem.Models.Entities;
using Graduation.BrainwaveSystem.Services.BaseServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Graduation.BrainwaveSystem.APIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BasesController<T, TRequest> : ControllerBase
    {
        private readonly IBaseService<T, TRequest> _service;

        public BasesController(IBaseService<T, TRequest> service)
        {
            _service = service;
        }

        // GET: api/Devices
        [HttpGet]
        [Authorize]
        public async Task<ActionResult<IEnumerable<T>>> Get()
        {
            var results = await _service.GetAll();
            return Ok(results);
        }

        // GET: api/Devices/5
        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<Device>> Get(Guid id)
        {
            var result = await _service.GetById(id);
            return Ok(result);
        }

        // POST: api/Devices
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPost]
        //[Authorize]
        //public async Task<ActionResult<Device>> Post(TRequest request)
        //{
        //    var result = await _service.Create(request);
        //    if (result == Guid.Empty)
        //        return Problem("Insert failed! Please contact KhaiND to check problem.");

        //    return CreatedAtAction("Get", new { id = result }, result);
        //}

        // PUT: api/Devices/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> Put(Guid id, TRequest request)
        {
            var result = await _service.Update(id, request);
            if (result == 0)
                return Problem("Update failed! Please contact KhaiND to check problem.");

            return Ok(new
            {
                Message = "Updated " + result + (result > 1 ? " records." : " record."),
                UpdatedRecord = await _service.GetById(id)
            });
        }

        // DELETE: api/Devices/5
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _service.Delete(id);
            if (result == 0)
                return Problem("Update failed! Please contact KhaiND to check problem.");

            return Ok("Deleted " + result + (result > 1 ? " records." : " record.") + " in system.");
        }

        // DELETE: api/Devices/5/delete-forever
        [HttpDelete("{id}/delete-forever")]
        [Authorize]
        public async Task<IActionResult> DeleteForever(Guid id)
        {
            var result = await _service.DeleteForever(id);
            if (result == 0)
                return Problem("Delete failed!");

            return Ok("Deleted forever " + result + (result > 1 ? " records." : " record.") + " in database.");
        }
    }
}

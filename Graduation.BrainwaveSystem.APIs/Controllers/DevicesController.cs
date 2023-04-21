using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Graduation.BrainwaveSystem.Models;
using Graduation.BrainwaveSystem.Models.Entities;
using Graduation.BrainwaveSystem.Services.Device;
using Graduation.BrainwaveSystem.Models.DTOs;

namespace Graduation.BrainwaveSystem.APIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DevicesController : ControllerBase
    {
        private readonly IDeviceService _service;

        public DevicesController(IDeviceService service)
        {
            _service = service;
        }

        // GET: api/Devices
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Device>>> GetDevice()
        {
            var results = _service.GetAll();
            return results != null ? Ok(results) : NotFound();
        }

        // GET: api/Devices/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Device>> GetDevice(Guid id)
        {
            var result = _service.GetById(id);
            return result != null ? Ok(result) : NotFound();
        }

        // POST: api/Devices
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Device>> PostDevice(DeviceRequest request)
        {
            var result = _service.Create(request);
            if (result == -2)
                return Problem("Entity set 'DataContext.Device'  is null.");
            if (result == 0)
                return Problem("Insert failed!");

            //return CreatedAtAction("GetDevice", new { id = result.Id }, result);
            return Created("Done", result);
        }

        // PUT: api/Devices/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDevice(Guid id, DeviceRequest request)
        {
            var result = _service.Update(id, request);
            if(result == -1)
                return NotFound();
            if (result == 0)
                return Problem("Update failed!");

            return Ok("Updated " + result + (result > 1 ? " records." : " record."));

        }

        // DELETE: api/Devices/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDevice(Guid id)
        {
            var result = _service.Delete(id);
            if (result == -1)
                return NotFound();
            if (result == 0)
                return Problem("Delete failed!");

            return Ok("Deleted " + result + (result > 1 ? " records." : " record.") + " in system.");
        }

        // DELETE: api/Devices/5/delete-forever
        [HttpDelete("{id}/delete-forever")]
        public async Task<IActionResult> DeleteForever(Guid id)
        {
            var result = _service.DeleteForever(id);
            if( result == -2)
                return Problem("Entity set 'DataContext.Device'  is null.");
            if (result == -1)
                return NotFound();
            if (result == 0)
                return Problem("Delete failed!");

            return Ok("Deleted " + result + (result > 1 ? " records." : " record.") + " in system.");
        }
    }
}

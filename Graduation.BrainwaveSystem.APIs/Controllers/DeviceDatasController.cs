using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Graduation.BrainwaveSystem.Models;
using Graduation.BrainwaveSystem.Models.Entities;
using Graduation.BrainwaveSystem.Services.DeviceData;
using Graduation.BrainwaveSystem.Models.DTOs;

namespace Graduation.BrainwaveSystem.APIs.Controllers
{
    //[Route("api/[controller]")]
    [ApiController]
    public class DeviceDatasController : BasesController<DeviceData>//ControllerBase
    {
        private readonly IDeviceDataService _service;

        public DeviceDatasController(IDeviceDataService service) : base(service)
        {
            _service = service;
        }

        // POST: api/DeviceDatas
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<DeviceData>> PostDeviceData(DeviceDataRequest request)
        {
            var result = await _service.Create(request);
            if (result == Guid.Empty)
                return Problem("Insert failed! Please contact KhaiND to check problem.");

            return CreatedAtAction("Get", new { id = result }, result);
        }

        // PUT: api/DeviceDatas/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDeviceData(Guid id, DeviceDataRequest request)
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

        // DELETE: api/DeviceDatas/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDeviceData(Guid id)
        {
            var result = await _service.Delete(id);
            if (result == 0)
                return Problem("Update failed! Please contact KhaiND to check problem.");

            return Ok("Deleted " + result + (result > 1 ? " records." : " record.") + " in system.");
        }
    }
}

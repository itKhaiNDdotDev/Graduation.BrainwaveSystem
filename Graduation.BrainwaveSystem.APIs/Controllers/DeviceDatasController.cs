using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Graduation.BrainwaveSystem.Models;
using Graduation.BrainwaveSystem.Models.Factories;

namespace Graduation.BrainwaveSystem.APIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DeviceDatasController : ControllerBase
    {
        private readonly DataContext _context;

        public DeviceDatasController(DataContext context)
        {
            _context = context;
        }

        // GET: api/DeviceDatas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DeviceData>>> GetDeviceData()
        {
          if (_context.DeviceDatas == null)
          {
              return NotFound();
          }
            return await _context.DeviceDatas.ToListAsync();
        }

        // GET: api/DeviceDatas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DeviceData>> GetDeviceData(Guid id)
        {
          if (_context.DeviceDatas == null)
          {
              return NotFound();
          }
            var deviceData = await _context.DeviceDatas.FindAsync(id);

            if (deviceData == null)
            {
                return NotFound();
            }

            return deviceData;
        }

        // PUT: api/DeviceDatas/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDeviceData(Guid id, DeviceData deviceData)
        {
            if (id != deviceData.Id)
            {
                return BadRequest();
            }

            _context.Entry(deviceData).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DeviceDataExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/DeviceDatas
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<DeviceData>> PostDeviceData(DeviceData deviceData)
        {
          if (_context.DeviceDatas == null)
          {
              return Problem("Entity set 'DataContext.DeviceDatas'  is null.");
          }
            _context.DeviceDatas.Add(deviceData);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDeviceData", new { id = deviceData.Id }, deviceData);
        }

        // DELETE: api/DeviceDatas/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDeviceData(Guid id)
        {
            if (_context.DeviceDatas == null)
            {
                return NotFound();
            }
            var deviceData = await _context.DeviceDatas.FindAsync(id);
            if (deviceData == null)
            {
                return NotFound();
            }

            _context.DeviceDatas.Remove(deviceData);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool DeviceDataExists(Guid id)
        {
            return (_context.DeviceDatas?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}

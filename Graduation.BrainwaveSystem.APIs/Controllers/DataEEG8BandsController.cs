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
    public class DataEEG8BandsController : ControllerBase
    {
        private readonly DataContext _context;

        public DataEEG8BandsController(DataContext context)
        {
            _context = context;
        }

        // GET: api/DataEEG8Band
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DataEEG8Band>>> GetDataEEG8Band()
        {
          if (_context.DataEEG8Bands == null)
          {
              return NotFound();
          }
            return await _context.DataEEG8Bands.ToListAsync();
        }

        // GET: api/DataEEG8Band/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DataEEG8Band>> GetDataEEG8Band(Guid id)
        {
          if (_context.DataEEG8Bands == null)
          {
              return NotFound();
          }
            var dataEEG8Band = await _context.DataEEG8Bands.FindAsync(id);

            if (dataEEG8Band == null)
            {
                return NotFound();
            }

            return dataEEG8Band;
        }

        // PUT: api/DataEEG8Band/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDataEEG8Band(Guid id, DataEEG8Band dataEEG8Band)
        {
            if (id != dataEEG8Band.Id)
            {
                return BadRequest();
            }

            _context.Entry(dataEEG8Band).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DataEEG8BandExists(id))
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

        // POST: api/DataEEG8Band
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<DataEEG8Band>> PostDataEEG8Band(DataEEG8Band dataEEG8Band)
        {
          if (_context.DataEEG8Bands == null)
          {
              return Problem("Entity set 'DataContext.DataEEG8Bands'  is null.");
          }
            _context.DataEEG8Bands.Add(dataEEG8Band);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDataEEG8Band", new { id = dataEEG8Band.Id }, dataEEG8Band);
        }

        // DELETE: api/DataEEG8Band/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDataEEG8Band(Guid id)
        {
            if (_context.DataEEG8Bands == null)
            {
                return NotFound();
            }
            var dataEEG8Band = await _context.DataEEG8Bands.FindAsync(id);
            if (dataEEG8Band == null)
            {
                return NotFound();
            }

            _context.DataEEG8Bands.Remove(dataEEG8Band);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool DataEEG8BandExists(Guid id)
        {
            return (_context.DataEEG8Bands?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}

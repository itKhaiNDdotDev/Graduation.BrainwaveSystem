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
    public class DataRawsController : ControllerBase
    {
        private readonly DataContext _context;

        public DataRawsController(DataContext context)
        {
            _context = context;
        }

        // GET: api/DataRaws
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DataRaw>>> GetDataRaw()
        {
          if (_context.DataRaws == null)
          {
              return NotFound();
          }
            return await _context.DataRaws.ToListAsync();
        }

        // GET: api/DataRaws/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DataRaw>> GetDataRaw(Guid id)
        {
          if (_context.DataRaws == null)
          {
              return NotFound();
          }
            var dataRaw = await _context.DataRaws.FindAsync(id);

            if (dataRaw == null)
            {
                return NotFound();
            }

            return dataRaw;
        }

        // PUT: api/DataRaws/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDataRaw(Guid id, DataRaw dataRaw)
        {
            if (id != dataRaw.Id)
            {
                return BadRequest();
            }

            _context.Entry(dataRaw).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DataRawExists(id))
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

        // POST: api/DataRaws
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<DataRaw>> PostDataRaw(DataRaw dataRaw)
        {
          if (_context.DataRaws == null)
          {
              return Problem("Entity set 'DataContext.DataRaws'  is null.");
          }
            _context.DataRaws.Add(dataRaw);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDataRaw", new { id = dataRaw.Id }, dataRaw);
        }

        // DELETE: api/DataRaws/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDataRaw(Guid id)
        {
            if (_context.DataRaws == null)
            {
                return NotFound();
            }
            var dataRaw = await _context.DataRaws.FindAsync(id);
            if (dataRaw == null)
            {
                return NotFound();
            }

            _context.DataRaws.Remove(dataRaw);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool DataRawExists(Guid id)
        {
            return (_context.DataRaws?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}

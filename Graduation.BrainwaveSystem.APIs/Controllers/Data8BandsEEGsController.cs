using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Graduation.BrainwaveSystem.Models;
using Graduation.BrainwaveSystem.Models.Entities;
using Graduation.BrainwaveSystem.Models.DTOs;
using Graduation.BrainwaveSystem.Services.Data8BandsEEGServices;

namespace Graduation.BrainwaveSystem.APIs.Controllers
{
    //[Route("api/[controller]")]
    [ApiController]
    public class Data8BandsEEGsController : BasesController<Data8BandsEEG, Data8BandsEEGRequest>//: ControllerBase
    {
        private readonly IData8BandsEEGService _service;

        public Data8BandsEEGsController(IData8BandsEEGService service) : base(service)
        {
            _service = service;
        }



        //private readonly DataContext _context;

        //public Data8BandsEEGsController(DataContext context)
        //{
        //    _context = context;
        //}

        //// GET: api/Data8BandsEEG
        //[HttpGet]
        //public async Task<ActionResult<IEnumerable<Data8BandsEEG>>> GetData8BandsEEG()
        //{
        //  if (_context.Data8BandsEEGs == null)
        //  {
        //      return NotFound();
        //  }
        //    return await _context.Data8BandsEEGs.ToListAsync();
        //}

        //// GET: api/Data8BandsEEG/5
        //[HttpGet("{id}")]
        //public async Task<ActionResult<Data8BandsEEG>> GetData8BandsEEG(Guid id)
        //{
        //  if (_context.Data8BandsEEGs == null)
        //  {
        //      return NotFound();
        //  }
        //    var data8BandsEEG = await _context.Data8BandsEEGs.FindAsync(id);

        //    if (data8BandsEEG == null)
        //    {
        //        return NotFound();
        //    }

        //    return data8BandsEEG;
        //}

        //// PUT: api/Data8BandsEEG/5
        //// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutData8BandsEEG(Guid id, Data8BandsEEG data8BandsEEG)
        //{
        //    if (id != data8BandsEEG.Id)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(data8BandsEEG).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!Data8BandsEEGExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return NoContent();
        //}

        //// POST: api/Data8BandsEEG
        //// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPost]
        //public async Task<ActionResult<Data8BandsEEG>> PostData8BandsEEG(Data8BandsEEG data8BandsEEG)
        //{
        //  if (_context.Data8BandsEEGs == null)
        //  {
        //      return Problem("Entity set 'DataContext.Data8BandsEEG'  is null.");
        //  }
        //    _context.Data8BandsEEGs.Add(data8BandsEEG);
        //    await _context.SaveChangesAsync();

        //    return CreatedAtAction("GetData8BandsEEG", new { id = data8BandsEEG.Id }, data8BandsEEG);
        //}

        //// DELETE: api/Data8BandsEEG/5
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteData8BandsEEG(Guid id)
        //{
        //    if (_context.Data8BandsEEGs == null)
        //    {
        //        return NotFound();
        //    }
        //    var data8BandsEEG = await _context.Data8BandsEEGs.FindAsync(id);
        //    if (data8BandsEEG == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.Data8BandsEEGs.Remove(data8BandsEEG);
        //    await _context.SaveChangesAsync();

        //    return NoContent();
        //}

        //private bool Data8BandsEEGExists(Guid id)
        //{
        //    return (_context.Data8BandsEEGs?.Any(e => e.Id == id)).GetValueOrDefault();
        //}
    }
}

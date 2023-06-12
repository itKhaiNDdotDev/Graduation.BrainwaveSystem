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
using Graduation.BrainwaveSystem.Services.DataRawEEGServices;

namespace Graduation.BrainwaveSystem.APIs.Controllers
{
    //[Route("api/[controller]")]
    [ApiController]
    public class DataRawEEGsController : BasesController<DataRawEEG, DataRawEEGRequest>// : ControllerBase
    {
        public readonly IDataRawEEGService _service;

        public DataRawEEGsController(IDataRawEEGService service) : base(service)
        {
            _service = service;
        }

        [HttpGet("{deviceId}/Last5Mins")]
        public async Task<ActionResult<DataRawEEGResponse[]>> GetLast5Mins(Guid deviceId)
        {
            return Ok(await _service.GetLastNDataRecords(deviceId, 300));
        }

        [HttpGet("{deviceId}/LastMin")]
        public async Task<ActionResult<DataRawEEGResponse[]>> GetLastMin(Guid deviceId)
        {
            return Ok(await _service.GetLastNDataRecords(deviceId, 60));
        }

        [HttpGet("{deviceId}/Last")]
        public async Task<ActionResult<DataRawEEGResponse[]>> GetLast(Guid deviceId)
        {
            return Ok(await _service.GetLastNDataRecords(deviceId));
        }

        [HttpGet("{deviceId}/Last15Secs")]
        public async Task<ActionResult<DataRawEEGResponse[]>> GetLast15Secs(Guid deviceId)
        {
            return Ok(await _service.GetLastNDataRecords(deviceId, 15));
        }

        [HttpGet("{deviceId}/FFT")]
        public async Task<ActionResult<(List<double> frequencyAxis, List<double> amplitudeSpectrum)>> GetFFTData(Guid deviceId)
        {
            var frequencyAxis = _service.GetFFTData(deviceId).frequencyAxis;
            var amplitudeSpectrum = _service.GetFFTData(deviceId).amplitudeSpectrum;
            return Ok(new { FrequencyAxis = frequencyAxis, AmplitudeSpectrum = amplitudeSpectrum });
        }

        [HttpGet("svm-classification")]
        public async Task<ActionResult> GetTrainOutput()
        {
            return Ok(_service.GetTrainOutput());
        }

        [HttpGet("fasttree")]
        public async Task<ActionResult> GetTrainFTOutput()
        {
            return Ok(_service.GetTrainFTOutput());
        }

        //private readonly DataContext _context;

        //public DataRawEEGsController(DataContext context)
        //{
        //    _context = context;
        //}

        //// GET: api/DataRawEEGs
        //[HttpGet]
        //public async Task<ActionResult<IEnumerable<DataRawEEG>>> GetDataRawEEG()
        //{
        //  if (_context.DataRawEEGs == null)
        //  {
        //      return NotFound();
        //  }
        //    return await _context.DataRawEEGs.ToListAsync();
        //}

        //// GET: api/DataRawEEGs/5
        //[HttpGet("{id}")]
        //public async Task<ActionResult<DataRawEEG>> GetDataRawEEG(Guid id)
        //{
        //  if (_context.DataRawEEGs == null)
        //  {
        //      return NotFound();
        //  }
        //    var dataRawEEG = await _context.DataRawEEGs.FindAsync(id);

        //    if (dataRawEEG == null)
        //    {
        //        return NotFound();
        //    }

        //    return dataRawEEG;
        //}

        //// PUT: api/DataRawEEGs/5
        //// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutDataRawEEG(Guid id, DataRawEEG dataRawEEG)
        //{
        //    if (id != dataRawEEG.Id)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(dataRawEEG).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!DataRawEEGExists(id))
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

        //// POST: api/DataRawEEGs
        //// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPost]
        //public async Task<ActionResult<DataRawEEG>> PostDataRawEEG(DataRawEEG dataRawEEG)
        //{
        //  if (_context.DataRawEEGs == null)
        //  {
        //      return Problem("Entity set 'DataContext.DataRawEEG'  is null.");
        //  }
        //    _context.DataRawEEGs.Add(dataRawEEG);
        //    await _context.SaveChangesAsync();

        //    return CreatedAtAction("GetDataRawEEG", new { id = dataRawEEG.Id }, dataRawEEG);
        //}

        //// DELETE: api/DataRawEEGs/5
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteDataRawEEG(Guid id)
        //{
        //    if (_context.DataRawEEGs == null)
        //    {
        //        return NotFound();
        //    }
        //    var dataRawEEG = await _context.DataRawEEGs.FindAsync(id);
        //    if (dataRawEEG == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.DataRawEEGs.Remove(dataRawEEG);
        //    await _context.SaveChangesAsync();

        //    return NoContent();
        //}

        //private bool DataRawEEGExists(Guid id)
        //{
        //    return (_context.DataRawEEGs?.Any(e => e.Id == id)).GetValueOrDefault();
        //}
    }
}

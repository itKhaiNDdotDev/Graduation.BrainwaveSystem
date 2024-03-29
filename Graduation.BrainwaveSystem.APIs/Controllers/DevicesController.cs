﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Graduation.BrainwaveSystem.Models;
using Graduation.BrainwaveSystem.Models.Entities;
using Graduation.BrainwaveSystem.Models.DTOs;
using Graduation.BrainwaveSystem.Services.DeviceServices;
using Microsoft.AspNetCore.Authorization;

namespace Graduation.BrainwaveSystem.APIs.Controllers
{
    //[Route("api/[controller]")]
    [ApiController]
    public class DevicesController : BasesController<Device, DeviceRequest>//ControllerBase
    {
        private readonly IDeviceService _service;

        public DevicesController(IDeviceService service) : base(service)
        {
            _service = service;
        }

        [HttpGet("exist")]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult<IEnumerable<Device>>> GetListExist()
        {
            var results = await _service.GetListExist();
            return Ok(results);
        }

        [HttpGet]
        [Route("userId")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<Device>>> GetListByUserId(Guid userId)
        {
            var results = await _service.GetListExistByUserId(userId);
            return Ok(results);
        }

        [HttpPatch("{id}/active")]
        [Authorize]
        public async Task<IActionResult> ToggleActive(Guid id)
        {
            await _service.ToggleActive(id);
            return Ok();
        }

        // POST: api/Devices
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Authorize]
        public async Task<ActionResult<Device>> PostDevice(DeviceRequest request)
        {
            var result = await _service.Create(request);
            if (result == Guid.Empty)
                return Problem("Insert failed! Please contact KhaiND to check problem.");

            return CreatedAtAction("Get", new { id = result }, result);
        }

        //// PUT: api/Devices/5
        //// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutDevice(Guid id, DeviceRequest request)
        //{
        //    var result = await _service.Update(id, request);
        //    if (result == 0)
        //        return Problem("Update failed! Please contact KhaiND to check problem.");

        //    return Ok(new
        //    {
        //        Message = "Updated " + result + (result > 1 ? " records." : " record."),
        //        UpdatedRecord = await _service.GetById(id)
        //    });
        //}

        //// DELETE: api/Devices/5
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteDevice(Guid id)
        //{
        //    var result = await _service.Delete(id);
        //    if (result == 0)
        //        return Problem("Update failed! Please contact KhaiND to check problem.");

        //    return Ok("Deleted " + result + (result > 1 ? " records." : " record.") + " in system.");
        //}
    }
}

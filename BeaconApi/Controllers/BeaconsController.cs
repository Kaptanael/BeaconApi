using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BeaconApi.Models;
using BeaconApi.Data;
using Microsoft.Extensions.Configuration;

namespace BeaconApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BeaconsController : ControllerBase
    {
        private readonly BeaconDbContext _context;
        private readonly IConfiguration _configuration;

        public BeaconsController(BeaconDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        [HttpGet]
        public async Task<IActionResult> GetBeacon()
        {
            var repo = new BeaconRepository(_configuration);
            var recons = repo.GetAll();
            return Ok(recons);
            //return await _context.Beacon.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Beacon>> GetBeacon(Guid id)
        {
            var beacon = await _context.Beacon.FindAsync(id);

            if (beacon == null)
            {
                return NotFound();
            }

            return beacon;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutBeacon(Guid id, Beacon beacon)
        {
            if (id != beacon.GUID)
            {
                return BadRequest();
            }

            _context.Entry(beacon).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BeaconExists(id))
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

        [HttpPost]
        public async Task<ActionResult<Beacon>> PostBeacon(Beacon beacon)
        {
            _context.Beacon.Add(beacon);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBeacon", new { id = beacon.GUID }, beacon);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Beacon>> DeleteBeacon(Guid id)
        {
            var beacon = await _context.Beacon.FindAsync(id);
            if (beacon == null)
            {
                return NotFound();
            }

            _context.Beacon.Remove(beacon);
            await _context.SaveChangesAsync();

            return beacon;
        }

        private bool BeaconExists(Guid id)
        {
            return _context.Beacon.Any(e => e.GUID == id);
        }
    }
}

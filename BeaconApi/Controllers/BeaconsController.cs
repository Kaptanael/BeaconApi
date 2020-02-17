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
        private readonly IBeaconRepository _beaconRepository;

        public BeaconsController(IBeaconRepository beaconRepository)
        {                     
            _beaconRepository = beaconRepository;
        }
        
        [Route("get-all")]
        [HttpGet]
        public IActionResult GetAllBeacon()
        {
            var beaconsToReturn = _beaconRepository.GetAll();      
            
            return Ok(beaconsToReturn);            
        }

        [Route("get-by-uuid/{uuid}")]
        [HttpGet]
        public ActionResult<Beacon> GetBeaconByUUID(string uuid)
        {
            var beaconToReturn= _beaconRepository.GetBeaconByUUID(uuid);

            if (beaconToReturn == null)
            {
                return NotFound();
            }

            return beaconToReturn;
        }

        [Route("get-by-id/{guid}")]
        [HttpGet]
        public ActionResult<Beacon> GetBeaconById(string guid)
        {
            Guid validGuid;

            bool isValid = Guid.TryParse(guid, out validGuid);

            if (!isValid) 
            {
                BadRequest();
            }

            var beaconsToReturn = _beaconRepository.GetBeaconById(validGuid);

            if (beaconsToReturn == null)
            {
                return NotFound();
            }

            return beaconsToReturn;
        }

        [Route("insert")]
        [HttpPost]
        public ActionResult InsertBeacon(Beacon beacon)
        {
            _context.Beacon.Add(beacon);
            _beaconRepository.in

            return CreatedAtAction("GetBeacon", new { id = beacon.GUID }, beacon);
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

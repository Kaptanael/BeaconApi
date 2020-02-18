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
using System.Net;
using BeaconApi.Extensions;

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
            try
            {                
                var beaconsToReturn = _beaconRepository.GetAll();                
                return Ok(beaconsToReturn);
            }
            catch (Exception ex)
            {
                Log.Write(ex);
                return StatusCode(500);
            }
        }

        [Route("get-beacon/{uuid}/{major}/{minor}")]
        [HttpGet]
        public ActionResult<Beacon> GetBeacon(string uuid, int major, int minor)
        {
            try
            {
                var beaconsToReturn = _beaconRepository.GetBeacon(uuid, major, minor);

                if (beaconsToReturn == null)
                {
                    return NotFound();
                }

                return beaconsToReturn;
            }
            catch (Exception ex)
            {
                Log.Write(ex);
                return StatusCode(500);
            }
        }

        [Route("get-by-uuid/{uuid}")]
        [HttpGet]
        public ActionResult<Beacon> GetBeaconByUUID(string uuid)
        {
            try
            {
                var beaconToReturn = _beaconRepository.GetBeaconByUUID(uuid);

                if (beaconToReturn == null)
                {
                    return NotFound();
                }

                return beaconToReturn;
            }
            catch (Exception ex)
            {
                Log.Write(ex);
                return StatusCode(500);
            }
        }

        [Route("get-by-guid/{guid}")]
        [HttpGet]
        public ActionResult<Beacon> GetBeaconByGuid(string guid)
        {
            Guid validGuid;
            bool isValid = Guid.TryParse(guid, out validGuid);

            if (!isValid)
            {
                return BadRequest(guid);
            }

            try
            {
                var beaconToReturn = _beaconRepository.GetBeaconByGuid(validGuid);

                if (beaconToReturn == null)
                {
                    return NotFound();
                }

                return beaconToReturn;
            }
            catch (Exception ex)
            {
                Log.Write(ex);
                return StatusCode(500);
            }
        }        

        [Route("insert")]
        [HttpPost]
        public ActionResult InsertBeacon(Beacon beacon)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(beacon);
            }

            try
            {
                _beaconRepository.Insert(beacon);
            }
            catch (Exception ex)
            {
                Log.Write(ex);
                return StatusCode(500);
            }

            return Ok(HttpStatusCode.Created);
        }

        [Route("delete/{guid}")]
        [HttpDelete]
        public ActionResult DeleteBeacon(string guid)
        {
            Guid validGuid;
            bool isValid = Guid.TryParse(guid, out validGuid);

            if (!isValid)
            {
                return BadRequest(guid);
            }

            try
            {
                var beaconToReturn = _beaconRepository.GetBeaconByGuid(validGuid);

                if (beaconToReturn == null)
                {
                    return NotFound();
                }

                _beaconRepository.Delete(validGuid);

                return Ok(HttpStatusCode.NoContent);
            }
            catch (Exception ex)
            {
                Log.Write(ex);
                return StatusCode(500);
            }
        }

        [Route("update")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBeacon(Guid id, Beacon beacon)
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

        private bool BeaconExists(Guid id)
        {
            return _context.Beacon.Any(e => e.GUID == id);
        }
    }
}

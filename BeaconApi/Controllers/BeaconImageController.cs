using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using BeaconApi.Data;
using BeaconApi.Dtos.BeaconImage;
using BeaconApi.Extensions;
using BeaconApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BeaconApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BeaconImageController : ControllerBase
    {        
        private readonly IBeaconImageRepository _beaconImageRepository;

        public BeaconImageController(IBeaconImageRepository beaconImageRepository)
        {
            _beaconImageRepository = beaconImageRepository;
        }

        [Route("insert")]
        [HttpPost]
        public ActionResult InsertBeaconImage(BeaconImageForCreateDto beaconImageForCreateDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(beaconImageForCreateDto);
            }

            try
            {
                //_beaconImageRepository.Insert(beaconImageForCreateDto);
            }
            catch (Exception ex)
            {
                Log.Write(ex);
                return StatusCode(500);
            }

            return Ok(HttpStatusCode.Created);
        }
    }
}
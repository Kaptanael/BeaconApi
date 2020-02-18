using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using BeaconApi.Data;
using BeaconApi.Dtos.BeaconImage;
using BeaconApi.Extensions;
using BeaconApi.Models;
using BeaconApi.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BeaconApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BeaconImageController : ControllerBase
    {        
        private readonly IBeaconImageService _beaconImageService;

        public BeaconImageController(IBeaconImageService beaconImageService)
        {
            _beaconImageService = beaconImageService;
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
                _beaconImageService.Insert(beaconImageForCreateDto);
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
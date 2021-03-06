﻿using System;
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

        [Route("get-beacon-image/{uuid}")]
        [HttpGet]
        public ActionResult<List<BeaconImage>> GetBeaconImageByUUID(string guid)
        {
            Guid validUUID;
            bool isValid = Guid.TryParse(guid, out validUUID);

            if (!isValid)
            {
                return BadRequest(guid);
            }
            try
            {
                var beaconImagesToReturn = _beaconImageService.GetBeaconImageByBeaconGuid(validUUID);

                if (beaconImagesToReturn == null)
                {
                    return NotFound();
                }

                return beaconImagesToReturn;
            }
            catch (Exception ex)
            {
                Log.Write(ex);
                return StatusCode(500);
            }
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
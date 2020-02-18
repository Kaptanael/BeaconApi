using BeaconApi.Data;
using BeaconApi.Dtos.Beacon;
using BeaconApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BeaconApi.Services
{
    public class BeaconService : IBeaconService
    {
        private readonly IBeaconRepository _beaconRepository;

        public BeaconService(IBeaconRepository beaconRepository)
        {
            _beaconRepository = beaconRepository;
        }

        public bool Insert(BeaconForCreateDto beaconForCreateDto)
        {
            var beacon = new Beacon
            {
                UUID = beaconForCreateDto.UUID,
                ShortDescription = beaconForCreateDto.ShortDescription,
                LongDescription = beaconForCreateDto.LongDescription,
                Major = beaconForCreateDto.Major,
                Minor = beaconForCreateDto.Minor,
                SVGHeight = beaconForCreateDto.SVGHeight,
                SVGWidth = beaconForCreateDto.SVGWidth,
                ThumbnailImageBinary = string.IsNullOrEmpty(beaconForCreateDto.ThumbnailImageBinary) ? null : Convert.FromBase64String(beaconForCreateDto.ThumbnailImageBinary)
            };

            bool result = _beaconRepository.Insert(beacon);

            return result;
        }
    }
}

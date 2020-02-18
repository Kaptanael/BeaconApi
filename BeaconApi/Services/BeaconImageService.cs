using BeaconApi.Data;
using BeaconApi.Dtos.BeaconImage;
using BeaconApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BeaconApi.Services
{
    public class BeaconImageService:IBeaconImageService
    {
        private readonly IBeaconImageRepository _beaconImageRepository;

        public BeaconImageService(IBeaconImageRepository beaconImageRepository)
        {
            _beaconImageRepository = beaconImageRepository;
        }

        public BeaconImage GetBeaconImageByGuid(Guid guid)
        {
            return _beaconImageRepository.GetBeaconImageByGuid(guid);
        }

        public List<BeaconImage> GetBeaconImageByBeaconGuid(Guid guid)
        {
            return _beaconImageRepository.GetBeaconImageByBeaconGuid(guid);
        }

        public bool Insert(BeaconImageForCreateDto beaconImageForCreateDto)
        {
            Guid beaconGuid;
            bool isValidBeaconGuid = Guid.TryParse(beaconImageForCreateDto.BeaconGUID, out beaconGuid); 

            var beaconImage = new BeaconImage
            {
                BeaconGUID = beaconGuid,
                BeaconImageBinary= Convert.FromBase64String(beaconImageForCreateDto.BeaconImageBinary)
            };

            bool result = _beaconImageRepository.Insert(beaconImage);

            return result;
        }
    }
}

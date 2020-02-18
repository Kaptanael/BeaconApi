using BeaconApi.Dtos.BeaconImage;
using BeaconApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BeaconApi.Services
{
    public interface IBeaconImageService
    {
        bool Insert(BeaconImageForCreateDto beaconImageForCreateDto);
        BeaconImage GetBeaconImageByGuid(Guid guid);
        List<BeaconImage> GetBeaconImageByBeaconGuid(Guid guid);
    }
}

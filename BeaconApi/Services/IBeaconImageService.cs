using BeaconApi.Dtos.BeaconImage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BeaconApi.Services
{
    public interface IBeaconImageService
    {
        bool Insert(BeaconImageForCreateDto beaconImageForCreateDto);
    }
}

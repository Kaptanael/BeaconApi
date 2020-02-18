using System;
using System.Collections.Generic;
using BeaconApi.Models;

namespace BeaconApi.Data
{
    public interface IBeaconImageRepository
    {
        BeaconImage GetBeaconImageByGuid(Guid guid);
        List<BeaconImage> GetBeaconImageByBeaconGuid(Guid guid);
        bool Insert(BeaconImage beacon);
    }
}
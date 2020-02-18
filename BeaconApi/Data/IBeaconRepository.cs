using System;
using System.Collections.Generic;
using BeaconApi.Models;

namespace BeaconApi.Data
{
    public interface IBeaconRepository
    {
        List<Beacon> GetAll();

        Beacon GetBeaconByGuid(Guid guid);

        Beacon GetBeacon(string uuid, int major, int minor);

        Beacon GetBeaconByUUID(string uuid);

        bool Insert(Beacon beacon);

        bool Delete(Guid guid);

        bool BeaconExists(string uuid, int major, int minor);
    }
}
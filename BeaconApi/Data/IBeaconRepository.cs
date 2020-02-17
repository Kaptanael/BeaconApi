using System;
using System.Collections.Generic;
using BeaconApi.Models;

namespace BeaconApi.Data
{
    public interface IBeaconRepository
    {
        List<Beacon> GetAll();

        Beacon GetBeaconById(Guid guid);

        Beacon GetBeaconByUUID(string uuid);

        bool Insert(Beacon beacon);

        bool Delete(Guid guid);
    }
}
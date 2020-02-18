using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BeaconApi.Data
{
    public class UnitOfWork : IUnitOfWork
    {

        private readonly IConfiguration _configuration;

        public UnitOfWork(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        private IBeaconRepository _beacons;
        public IBeaconRepository Beacons
        {
            get
            {
                if (_beacons == null)
                {
                    _beacons = new BeaconRepository(_configuration);
                }
                return _beacons;
            }
        }

        private IBeaconImageRepository _beaconImage;
        public IBeaconImageRepository BeaconImages
        {
            get
            {
                if (_beaconImage == null)
                {
                    _beaconImage = new BeaconImageRepository(_configuration);
                }
                return _beaconImage;
            }
        }        
    }
}

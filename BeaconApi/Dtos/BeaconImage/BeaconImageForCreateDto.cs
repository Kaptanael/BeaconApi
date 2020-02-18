using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BeaconApi.Dtos.BeaconImage
{
    public class BeaconImageForCreateDto
    {
        public string BeaconGUID { get; set; }

        public string BeaconImage { get; set; }
    }
}

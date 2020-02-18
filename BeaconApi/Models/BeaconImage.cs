using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BeaconApi.Models
{
    public class BeaconImage
    {
        public Guid BeaconImageID { get; set; }

        public Guid BeaconGUID { get; set; }

        public byte[] BeaconImageBinary { get; set; }
    }
}

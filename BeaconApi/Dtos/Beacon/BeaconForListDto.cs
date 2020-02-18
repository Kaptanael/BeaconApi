using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BeaconApi.Dtos.Beacon
{
    public class BeaconForListDto
    {
        public Guid GUID { get; set; }

        public string UUID { get; set; }

        public string ShortDescription { get; set; }

        public string LongDescription { get; set; }

        public int Major { get; set; }

        public int Minor { get; set; }

        public string SVGHeight { get; set; }

        public string SVGWidth { get; set; }

        public string ThumbnailImageBinary { get; set; }
        
    }
}

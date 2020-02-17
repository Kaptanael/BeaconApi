using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BeaconApi.Models
{
    public class Beacon
    {        
        public Guid GUID { get; set; }

        public string UUID { get; set; }

        public string ShortDescription { get; set; }

        public string LongDescription { get; set; }

        public int Major { get; set; }

        public int Minor { get; set; }

        public string SVGHeight { get; set; }

        public string SVGWidth { get; set; }

        public string ThumbnailFilePath { get; set; }

        public string ImageFilePath { get; set; }

        public string VideoFilePath { get; set; }

    }
}

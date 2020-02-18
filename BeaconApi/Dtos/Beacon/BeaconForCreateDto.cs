using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BeaconApi.Dtos.Beacon
{
    public class BeaconForCreateDto
    {
        [Required, MaxLength(50)]
        public string UUID { get; set; }

        [Required, MaxLength(100)]
        public string ShortDescription { get; set; }
        
        public string LongDescription { get; set; }

        [Required]
        public int Major { get; set; }

        [Required]
        public int Minor { get; set; }

        public string SVGHeight { get; set; }

        public string SVGWidth { get; set; }

        public string ThumbnailImageBinary { get; set; }        
    }
}

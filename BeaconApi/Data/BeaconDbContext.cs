using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace BeaconApi.Models
{
    public class BeaconDbContext : DbContext
    {
        public BeaconDbContext (DbContextOptions<BeaconDbContext> options)
            : base(options)
        {
        }

        public DbSet<Beacon> Beacon { get; set; }
    }
}

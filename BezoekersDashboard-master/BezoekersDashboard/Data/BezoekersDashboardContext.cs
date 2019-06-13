using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using BezoekersDashboard.Models;

namespace BezoekersDashboard.Models
{
    public class BezoekersDashboardContext : DbContext
    {
        public BezoekersDashboardContext (DbContextOptions<BezoekersDashboardContext> options)
            : base(options)
        {
        }

        public DbSet<BezoekersDashboard.Models.Parking> Parking { get; set; }

        public DbSet<BezoekersDashboard.Models.Visitors> Visitors { get; set; }

        public DbSet<BezoekersDashboard.Models.Sponsor> Sponsor { get; set; }

        public DbSet<BezoekersDashboard.Models.Roos24> Roos24 { get; set; }
    }
}

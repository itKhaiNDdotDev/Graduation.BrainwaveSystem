using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Graduation.BrainwaveSystem.Models.Entities;

namespace Graduation.BrainwaveSystem.Models
{
    public class DataContext : DbContext
    {
        public DataContext (DbContextOptions<DataContext> options)
            : base(options)
        {
        }

        public DbSet<Graduation.BrainwaveSystem.Models.Entities.Device> Device { get; set; } = default!;

        public DbSet<Graduation.BrainwaveSystem.Models.Entities.DeviceData>? DeviceData { get; set; }

        public DbSet<Graduation.BrainwaveSystem.Models.Entities.DataRawEEG>? DataRawEEG { get; set; }

        public DbSet<Graduation.BrainwaveSystem.Models.Entities.Data8BandsEEG>? Data8BandsEEG { get; set; }
    }
}

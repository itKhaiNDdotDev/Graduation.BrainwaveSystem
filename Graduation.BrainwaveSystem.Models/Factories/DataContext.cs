using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graduation.BrainwaveSystem.Models.Factories
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options)
            : base(options)
        {
        }

        public DbSet<Device> Devices { get; set; } = default!;
        public DbSet<DeviceData> DeviceDatas { get; set; } = default!;
        public DbSet<DataEEG8Band> DataEEG8Bands { get; set; } = default!;
        public DbSet<DataRaw> DataRaws { get; set; } = default!;
    }
}

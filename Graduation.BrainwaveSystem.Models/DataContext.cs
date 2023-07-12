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
        public DataContext() : base() // Overload do gặp lỗi Unable object DataContext....design time...
        {
        }

        public DataContext (DbContextOptions<DataContext> options)
            : base(options)
        {
        }

        public DbSet<Device> Devices { get; set; } = default!;

        public DbSet<DeviceData>? DeviceDatas { get; set; }

        public DbSet<DataRawEEG>? DataRawEEGs { get; set; }

        public DbSet<Data8BandsEEG>? Data8BandsEEGs { get; set; }
    }
}

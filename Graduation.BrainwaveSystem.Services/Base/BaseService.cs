using Graduation.BrainwaveSystem.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graduation.BrainwaveSystem.Services.Base
{
    public class BaseService<T> : IBaseService<T> where T : class
    {
        private readonly DataContext _context;
        private readonly DbSet<T> Ts;

        public BaseService(DataContext context)
        {
            _context = context;
            Ts = _context.Set<T>();
        }

        public async Task<List<T>> GetAll()
        {
            if (Ts == null)
            {
                throw new Exception($"404 - Not Found: Entity set 'DataContext.{typeof(T).Name}s' is null.");
            }
            return await Ts.ToListAsync();
        }

        public async Task<T> GetById(Guid id)
        {
            if (Ts == null)
            {
                throw new Exception($"404 - Not Found: Entity set 'DataContext.{typeof(T).Name}s' is null.");
            }
            var record = await Ts.FindAsync(id);

            if (record == null)
            {
                throw new Exception($"404 - Not Found: {typeof(T).Name} record with id [{id}] is not exist.");
            }
            return record;
        }

        public async Task<int> DeleteForever(Guid id)
        {
            var record = await GetById(id);

            Ts.Remove(record);
            return await _context.SaveChangesAsync();
        }
    }
}

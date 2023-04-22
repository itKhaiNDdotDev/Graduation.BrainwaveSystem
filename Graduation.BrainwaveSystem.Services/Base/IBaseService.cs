using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graduation.BrainwaveSystem.Services.Base
{
    public interface IBaseService<T>
    {
        public Task<List<T>> GetAll();

        public Task<T> GetById(Guid id);

        public Task<int> DeleteForever(Guid id);
    }
}

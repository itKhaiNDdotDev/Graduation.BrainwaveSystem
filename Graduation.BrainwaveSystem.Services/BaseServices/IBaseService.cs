using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graduation.BrainwaveSystem.Services.BaseServices
{
    public interface IBaseService<T, TRequest>
    {
        public Task<List<T>> GetAll();

        public Task<T> GetById(Guid id);

        public Task<Guid> Create(TRequest request);

        public Task<int> Update(Guid id, TRequest request);

        public Task<int> Delete(Guid id);

        public Task<int> DeleteForever(Guid id);
    }
}
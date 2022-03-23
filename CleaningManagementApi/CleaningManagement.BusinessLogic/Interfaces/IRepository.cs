using System;
using System.Linq;
using System.Threading.Tasks;

namespace CleaningManagement.BusinessLogic.Interfaces
{
    public interface IRepository<T>
    {
        public Task<T> CreateAsync(T item);
        public Task<T> ReadAsync(Guid id);
        public Task<T> UpdateAsync(T item);
        public Task<T> DeleteAsync(Guid id);
        public Task SaveAsync();
        public IQueryable<T> ReadAll();
    }
}

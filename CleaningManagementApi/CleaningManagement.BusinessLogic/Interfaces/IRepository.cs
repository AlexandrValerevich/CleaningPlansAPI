using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace CleaningManagement.BusinessLogic.Interfaces
{
    public interface IRepository<T>
    {
        public Task<T> CreateAsync(T item);
        public Task<T> ReadAsync(Guid id);
        public Task<T> UpdateAsync(T item);
        public Task<bool> DeleteAsync(Guid id);
        public Task SaveAsync();
        public Task<IEnumerable<T>> ReadAllAsync();
        public Task<IEnumerable<T>> GetCliningPlansByCustomerIdAsync(int customerId);
    }
}

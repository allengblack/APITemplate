using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public interface IRepository<T> where T: class
    {
        Task<T> Create(T model);
        Task<T> Get(Guid id);
        Task<List<T>> Get();
        Task<T> Edit(T model);
        Task Delete(Guid id);
    }
}
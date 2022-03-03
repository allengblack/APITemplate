using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.DataAccess
{
    public class Repository <T> : IRepository<T> where T: class
    {
        public Task<T> Create(T model) => throw new NotImplementedException();

        public Task<T> Get(Guid id) => throw new NotImplementedException();

        public Task<List<T>> Get() => throw new NotImplementedException();

        public Task<T> Edit(T model) => throw new NotImplementedException();
        
        public Task Delete(Guid id) => throw new NotImplementedException();
    }
}
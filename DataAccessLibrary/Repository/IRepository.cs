﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLibrary.Repository
{
    public interface IRepository<T> where T : class
    {
        Task Add(T entity);

        Task Update(T entity);

        Task Delete(T entity);

        Task<IEnumerable<T>> GetAll();
    }
}

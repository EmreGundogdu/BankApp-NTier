using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankApp.Web.Data.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        void Create(T entity);
        void Remove(T entity);
        void Update(T entity);
        T GetById(object entity);
        List<T> GetAll();
        IQueryable<T> GetQuaryable();
    }
}

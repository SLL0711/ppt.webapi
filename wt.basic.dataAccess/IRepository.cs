using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wt.basic.dataAccess
{
    public interface IRepository<T>
    {
        Task Add(T t);
        Task<T> RetrieveById(int id);
        IQueryable<T> RetrieveAll();
        Task Delete(T t);
        Task DeleteById(int id);
        Task Update(T t);
    }
}

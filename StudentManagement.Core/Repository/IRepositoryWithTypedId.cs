using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagement.Core.Repository
{
    public interface IRepositoryWithTypedId<T, in TId>
    {
        void Delete(TId id);
        T Get(TId id);
        IEnumerable<T> GetAll();
        T SaveOrUpdate(T entity);
    }
}

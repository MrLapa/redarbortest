using System.Collections.Generic;
using System.Threading.Tasks;

namespace Redarbor.Core.Interfaces
{
    public interface IBase<T> where T : class
    {
        Task<T> Add(T entity);
        Task<IReadOnlyList<T>> GetAll();
        Task<T> GetById(int id);
        Task<T> Update(T entity);
        Task<T> Delete(T entity);
    }
}

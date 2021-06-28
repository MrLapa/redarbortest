using System.Collections.Generic;
using System.Threading.Tasks;

namespace Redarbor.Core.Interfaces
{
    public interface IBase<T> where T : class
    {        
        Task<IReadOnlyList<T>> GetAll();
        Task<T> GetById(int id);        
    }
}

using System.Collections.Generic;
using System.Threading.Tasks;

namespace Redarbor.Core.Interfaces
{
    public interface IDataAccess<T>
    {
        Task<int> SetDataInDataBase(T entity, string storeProcedure);
        Task<IReadOnlyList<T>> GetDataFromDataBase(string storeProcedure, int? id = null);
    }
}

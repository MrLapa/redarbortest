using Redarbor.Core.Entities;
using System.Threading.Tasks;

namespace Redarbor.Core.Interfaces
{
    public interface IEmployeeRepository : IBase<Employee>
    {
        Task<int> Add(Employee entity);
        Task<int> Update(Employee entity);
    }
}

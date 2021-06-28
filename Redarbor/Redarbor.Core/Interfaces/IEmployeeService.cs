using Redarbor.Core.Entities;
using System.Threading.Tasks;

namespace Redarbor.Core.Interfaces
{
    public interface IEmployeeService : IBase<Employee>
    {
        Task<Employee> Add(Employee entity);
        Task<Employee> Update(int id, Employee entity);
        Task Delete(int id);
    }
}

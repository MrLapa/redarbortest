using Redarbor.Core.Entities;

namespace Redarbor.Core.Interfaces
{
    public interface IUnitOfWork
    {
        IEmployeeRepository EmployeeRepository { get; }
        IDataAccess<Employee> EmployeeDataAccessRepository { get; }
        IDataAccess<DeleteEntity> DeleteEntityDataAccess { get; }
    }
}

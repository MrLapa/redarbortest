using Redarbor.Core.Entities;
using Redarbor.Core.Interfaces;
using Redarbor.Infrastructure.Data;

namespace Redarbor.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IEmployeeRepository employeeRepository;
        private readonly IDataAccess<Employee> employeeDataAccessRepository;
        private readonly IDataAccess<DeleteEntity> deleteEntityDataAccess;

        public IEmployeeRepository EmployeeRepository => employeeRepository
            ?? new EmployeeRepository(this);
        public IDataAccess<Employee> EmployeeDataAccessRepository => employeeDataAccessRepository
            ?? new DataAccess<Employee>();
        public IDataAccess<DeleteEntity> DeleteEntityDataAccess => deleteEntityDataAccess
            ?? new DataAccess<DeleteEntity>();
    }
}

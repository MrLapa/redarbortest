using Redarbor.Core.Entities;
using Redarbor.Core.Interfaces;
using Redarbor.Infrastructure.Data;

namespace Redarbor.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IEmployeeRepository employeeRepository;
        private readonly IDataAccess<Employee> employeeDataAccessRepository;

        public IEmployeeRepository EmployeeRepository => employeeRepository
            ?? new EmployeeRepository(this);
        public IDataAccess<Employee> EmployeeDataAccessRepository => employeeDataAccessRepository
            ?? new DataAccess<Employee>();
    }
}

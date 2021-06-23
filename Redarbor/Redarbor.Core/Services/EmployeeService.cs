using Redarbor.Core.Entities;
using Redarbor.Core.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Redarbor.Core.Services
{
    public class EmployeeService : IEmployeeService
    {
        public Task<Employee> Add(Employee entity)
        {
            throw new System.NotImplementedException();
        }

        public Task<Employee> Delete(Employee entity)
        {
            throw new System.NotImplementedException();
        }

        public Task<IReadOnlyList<Employee>> GetAll()
        {
            throw new System.NotImplementedException();
        }

        public Task<Employee> GetById(int id)
        {
            throw new System.NotImplementedException();
        }

        public Task<Employee> Update(Employee entity)
        {
            throw new System.NotImplementedException();
        }
    }
}

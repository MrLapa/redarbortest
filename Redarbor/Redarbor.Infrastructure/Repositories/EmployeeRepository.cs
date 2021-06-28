using Redarbor.Core.Entities;
using Redarbor.Core.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Redarbor.Infrastructure.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly IUnitOfWork unitOfWork;

        public EmployeeRepository(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<Employee> Add(Employee entity)
        {
            string procedure = "ADD_EMPLOYEE";
            var entityId = await unitOfWork
                .EmployeeDataAccessRepository
                .SetDataInDataBase(entity, procedure);

            entity.Id = entityId;

            return entity;
        }

        public async Task<Employee> Delete(Employee entity)
        {
            string procedure = "DELETE_EMPLOYEE";
            var entityId = await unitOfWork
                .EmployeeDataAccessRepository
                .SetDataInDataBase(entity, procedure);

            return entity;
        }

        public async Task<IReadOnlyList<Employee>> GetAll()
        {
            string procedure = "GET_EMPLOYEE";
            var data = await unitOfWork
                .EmployeeDataAccessRepository
                .GetDataFromDataBase(procedure);

            return data;
        }

        public async Task<Employee> GetById(int id)
        {
            string procedure = "GET_EMPLOYEES";
            var data = await unitOfWork
                .EmployeeDataAccessRepository
                .GetDataFromDataBase(procedure, id);

            var employee = data.FirstOrDefault();

            return employee;
        }

        public async Task<Employee> Update(Employee entity)
        {
            string procedure = "UPDATE_EMPLOYEE";
            await unitOfWork
                .EmployeeDataAccessRepository
                .SetDataInDataBase(entity, procedure);

            return entity;
        }
    }
}

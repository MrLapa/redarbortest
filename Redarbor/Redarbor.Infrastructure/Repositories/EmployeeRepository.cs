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

        public async Task<int> Add(Employee entity)
        {
            string procedure = "ADD_EMPLOYEE";
            var entityId = await unitOfWork
                .EmployeeDataAccessRepository
                .SetDataInDataBase(entity, procedure);

            return entityId;
        }

        public async Task<int> Delete(int id)
        {
            var deleteEntity = new DeleteEntity
            {
                Id = id
            };

            string procedure = "DELETE_EMPLOYEE";
            int affectedRows = await unitOfWork
                .DeleteEntityDataAccess
                .SetDataInDataBase(deleteEntity, procedure);

            return affectedRows;
        }

        public async Task<IReadOnlyList<Employee>> GetAll()
        {
            string procedure = "GET_EMPLOYEES";
            var data = await unitOfWork
                .EmployeeDataAccessRepository
                .GetDataFromDataBase(procedure);

            return data;
        }

        public async Task<Employee> GetById(int id)
        {
            string procedure = "GET_EMPLOYEE";
            var data = await unitOfWork
                .EmployeeDataAccessRepository
                .GetDataFromDataBase(procedure, id);

            var employee = data.FirstOrDefault();

            return employee;
        }

        public async Task<int> Update(Employee entity)
        {
            string procedure = "UPDATE_EMPLOYEE";
            int affectedRows = await unitOfWork
                .EmployeeDataAccessRepository
                .SetDataInDataBase(entity, procedure);

            return affectedRows;
        }
    }
}

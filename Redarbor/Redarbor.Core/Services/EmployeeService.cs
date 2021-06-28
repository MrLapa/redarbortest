using Redarbor.Core.Entities;
using Redarbor.Core.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Redarbor.Core.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IUnitOfWork unitOfWork;
        public EmployeeService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<Employee> Add(Employee entity)
        {
            var entityId = await unitOfWork.EmployeeRepository.Add(entity);
            entity.Id = entityId;

            return entity;
        }

        public async Task<IReadOnlyList<Employee>> GetAll()
        {
            var data = await unitOfWork.EmployeeRepository.GetAll();
            
            return data;
        }

        public Task<Employee> GetById(int id)
        {
            throw new System.NotImplementedException();
        }

        public async Task<Employee> Update(Employee entity)
        {            
            await unitOfWork.EmployeeRepository.Update(entity);

            return entity;
        }
    }
}

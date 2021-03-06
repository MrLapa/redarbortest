using Redarbor.Core.Entities;
using Redarbor.Core.Exceptions;
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

        public async Task<Employee> GetById(int id)
        {
            var data = await unitOfWork.EmployeeRepository.GetById(id);

            return data;
        }

        public async Task<Employee> Update(int id, Employee entity)
        {
            var employee = await GetById(id);
            if (employee == null)
            {
                throw new BusinessException("Employee doesn't exist");
            }

            entity.Id = id;
            await unitOfWork.EmployeeRepository.Update(entity);

            return entity;
        }

        public async Task Delete(int id)
        {
            var employee = await GetById(id);
            if (employee == null)
            {
                throw new BusinessException("Employee doesn't exist");
            }

            await unitOfWork.EmployeeRepository.Delete(id);
        }
    }
}

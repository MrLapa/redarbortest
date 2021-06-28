using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Redarbor.Api.Responses;
using Redarbor.Core.DTOs.Request;
using Redarbor.Core.Entities;
using Redarbor.Core.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Redarbor.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]    
    public class RedarborController : ControllerBase
    {
        private readonly IEmployeeService service;
        private readonly IMapper mapper;

        public RedarborController(IEmployeeService service, IMapper mapper)
        {            
            this.service = service;
            this.mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> AddEmployee(EmployeeRequestDto employee)
        {
            var mappedEmployee = mapper.Map<Employee>(employee);
            var addedEmployee = await service.Add(mappedEmployee);
            var response = new ApiResponse<Employee>(addedEmployee);

            return Ok(response);
        }

        [HttpGet]
        public async Task<IActionResult> GetEmployees()
        {            
            var employees = await service.GetAll();            
            var response = new ApiResponse<IReadOnlyList<Employee>>(employees);

            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetEmployee(int id)
        {
            var employee = await service.GetById(id);
            var response = new ApiResponse<Employee>(employee);

            return Ok(response);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEmployee(int id, EmployeeRequestDto employee)
        {
            var mappedSong = mapper.Map<Employee>(employee);            
            var addedEmployee = await service.Update(id, mappedSong);            
            var response = new ApiResponse<Employee>(addedEmployee);

            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployee(int id)
        {            
            await service.Delete(id);            
            var response = new ApiResponse<string>("Success delete");

            return Ok(response);
        }
    }
}

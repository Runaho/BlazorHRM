using System;
using BlazorHRM.Shared;

namespace BlazorHRM.Client.Services
{
    public interface IEmployeeService
    {
        Task<IEnumerable<Employee>> GetEmployees();
        Task<Employee> GetEmployeeDetail(int employeeId);
        Task<Employee> AddEmployee(Employee employee);
        Task<Employee> UpdateEmployee(Employee employee);
        Task<HttpResponseMessage> DeleteEmployee(int employeeId);
    }
}


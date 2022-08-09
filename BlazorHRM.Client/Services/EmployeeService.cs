using System;
using System.Text.Json;
using BlazorHRM.Shared;

namespace BlazorHRM.Client.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly HttpClient _httpClient;

        public EmployeeService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<Employee> AddEmployee(Employee employee)
        {
            return await new ServiceRequestHelper<Employee>(_httpClient).SendAsync("employee", employee, RequestTypes.Post);
        }

        public async Task<HttpResponseMessage> DeleteEmployee(int employeeId)
        {
            return await new ServiceRequestHelper<Employee>(_httpClient).DeleteAsync("employee", employeeId.ToString());
        }

        public async Task<Employee> GetEmployeeDetail(int employeeId)
        {
            return await new ServiceRequestHelper<Employee>(_httpClient).GetAsync("employee", $"?id={employeeId}");
        }

        public async Task<IEnumerable<Employee>> GetEmployees()
        {
            return await new ServiceRequestHelper<IEnumerable<Employee>>(_httpClient).GetAsync("employees");
        }

        public async Task<Employee> UpdateEmployee(Employee employee)
        {
            return await new ServiceRequestHelper<Employee>(_httpClient).SendAsync("employee", employee, RequestTypes.Put);
        }
    }
}


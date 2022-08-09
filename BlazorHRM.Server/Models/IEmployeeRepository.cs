using BlazorHRM.Shared;

namespace BlazorHRM.Server.Models
{
    public interface IEmployeeRepository
    {
        /// <summary>
        /// Gets all employees
        /// </summary>
        /// <returns>Json Employee Array</returns>
        IEnumerable<Employee> GetAllEmployees();

        /// <summary>
        /// Get employee by id
        /// </summary>
        /// <param name="employeeId">Employee Identity</param>
        /// <returns>Json Employee Model</returns>
        Employee GetEmployeeById(int employeeId);

        /// <summary>
        /// Create new employee
        /// </summary>
        /// <param name="employee">Employee Model</param>
        /// <returns>Created Employee</returns>
        Employee AddEmployee(Employee employee);

        /// <summary>
        /// Update employee
        /// </summary>
        /// <param name="employee">Employee Model</param>
        /// <returns>Updated Employee</returns>
        Employee UpdateEmployee(Employee employee);

        /// <summary>
        /// Delete Employee
        /// </summary>
        /// <param name="employeeId">Employee Identity</param>
        void DeleteEmployee(int employeeId);
    }
}

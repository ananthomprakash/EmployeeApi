using Employees.Data.Classes;
using Employees.Data.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Employees.Data.Interface
{
    public interface IEmployeeRepository
    {
        Task<List<Employee>> GetEmployees();

        Task<List<Department>> GetDepartments();

        Task<List<EmployeeDetails>> GetEmployeeDetails();

        Task<EmployeeDetails> GetEmployeeDetail(int? employeeId);

        Task<int> AddEmployee(EmployeeRequest employee);

        Task<int> DeleteEmployee(int? employeeId);

        Task<int> UpdateEmployee(Employee employee);

    }
}

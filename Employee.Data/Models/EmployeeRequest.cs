using Newtonsoft.Json;

namespace Employees.Data.Models
{
    public class EmployeeRequest
    {

        [JsonProperty("employeeName")]
        public string EmployeeName { get; set; }
        [JsonProperty("employeeSalary")]
        public double? EmployeeSalary { get; set; }
        [JsonProperty("employeeCity")]
        public string EmployeeCity { get; set; }
    }
}

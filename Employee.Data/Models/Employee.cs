using System;
using System.Collections.Generic;

#nullable disable

namespace Employees.Data.Models
{
    public partial class Employee
    {
        public Employee()
        {
            Departments = new HashSet<Department>();
        }

        public int EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public decimal? EmployeeSalary { get; set; }
        public string EmployeeCity { get; set; }

        public virtual ICollection<Department> Departments { get; set; }
    }
}

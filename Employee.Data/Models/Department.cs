using System;
using System.Collections.Generic;

#nullable disable

namespace Employees.Data.Models
{
    public partial class Department
    {
        public int DepartmentId { get; set; }
        public string DepartmentName { get; set; }
        public int? EmployeeId { get; set; }
        public DateTime? CreatedDate { get; set; }

        public virtual Employee Employee { get; set; }
    }
}

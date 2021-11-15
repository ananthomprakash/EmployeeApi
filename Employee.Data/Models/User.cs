using System;
using System.Collections.Generic;

#nullable disable

namespace Employees.Data.Models
{
    public partial class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public bool IsAdmin { get; set; }
    }
}

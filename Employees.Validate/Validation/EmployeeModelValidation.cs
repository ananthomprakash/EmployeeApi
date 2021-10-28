using Employees.Data.Models;
using FluentValidation;

namespace Employees.Validate.Validation
{
    public class EmployeeModelValidation : AbstractValidator<Employee>
    {
       public EmployeeModelValidation()
        {
            RuleFor(x => x.EmployeeId).NotNull().NotEmpty().WithMessage("{PropertyName} must be entered to update the employee}");
            RuleFor(x => x.EmployeeName).NotNull().NotEmpty();
            
        }
    }
}

using Employees.Data.Models;
using FluentValidation;

namespace Employees.Validate.Validation
{
    public class EmployeeValidation : AbstractValidator<EmployeeRequest>
    {
        public EmployeeValidation()
        {
            RuleFor(x => x.EmployeeName).NotNull().NotEmpty().WithMessage("{PropertyName} should not be empty!");

           
        }
    }
}

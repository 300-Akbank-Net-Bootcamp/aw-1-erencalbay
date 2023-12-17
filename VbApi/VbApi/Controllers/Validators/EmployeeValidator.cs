using FluentValidation;
using Microsoft.Extensions.Options;

namespace VbApi.Controllers.Validators
{
    public class EmployeeValidator : AbstractValidator<Employee>
    {
        public EmployeeValidator()
        {

            ValidatorOptions.Global.DefaultRuleLevelCascadeMode = CascadeMode.Stop;
            RuleFor(employee => employee.Name)
                .NotNull().NotEmpty().WithMessage("Name cannot be empty")
                .MinimumLength(10).WithMessage("Name must be at least 10 characters long")
                .MaximumLength(25).WithMessage("Name cannot exceed 25 characters");
            RuleFor(employee => employee.Phone)
                .NotNull().NotEmpty().WithMessage("Phone number cannot be empty")
                .Matches(@"(0(\d{3})(\d{3})(\d{2})(\d{2}))$").WithMessage("Please enter a valid phone number starting with '05XX' and consisting of 11 digits");
            RuleFor(employee => employee.Email)
                .NotNull().WithMessage("Email cannot be empty")
                .EmailAddress().WithMessage("Please enter a valid email address");
            RuleFor(employee => employee.DateOfBirth)
                .NotNull().WithMessage("Date of birth cannot be empty")
                .LessThanOrEqualTo(DateTime.Today).WithMessage("Please enter a valid date of birth")
                .LessThan(DateTime.Now.AddYears(65)).WithMessage("Please enter a valid date of birth");
            RuleFor(employee => employee.HourlySalary)
                .NotEmpty().WithMessage("Salary cannot be empty")
                .When(employee => employee.DateOfBirth >= DateTime.Today.AddYears(-30)).InclusiveBetween(200, 400).WithMessage("Hourly salary for senior employees must be between 200 and 500")
                .When(employee => employee.DateOfBirth < DateTime.Today.AddYears(-30)).InclusiveBetween(100, 400).WithMessage("Hourly salary for junior employees must be between 100 and 400");
        }
    }  
}

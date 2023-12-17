using FluentValidation;

namespace VbApi.Controllers.Validators
{
    public class StaffValidator : AbstractValidator<Staff>
    {
        public StaffValidator()
        {
            RuleFor(staff => staff.Name)
                .NotNull().NotEmpty().WithMessage("Name cannot be empty")
                .MinimumLength(10).WithMessage("Name must be at least 10 characters long")
                .MaximumLength(25).WithMessage("Name cannot exceed 25 characters");
            RuleFor(staff => staff.Phone)
                .NotNull().NotEmpty().WithMessage("Phone number cannot be empty")
                .Matches(@"(0(\d{3})(\d{3})(\d{2})(\d{2}))$").WithMessage("Please enter a valid phone number starting with '05XX' and consisting of 11 digits");
            RuleFor(staff => staff.Email)
                .NotNull().WithMessage("Email cannot be empty")
                .EmailAddress().WithMessage("Please enter a valid email address");
            RuleFor(staff => staff.HourlySalary)
                .NotEmpty().WithMessage("Salary cannot be empty")
                .InclusiveBetween(30, 400).WithMessage("Salary must be between 30 and 400");
        }
    }
}

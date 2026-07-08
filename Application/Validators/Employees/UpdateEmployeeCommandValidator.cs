using Application.Commands.Employees;
using FluentValidation;

namespace Application.Validators.Employees;

public sealed class UpdateEmployeeCommandValidator : AbstractValidator<UpdateEmployeeCommand>
{
    public UpdateEmployeeCommandValidator()
    {
        RuleFor(e => e.Employee.Name).NotEmpty().MaximumLength(30);

        RuleFor(e => e.Employee.Age).NotEmpty().InclusiveBetween(18, 65);

        RuleFor(e => e.Employee.Position).NotEmpty().MaximumLength(20);
    }
}

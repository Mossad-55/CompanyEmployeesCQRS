using Application.Commands.Companies;
using FluentValidation;

namespace Application.Validators.Companies;

public sealed class UpdateCompanyCommandValidator : AbstractValidator<UpdateCompanyCommand>
{
    public UpdateCompanyCommandValidator()
    {
        RuleFor(c => c.Company.Name).NotEmpty().MaximumLength(30);

        RuleFor(c => c.Company.Address).NotEmpty().MaximumLength(60);

        RuleFor(c => c.Company.Country).NotEmpty().MaximumLength(5);
    }
}

using FluentValidation;
using RentalCar.Service.Application.Commands.Request;

namespace RentalCar.Service.Application.Validators;

public class CreateServiceValidator : AbstractValidator<CreateServiceRequest>
{
    public CreateServiceValidator()
    {
        RuleFor(m => m.Name)
            .NotEmpty().WithMessage("Informe o nome");

        RuleFor(m => m.Description)
            .NotEmpty().WithMessage("Informa a descrição");
    }
}


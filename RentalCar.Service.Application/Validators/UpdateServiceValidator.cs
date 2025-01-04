using FluentValidation;
using RentalCar.Service.Application.Commands.Request;

namespace RentalCar.Service.Application.Validators;

public class UpdateServiceValidator : AbstractValidator<UpdateServiceRequest>
{
    public UpdateServiceValidator()
    {
        RuleFor(u => u.Id)
            .NotEmpty().WithMessage("Informe o código");

        RuleFor(m => m.Name)
            .NotEmpty().WithMessage("Informe o nome");

        RuleFor(m => m.Description)
            .NotEmpty().WithMessage("Informa a Descrição");
    }
}


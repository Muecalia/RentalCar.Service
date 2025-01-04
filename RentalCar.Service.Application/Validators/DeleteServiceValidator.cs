using FluentValidation;
using RentalCar.Service.Application.Commands.Request;

namespace RentalCar.Service.Application.Validators;

public class DeleteServiceValidator : AbstractValidator<DeleteServiceRequest>
{
    public DeleteServiceValidator()
    {
        RuleFor(c => c.Id)
            .NotEmpty().WithMessage("Informe o código");
    }
}


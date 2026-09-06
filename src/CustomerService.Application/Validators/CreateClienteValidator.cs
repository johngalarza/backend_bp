using CustomerService.Application.DTOs;
using FluentValidation;

namespace CustomerService.Application.Validators;

public class CreateClienteValidator : AbstractValidator<CreateClienteDto>
{
    public CreateClienteValidator()
    {
        RuleFor(x => x.Nombre)
            .NotEmpty()
            .WithMessage("El nombre es obligatorio.")
            .MaximumLength(150)
            .WithMessage("El nombre no puede superar los 150 caracteres.");

        RuleFor(x => x.Genero)
            .NotEmpty()
            .WithMessage("El género es obligatorio.")
            .MaximumLength(50)
            .WithMessage("El género no puede superar los 50 caracteres.");

        RuleFor(x => x.Edad)
            .InclusiveBetween(18, 120)
            .WithMessage("La edad debe estar entre 18 y 120 años.");

        RuleFor(x => x.Identificacion)
            .NotEmpty()
            .WithMessage("La identificación es obligatoria.")
            .MaximumLength(20)
            .WithMessage("La identificación no puede superar los 20 caracteres.");

        RuleFor(x => x.Direccion)
            .NotEmpty()
            .WithMessage("La dirección es obligatoria.")
            .MaximumLength(250)
            .WithMessage("La dirección no puede superar los 250 caracteres.");

        RuleFor(x => x.Telefono)
            .NotEmpty()
            .WithMessage("El teléfono es obligatorio.")
            .MaximumLength(20)
            .WithMessage("El teléfono no puede superar los 20 caracteres.");

        RuleFor(x => x.ClienteId)
            .NotEmpty()
            .WithMessage("El ClienteId es obligatorio.")
            .MaximumLength(50)
            .WithMessage("El ClienteId no puede superar los 50 caracteres.");

        RuleFor(x => x.Password)
            .NotEmpty()
            .WithMessage("La contraseña es obligatoria.")
            .MinimumLength(6)
            .WithMessage("La contraseña debe tener al menos 6 caracteres.");
    }
}
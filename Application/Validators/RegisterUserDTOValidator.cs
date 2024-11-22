using Application.DTOs;
using FluentValidation;

namespace Application.Validators
{
    public class RegisterUserDTOValidator : AbstractValidator<RegisterUserDTO>
    {
        public RegisterUserDTOValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("El nombre es requerido")
                .MaximumLength(100).WithMessage("El nombre no puede tener más de 100 caracteres");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("El email es requerido")
                .EmailAddress().WithMessage("El formato del email no es válido");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("La contraseña es requerida")
                .MinimumLength(6).WithMessage("La contraseña debe tener al menos 6 caracteres");

            RuleFor(x => x.ConfirmPassword)
                .Equal(x => x.Password).WithMessage("Las contraseñas no coinciden");

            RuleFor(x => x.RoleIds)
                .NotEmpty().WithMessage("Debe seleccionar al menos un rol");
        }
    }
}

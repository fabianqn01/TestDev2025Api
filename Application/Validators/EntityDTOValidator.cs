using Application.DTOs;
using FluentValidation;

namespace Application.Validators
{
    public class EntityDTOValidator : AbstractValidator<EntityDTO>
    {
        public EntityDTOValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("El nombre es requerido")
                .MaximumLength(100).WithMessage("El nombre no puede tener más de 100 caracteres");

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("La descripción es requerida")
                .MaximumLength(500).WithMessage("La descripción no puede tener más de 500 caracteres");
        }
    }
}

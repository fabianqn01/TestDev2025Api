using Application.DTOs;
using FluentValidation;

namespace Application.Validators
{
    public class EmployeeDTOValidator : AbstractValidator<EmployeeDTO>
    {
        public EmployeeDTOValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("El nombre es requerido")
                .MaximumLength(100).WithMessage("El nombre no puede tener más de 100 caracteres");

            RuleFor(x => x.Position)
                .NotEmpty().WithMessage("El cargo es requerido")
                .MaximumLength(100).WithMessage("El cargo no puede tener más de 100 caracteres");

            RuleFor(x => x.Salary)
                .GreaterThan(0).WithMessage("El salario debe ser mayor que 0");

            RuleFor(x => x.EntityId)
                .GreaterThan(0).WithMessage("La entidad es requerida");
        }
    }
}

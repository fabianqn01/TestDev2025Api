using Application.DTOs;
using Application.Validators;
using FluentValidation.TestHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Tests.Validators
{
    public class EntityDTOValidatorTests
    {
        private readonly EntityDTOValidator _validator;

        public EntityDTOValidatorTests()
        {
            _validator = new EntityDTOValidator();
        }

        [Fact]
        public void Should_HaveError_When_NameIsEmpty()
        {
            var model = new EntityDTO { Name = "" };
            var result = _validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(x => x.Name)
                  .WithErrorMessage("El nombre es requerido");
        }

        [Fact]
        public void Should_NotHaveError_When_NameIsValid()
        {
            var model = new EntityDTO { Name = "Valid Entity" };
            var result = _validator.TestValidate(model);
            result.ShouldNotHaveValidationErrorFor(x => x.Name);
        }

        [Fact]
        public void Should_HaveError_When_DescriptionIsEmpty()
        {
            var model = new EntityDTO { Description = "" };
            var result = _validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(x => x.Description)
                  .WithErrorMessage("La descripción es requerida");
        }
    }
}

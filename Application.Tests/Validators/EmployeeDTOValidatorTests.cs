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
    public class EmployeeDTOValidatorTests
    {
        private readonly EmployeeDTOValidator _validator;

        public EmployeeDTOValidatorTests()
        {
            _validator = new EmployeeDTOValidator();
        }

        [Fact]
        public void Should_HaveError_When_NameIsEmpty()
        {
            var model = new EmployeeDTO { Name = "" };
            var result = _validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(x => x.Name);
        }

        [Fact]
        public void Should_NotHaveError_When_NameIsValid()
        {
            var model = new EmployeeDTO { Name = "John Doe" };
            var result = _validator.TestValidate(model);
            result.ShouldNotHaveValidationErrorFor(x => x.Name);
        }
    }
}

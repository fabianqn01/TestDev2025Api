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
    public class RegisterUserDTOValidatorTests
    {
        private readonly RegisterUserDTOValidator _validator;

        public RegisterUserDTOValidatorTests()
        {
            _validator = new RegisterUserDTOValidator();
        }

        [Fact]
        public void Should_Have_Error_When_Name_Is_Empty()
        {
            // Arrange
            var model = new RegisterUserDTO { Name = string.Empty, Email = "test@example.com", Password = "password123", ConfirmPassword = "password123" };

            // Act
            var result = _validator.TestValidate(model);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.Name);
        }

        [Fact]
        public void Should_Have_Error_When_Email_Is_Empty()
        {
            // Arrange
            var model = new RegisterUserDTO { Name = "Test User", Email = string.Empty, Password = "password123", ConfirmPassword = "password123" };

            // Act
            var result = _validator.TestValidate(model);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.Email);
        }

        [Fact]
        public void Should_Have_Error_When_Email_Is_Invalid()
        {
            // Arrange
            var model = new RegisterUserDTO { Name = "Test User", Email = "invalid-email", Password = "password123", ConfirmPassword = "password123" };

            // Act
            var result = _validator.TestValidate(model);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.Email);
        }

        [Fact]
        public void Should_Have_Error_When_Password_Is_Empty()
        {
            // Arrange
            var model = new RegisterUserDTO { Name = "Test User", Email = "test@example.com", Password = string.Empty, ConfirmPassword = "password123" };

            // Act
            var result = _validator.TestValidate(model);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.Password);
        }

        [Fact]
        public void Should_Have_Error_When_Password_Is_Too_Short()
        {
            // Arrange
            var model = new RegisterUserDTO { Name = "Test User", Email = "test@example.com", Password = "123", ConfirmPassword = "123" };

            // Act
            var result = _validator.TestValidate(model);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.Password);
        }

        [Fact]
        public void Should_Have_Error_When_Password_And_ConfirmPassword_Do_Not_Match()
        {
            // Arrange
            var model = new RegisterUserDTO { Name = "Test User", Email = "test@example.com", Password = "password123", ConfirmPassword = "password124" };

            // Act
            var result = _validator.TestValidate(model);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.ConfirmPassword);
        }

        [Fact]
        public void Should_Have_Error_When_RoleIds_Are_Empty()
        {
            // Arrange
            var model = new RegisterUserDTO { Name = "Test User", Email = "test@example.com", Password = "password123", ConfirmPassword = "password123", RoleIds = new List<int>() };

            // Act
            var result = _validator.TestValidate(model);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.RoleIds);
        }

        [Fact]
        public void Should_Not_Have_Error_When_Valid_Properties()
        {
            // Arrange
            var model = new RegisterUserDTO
            {
                Name = "Test User",
                Email = "test@example.com",
                Password = "password123",
                ConfirmPassword = "password123",
                RoleIds = new List<int> { 1 }
            };

            // Act
            var result = _validator.TestValidate(model);

            // Assert
            result.ShouldNotHaveValidationErrorFor(x => x.Name);
            result.ShouldNotHaveValidationErrorFor(x => x.Email);
            result.ShouldNotHaveValidationErrorFor(x => x.Password);
            result.ShouldNotHaveValidationErrorFor(x => x.ConfirmPassword);
            result.ShouldNotHaveValidationErrorFor(x => x.RoleIds);
        }
    }
}

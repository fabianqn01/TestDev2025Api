using Application.Services;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Tests.Services
{
    public class ApiResponseServiceTests
    {
        private readonly ApiResponseService _service;

        public ApiResponseServiceTests()
        {
            _service = new ApiResponseService();
        }

        [Fact]
        public void Success_ShouldReturnSuccessfulApiResponse()
        {
            // Arrange
            var data = "Test Data";

            // Act
            var response = _service.Success(data, "Success message");

            // Assert
            response.Should().NotBeNull();
            response.Success.Should().BeTrue();
            response.Message.Should().Be("Success message");
            response.Data.Should().Be(data);
        }

        [Fact]
        public void Error_ShouldReturnErrorApiResponse()
        {
            // Arrange
            string message = "Error occurred";

            // Act
            var response = _service.Error<string>(message);

            // Assert
            response.Success.Should().BeFalse();
            response.Message.Should().Be(message);
            response.Data.Should().BeNull();
        }

        [Fact]
        public void Exception_ShouldReturnExceptionApiResponse()
        {
            // Arrange
            string message = "Unexpected error";

            // Act
            var response = _service.Exception<string>(message);

            // Assert
            response.Success.Should().BeFalse();
            response.Message.Should().Be(message);
            response.Data.Should().BeNull();
        }
    }
}

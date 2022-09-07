using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CloudCustomers.API.Controllers;
using CloudCustomers.API.Models;
using CloudCustomers.API.Services;
using CloudCustomers.UnitTests.Fixtures;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace CloudCustomers.UnitTests.Systems.Controllers
{
    public class TestUsersController
    {
        /// <summary>
        /// Check is Get() returns status code 200.
        /// </summary>
        /// <returns>Returns status code 200.</returns>
        [Fact]
        public async Task Get_OnSuccess_ReturnsStatusCode200()
        {
            // Arrange
            var mockUsersService = new Mock<IUsersService>();
            mockUsersService
                .Setup(service => service.GetAllUsers())
                .ReturnsAsync(UsersFixture.GetTestUsers());
            var sut = new UsersController(mockUsersService.Object);

            // Act
            var result = (OkObjectResult)await sut.Get();

            // Assert
            result.StatusCode.Should().Be(200);
        }

        /// <summary>
        /// Check is Get() invokes user service exactly once.
        /// </summary>
        /// <returns>Returns invokes user service exactly once.</returns>
        [Fact]
        public async Task Get_OnSuccess_InvokesUserServiceExactlyOnce()
        {
            // Arrange
            var mockUsersService = new Mock<IUsersService>();
            mockUsersService
                .Setup(service => service.GetAllUsers())
                .ReturnsAsync(new List<User>());

            var sut = new UsersController(mockUsersService.Object);

            // Act
            var result = await sut.Get();

            // Assert
            mockUsersService.Verify(service => service.GetAllUsers(), Times.Once);
        }

        /// <summary>
        /// Check is Get() on success returns list of users.
        /// </summary>
        /// <returns>Returns list of users.</returns>
        [Fact]
        public async Task Get_OnSuccess_ReturnsListOfUsers()
        {
            // Arrange
            var mockUsersService = new Mock<IUsersService>();

            mockUsersService
                .Setup(service => service.GetAllUsers())
                .ReturnsAsync(UsersFixture.GetTestUsers());

            var sut = new UsersController(mockUsersService.Object);

            // Act
            var result = await sut.Get();

            // Assert
            result.Should().BeOfType<OkObjectResult>();
            var objectResult = (OkObjectResult)result;
            objectResult.Value.Should().BeOfType<List<User>>();
        }

        /// <summary>
        /// Check is Get() returns 404 when no users found.
        /// </summary>
        /// <returns>Returns status code 404.</returns>
        [Fact]
        public async Task Get_OnNoUsersFound_Returns404()
        {
            // Arrange
            var mockUsersService = new Mock<IUsersService>();

            mockUsersService
                .Setup(service => service.GetAllUsers())
                .ReturnsAsync(new List<User>());

            var sut = new UsersController(mockUsersService.Object);

            // Act
            var result = await sut.Get();

            // Assert
            result.Should().BeOfType<NotFoundResult>();
            var objectResult = (NotFoundResult)result;
            objectResult.StatusCode.Should().Be(404);
        }

    }
}

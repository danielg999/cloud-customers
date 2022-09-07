using CloudCustomers.API.Config;
using CloudCustomers.API.Models;
using CloudCustomers.API.Services;
using CloudCustomers.UnitTests.Fixtures;
using CloudCustomers.UnitTests.Helpers;
using FluentAssertions;
using Microsoft.Extensions.Options;
using Moq;
using Moq.Protected;

namespace CloudCustomers.UnitTests.Systems.Services
{
    public class TestUsersService
    {
        /// <summary>
        /// The endpoint.
        /// </summary>
        private const string Endpoint = "https://example.com/users";

        /// <summary>
        /// Get all users when called and invokes once HTTP GET request.
        /// </summary>
        /// <returns>Should invokes once HTTP GET request.</returns>
        [Fact]
        public async Task GetAllUsers_WhenCalled_InvokesHttpGetRequest()
        {
            // Arrange
            var expectedResponse = UsersFixture.GetTestUsers();
            var handlerMock = MockHttpMessageHandler<User>.SetupBasicGetResourceList(expectedResponse);
            var httpClient = new HttpClient(handlerMock.Object);

            var config = Options.Create(
                new UsersApiOptions
                {
                    Endpoint = Endpoint
                });
            var sut = new UsersService(httpClient, config);

            // Act
            await sut.GetAllUsers();

            // Assert
            handlerMock
                .Protected()
                .Verify(
                    "SendAsync",
                    Times.Exactly(1),
                    ItExpr.Is<HttpRequestMessage>(req => req.Method == HttpMethod.Get),
                    ItExpr.IsAny<CancellationToken>());
        }

        /// <summary>
        /// Get all users when called.
        /// </summary>
        /// <returns>Returns list of users.</returns>
        [Fact]
        public async Task GetAllUsers_WhenCalled_ReturnsListOfUsers()
        {
            // Arrange
            var expectedResponse = UsersFixture.GetTestUsers();
            var handlerMock = MockHttpMessageHandler<User>.SetupBasicGetResourceList(expectedResponse);
            var httpClient = new HttpClient(handlerMock.Object);

            var config = Options.Create(
                new UsersApiOptions
                {
                    Endpoint = Endpoint
                });
            var sut = new UsersService(httpClient, config);

            // Act
            var result = await sut.GetAllUsers();

            // Assert
            result.Should().BeOfType<List<User>>();
        }

        /// <summary>
        /// Get all users when called.
        /// </summary>
        /// <returns>Returns empty list of users.</returns>
        [Fact]
        public async Task GetAllUsers_WhenCalled_ReturnsEmptyListOfUsers()
        {
            // Arrange
            var handlerMock = MockHttpMessageHandler<User>.SetupReturn404();
            var httpClient = new HttpClient(handlerMock.Object);

            var config = Options.Create(
                new UsersApiOptions
                {
                    Endpoint = Endpoint
                });
            var sut = new UsersService(httpClient, config);

            // Act
            var result = await sut.GetAllUsers();

            // Assert
            result.Count.Should().Be(0);
        }

        /// <summary>
        /// Get all users when hits 404.
        /// </summary>
        /// <returns>Returns list of users of expected size.</returns>
        [Fact]
        public async Task GetAllUsers_WhenHits404_ReturnsListOfUsersOfExpectedSize()
        {
            // Arrange
            var expectedResponse = UsersFixture.GetTestUsers();

            var handlerMock = MockHttpMessageHandler<User>.SetupBasicGetResourceList(expectedResponse);
            var httpClient = new HttpClient(handlerMock.Object);

            var config = Options.Create(
                new UsersApiOptions
                {
                    Endpoint = Endpoint
                });
            var sut = new UsersService(httpClient, config);

            // Act
            var result = await sut.GetAllUsers();

            // Assert
            result.Count.Should().Be(expectedResponse.Count);
        }

        /// <summary>
        /// Get all users when hits 404 and invokes configured external url.
        /// </summary>
        /// <returns>Should invokes once HTTP GET configured external url.</returns>
        [Fact]
        public async Task GetAllUsers_WhenHits404_InvokesConfiguredExternalUrl()
        {
            // Arrange
            var expectedResponse = UsersFixture.GetTestUsers();
            var handlerMock = MockHttpMessageHandler<User>.SetupBasicGetResourceList(expectedResponse);
            var httpClient = new HttpClient(handlerMock.Object);

            var config = Options.Create(
                new UsersApiOptions
                {
                    Endpoint = Endpoint
                });

            var sut = new UsersService(httpClient, config);

            // Act
            await sut.GetAllUsers();

            var uri = new Uri(Endpoint);

            // Assert
            handlerMock
                .Protected()
                .Verify(
                    "SendAsync",
                    Times.Exactly(1),
                    ItExpr.Is<HttpRequestMessage>(req =>
                        req.Method == HttpMethod.Get
                        && req.RequestUri == uri),
                    ItExpr.IsAny<CancellationToken>());
        }
    }
}
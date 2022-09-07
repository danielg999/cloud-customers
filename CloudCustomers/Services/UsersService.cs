using CloudCustomers.API.Config;
using CloudCustomers.API.Models;
using Microsoft.Extensions.Options;

namespace CloudCustomers.API.Services
{
    /// <summary>
    /// Interface of UsersService.
    /// </summary>
    public interface IUsersService
    {
        public Task<List<User>> GetAllUsers();
    }

    /// <summary>
    /// Service of users.
    /// </summary>
    public class UsersService : IUsersService
    {
        /// <summary>
        /// Field to inject HttpClient.
        /// </summary>
        private readonly HttpClient _httpClient;

        /// <summary>
        /// Field to inject UsersApiOptions.
        /// </summary>
        private readonly UsersApiOptions _apiConfig;

        /// <summary>
        /// Constructor of UsersService.
        /// </summary>
        /// <param name="httpClient">HTTP client.</param>
        /// <param name="apiConfig">API config.</param>
        public UsersService(HttpClient httpClient, IOptions<UsersApiOptions> apiConfig)
        {
            _httpClient = httpClient;
            _apiConfig = apiConfig.Value;
        }

        /// <summary>
        /// Get all users.
        /// </summary>
        /// <returns>Returns list of users.</returns>
        public async Task<List<User>> GetAllUsers()
        {
            var usersResponse = await _httpClient.GetAsync(_apiConfig.Endpoint);
            if (usersResponse.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return new List<User> { };
            }

            var responseContent = usersResponse.Content;
            var allUsers = await responseContent.ReadFromJsonAsync<List<User>>();
            return allUsers.ToList();
        }
    }
}
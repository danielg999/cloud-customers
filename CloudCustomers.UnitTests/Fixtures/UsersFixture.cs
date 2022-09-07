using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CloudCustomers.API.Models;

namespace CloudCustomers.UnitTests.Fixtures
{
    /// <summary>
    /// Class to deliver data of users to tests.
    /// </summary>
    public static class UsersFixture
    {
        /// <summary>
        /// Get test users.
        /// </summary>
        /// <returns>Returns list of users to tests.</returns>
        public static List<User> GetTestUsers() =>
            new()
            {
                new User
                {
                    Id = 1,
                    Name = "John",
                    Email = "John@xx.com",
                    Address = new Address()
                    {
                        Street = "Głogowska 123",
                        City = "Poznań",
                        ZipCode = "11-222"
                    }
                },
                new User
                {
                    Id = 2,
                    Name = "Maria",
                    Email = "maria@xx.com",
                    Address = new Address()
                    {
                        Street = "Szkolna 10",
                        City = "Poznań",
                        ZipCode = "11-255"
                    }
                },
                new User
                {
                    Id = 3,
                    Name = "Ralph",
                    Email = "ralph@xx.com",
                    Address = new Address()
                    {
                        Street = "Parkowa 1",
                        City = "Warszawa",
                        ZipCode = "16-205"
                    }
                }
            };
    }
}

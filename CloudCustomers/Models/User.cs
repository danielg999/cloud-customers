namespace CloudCustomers.API.Models
{
    public class User
    {
        /// <summary>
        /// The ID.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// The name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The email.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// The address.
        /// </summary>
        public Address Address { get; set; }
    }
}

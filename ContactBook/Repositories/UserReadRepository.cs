using ContactBook.Domain;
using Microsoft.Data.Sqlite;

namespace ContactBook.Repositories
{
    public class UserReadRepository : IUserReadRepository
    {
        private readonly string _connectionString;

        public UserReadRepository()
        {
            _connectionString = "Data Source=AppData/contact-database.db;";
        }

        public UserAddress GetUserAddress(string userId)
        {
            using var connection = new SqliteConnection(_connectionString);
            connection.Open();

            using var command = connection.CreateCommand();
            command.CommandText = @"
        SELECT ua.UserId, abs.State, ad.Id, ad.City, ad.Postcode
        FROM UserAddresses ua
        JOIN AddressByState abs ON ua.AddressByStateId = abs.Id
        JOIN AddressDetails ad ON abs.AddressDetailsId = ad.Id
        WHERE ua.UserId = @UserId";
            command.Parameters.AddWithValue("@UserId", userId);

            using var reader = command.ExecuteReader();

            var userAddress = new UserAddress();
            userAddress.UserId = userId;
            userAddress.AddressByStateDictionary = new Dictionary<string, Address>();

            while (reader.Read())
            {
                var state = reader.GetString(1);
                var address = new Address
                {
                    Id = reader.GetString(2),
                    City = reader.GetString(3),
                    Postcode = reader.GetString(4)
                };

                userAddress.AddressByStateDictionary[state] = address;
            }

            return userAddress;
        }

        public UserContact GetUserContact(string userId)
        {
            throw new NotImplementedException();
        }
    }
}

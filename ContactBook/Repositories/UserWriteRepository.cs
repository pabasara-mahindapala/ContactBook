using ContactBook.Domain;
using Microsoft.Data.Sqlite;

namespace ContactBook.Repositories
{
    public class UserWriteRepository : IUserWriteRepository
    {
        private readonly string _connectionString;

        public UserWriteRepository()
        {
            _connectionString = "Data Source=AppData/contact-database.db;";
        }

        public async Task<User> GetAsync(string userId)
        {
            using var connection = new SqliteConnection(_connectionString);
            await connection.OpenAsync();

            using var command = connection.CreateCommand();
            command.CommandText = "SELECT Id, FirstName, LastName FROM Users WHERE Id = @Id";

            command.Parameters.AddWithValue("@Id", userId);

            using var reader = await command.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                return new User(reader.GetString(0), reader.GetString(1), reader.GetString(2));
            }

            return null;
        }

        public async Task CreateAsync(User user)
        {
            using var connection = new SqliteConnection(_connectionString);
            await connection.OpenAsync();

            using var command = connection.CreateCommand();
            command.CommandText = "INSERT INTO Users (Id, FirstName, LastName) VALUES (@Id, @FirstName, @LastName)";

            command.Parameters.AddWithValue("@Id", user.Id);
            command.Parameters.AddWithValue("@FirstName", user.FirstName);
            command.Parameters.AddWithValue("@LastName", user.LastName);

            await command.ExecuteNonQueryAsync();
        }

        public async Task UpdateAsync(User user)
        {
            using var connection = new SqliteConnection(_connectionString);
            await connection.OpenAsync();

            using var command = connection.CreateCommand();
            command.CommandText = "UPDATE Users SET FirstName = @FirstName, LastName = @LastName WHERE Id = @Id";

            command.Parameters.AddWithValue("@Id", user.Id);
            command.Parameters.AddWithValue("@FirstName", user.FirstName);
            command.Parameters.AddWithValue("@LastName", user.LastName);

            await command.ExecuteNonQueryAsync();
        }

        public async Task DeleteAsync(string userId)
        {
            using var connection = new SqliteConnection(_connectionString);
            await connection.OpenAsync();

            using var command = connection.CreateCommand();
            command.CommandText = "DELETE FROM Users WHERE Id = @Id";

            command.Parameters.AddWithValue("@Id", userId);

            await command.ExecuteNonQueryAsync();
        }

        public async Task<Contact?> GetContactAsync(string contactId)
        {
            using var connection = new SqliteConnection(_connectionString);
            await connection.OpenAsync();

            using var command = connection.CreateCommand();
            command.CommandText = "SELECT Id, Type, Detail, UserId FROM Contacts WHERE Id = @Id";

            command.Parameters.AddWithValue("@Id", contactId);

            using var reader = await command.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                return new Contact
                {
                    Id = reader.GetString(0),
                    Type = reader.GetString(1),
                    Detail = reader.GetString(2),
                    UserId = reader.GetString(3)
                };
            }

            return null;
        }

        public async Task CreateContactAsync(Contact contact)
        {
            using var connection = new SqliteConnection(_connectionString);
            await connection.OpenAsync();

            using var command = connection.CreateCommand();
            command.CommandText = "INSERT INTO Contacts (Id, Type, Detail, UserId) VALUES (@Id, @Type, @Detail, @UserId)";

            command.Parameters.AddWithValue("@Id", contact.Id);
            command.Parameters.AddWithValue("@Type", contact.Type);
            command.Parameters.AddWithValue("@Detail", contact.Detail);
            command.Parameters.AddWithValue("@UserId", contact.UserId);

            await command.ExecuteNonQueryAsync();
        }

        public async Task UpdateContactAsync(Contact contact)
        {
            using var connection = new SqliteConnection(_connectionString);
            await connection.OpenAsync();

            using var command = connection.CreateCommand();
            command.CommandText = "UPDATE Contacts SET Type = @Type, Detail = @Detail, UserId = @UserId WHERE Id = @Id";

            command.Parameters.AddWithValue("@Id", contact.Id);
            command.Parameters.AddWithValue("@Type", contact.Type);
            command.Parameters.AddWithValue("@Detail", contact.Detail);
            command.Parameters.AddWithValue("@UserId", contact.UserId);

            await command.ExecuteNonQueryAsync();
        }

        public async Task DeleteContactAsync(string contactId)
        {
            using var connection = new SqliteConnection(_connectionString);
            await connection.OpenAsync();

            using var command = connection.CreateCommand();
            command.CommandText = "DELETE FROM Contacts WHERE Id = @Id";

            command.Parameters.AddWithValue("@Id", contactId);

            await command.ExecuteNonQueryAsync();
        }

        public async Task<Address?> GetAddressAsync(string addressId)
        {
            using var connection = new SqliteConnection(_connectionString);
            await connection.OpenAsync();

            using var command = connection.CreateCommand();
            command.CommandText = "SELECT Id, City, State, Postcode, UserId FROM Addresses WHERE Id = @Id";

            command.Parameters.AddWithValue("@Id", addressId);

            using var reader = await command.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                return new Address
                {
                    Id = reader.GetString(0),
                    City = reader.GetString(1),
                    State = reader.GetString(2),
                    Postcode = reader.GetString(3),
                    UserId = reader.GetString(4)
                };
            }

            return null;
        }

        public async Task CreateAddressAsync(Address address)
        {
            using var connection = new SqliteConnection(_connectionString);
            await connection.OpenAsync();

            using var command = connection.CreateCommand();
            command.CommandText = "INSERT INTO Addresses (Id, City, State, Postcode, UserId) VALUES (@Id, @City, @State, @Postcode, @UserId)";

            command.Parameters.AddWithValue("@Id", address.Id);
            command.Parameters.AddWithValue("@City", address.City);
            command.Parameters.AddWithValue("@State", address.State);
            command.Parameters.AddWithValue("@Postcode", address.Postcode);
            command.Parameters.AddWithValue("@UserId", address.UserId);

            await command.ExecuteNonQueryAsync();
        }

        public async Task UpdateAddressAsync(Address address)
        {
            using var connection = new SqliteConnection(_connectionString);
            await connection.OpenAsync();

            using var command = connection.CreateCommand();
            command.CommandText = "UPDATE Addresses SET City = @City, State = @State, Postcode = @Postcode, UserId = @UserId WHERE Id = @Id";

            command.Parameters.AddWithValue("@Id", address.Id);
            command.Parameters.AddWithValue("@City", address.City);
            command.Parameters.AddWithValue("@State", address.State);
            command.Parameters.AddWithValue("@Postcode", address.Postcode);
            command.Parameters.AddWithValue("@UserId", address.UserId);

            await command.ExecuteNonQueryAsync();
        }

        public async Task DeleteAddressAsync(string addressId)
        {
            using var connection = new SqliteConnection(_connectionString);
            await connection.OpenAsync();

            using var command = connection.CreateCommand();
            command.CommandText = "DELETE FROM Addresses WHERE Id = @Id";

            command.Parameters.AddWithValue("@Id", addressId);

            await command.ExecuteNonQueryAsync();
        }
    }
}

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

        public User Get(string userId)
        {
            using var connection = new SqliteConnection(_connectionString);
            connection.Open();

            using var command = connection.CreateCommand();
            command.CommandText = "SELECT Id, FirstName, LastName FROM Users WHERE Id = @Id";

            command.Parameters.AddWithValue("@Id", userId);

            using var reader = command.ExecuteReader();
            while (reader.Read())
            {
                return new User(reader.GetString(0), reader.GetString(1), reader.GetString(2));
            }

            return null;
        }

        public void Create(User user)
        {
            using var connection = new SqliteConnection(_connectionString);
            connection.Open();

            using var command = connection.CreateCommand();
            command.CommandText = "INSERT INTO Users (Id, FirstName, LastName) VALUES (@Id, @FirstName, @LastName)";

            command.Parameters.AddWithValue("@Id", user.Id);
            command.Parameters.AddWithValue("@FirstName", user.FirstName);
            command.Parameters.AddWithValue("@LastName", user.LastName);

            command.ExecuteNonQuery();
        }

        public void Update(User user)
        {
            using var connection = new SqliteConnection(_connectionString);
            connection.Open();

            using var command = connection.CreateCommand();
            command.CommandText = "UPDATE Users SET FirstName = @FirstName, LastName = @LastName WHERE Id = @Id";

            command.Parameters.AddWithValue("@Id", user.Id);
            command.Parameters.AddWithValue("@FirstName", user.FirstName);
            command.Parameters.AddWithValue("@LastName", user.LastName);

            command.ExecuteNonQuery();
        }

        public void Delete(string userId)
        {
            using var connection = new SqliteConnection(_connectionString);
            connection.Open();

            using var command = connection.CreateCommand();
            command.CommandText = "DELETE FROM Users WHERE Id = @Id";

            command.Parameters.AddWithValue("@Id", userId);

            command.ExecuteNonQuery();
        }

        public Contact GetContact(string contactId)
        {
            using var connection = new SqliteConnection(_connectionString);
            connection.Open();

            using var command = connection.CreateCommand();
            command.CommandText = "SELECT Id, Type, Detail, UserId FROM Contacts WHERE Id = @Id";

            command.Parameters.AddWithValue("@Id", contactId);

            using var reader = command.ExecuteReader();
            while (reader.Read())
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

        public void CreateContact(Contact contact)
        {
            using var connection = new SqliteConnection(_connectionString);
            connection.Open();

            using var command = connection.CreateCommand();
            command.CommandText = "INSERT INTO Contacts (Id, Type, Detail, UserId) VALUES (@Id, @Type, @Detail, @UserId)";

            command.Parameters.AddWithValue("@Id", contact.Id);
            command.Parameters.AddWithValue("@Type", contact.Type);
            command.Parameters.AddWithValue("@Detail", contact.Detail);
            command.Parameters.AddWithValue("@UserId", contact.UserId);

            command.ExecuteNonQuery();
        }

        public void UpdateContact(Contact contact)
        {
            using var connection = new SqliteConnection(_connectionString);
            connection.Open();

            using var command = connection.CreateCommand();
            command.CommandText = "UPDATE Contacts SET Type = @Type, Detail = @Detail, UserId = @UserId WHERE Id = @Id";

            command.Parameters.AddWithValue("@Id", contact.Id);
            command.Parameters.AddWithValue("@Type", contact.Type);
            command.Parameters.AddWithValue("@Detail", contact.Detail);
            command.Parameters.AddWithValue("@UserId", contact.UserId);

            command.ExecuteNonQuery();
        }

        public void DeleteContact(string contactId)
        {
            using var connection = new SqliteConnection(_connectionString);
            connection.Open();

            using var command = connection.CreateCommand();
            command.CommandText = "DELETE FROM Contacts WHERE Id = @Id";

            command.Parameters.AddWithValue("@Id", contactId);

            command.ExecuteNonQuery();
        }

        public Address GetAddress(string addressId)
        {
            using var connection = new SqliteConnection(_connectionString);
            connection.Open();

            using var command = connection.CreateCommand();
            command.CommandText = "SELECT Id, City, State, Postcode, UserId FROM Addresses WHERE Id = @Id";

            command.Parameters.AddWithValue("@Id", addressId);

            using var reader = command.ExecuteReader();
            while (reader.Read())
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

        public void CreateAddress(Address address)
        {
            using var connection = new SqliteConnection(_connectionString);
            connection.Open();

            using var command = connection.CreateCommand();
            command.CommandText = "INSERT INTO Addresses (Id, City, State, Postcode, UserId) VALUES (@Id, @City, @State, @Postcode, @UserId)";

            command.Parameters.AddWithValue("@Id", address.Id);
            command.Parameters.AddWithValue("@City", address.City);
            command.Parameters.AddWithValue("@State", address.State);
            command.Parameters.AddWithValue("@Postcode", address.Postcode);
            command.Parameters.AddWithValue("@UserId", address.UserId);

            command.ExecuteNonQuery();
        }

        public void UpdateAddress(Address address)
        {
            using var connection = new SqliteConnection(_connectionString);
            connection.Open();

            using var command = connection.CreateCommand();
            command.CommandText = "UPDATE Addresses SET City = @City, State = @State, Postcode = @Postcode, UserId = @UserId WHERE Id = @Id";

            command.Parameters.AddWithValue("@Id", address.Id);
            command.Parameters.AddWithValue("@City", address.City);
            command.Parameters.AddWithValue("@State", address.State);
            command.Parameters.AddWithValue("@Postcode", address.Postcode);
            command.Parameters.AddWithValue("@UserId", address.UserId);

            command.ExecuteNonQuery();
        }

        public void DeleteAddress(string addressId)
        {
            using var connection = new SqliteConnection(_connectionString);
            connection.Open();

            using var command = connection.CreateCommand();
            command.CommandText = "DELETE FROM Addresses WHERE Id = @Id";

            command.Parameters.AddWithValue("@Id", addressId);

            command.ExecuteNonQuery();
        }
    }
}

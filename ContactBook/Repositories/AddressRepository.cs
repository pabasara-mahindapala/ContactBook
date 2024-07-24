using ContactBook.Domain;
using Microsoft.Data.Sqlite;

namespace ContactBook.Repositories
{
    public class AddressRepository : IAddressRepository
    {
        private readonly string _connectionString;

        public AddressRepository()
        {
            _connectionString = "Data Source=AppData/contact-database.db;";
        }

        public void Create(Address address)
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

        public void Delete(string addressId)
        {
            using var connection = new SqliteConnection(_connectionString);
            connection.Open();

            using var command = connection.CreateCommand();
            command.CommandText = "DELETE FROM Addresses WHERE Id = @Id";

            command.Parameters.AddWithValue("@Id", addressId);

            command.ExecuteNonQuery();
        }

        public Address Get(string addressId)
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

        public void Update(Address address)
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
    }
}

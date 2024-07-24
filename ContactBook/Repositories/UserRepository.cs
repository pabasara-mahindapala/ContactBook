using ContactBook.Domain;
using Microsoft.Data.Sqlite;

namespace ContactBook.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly string _connectionString;

        public UserRepository()
        {
            _connectionString = "Data Source=AppData/contact-database.db;";
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

        public void Delete(string userId)
        {
            using var connection = new SqliteConnection(_connectionString);
            connection.Open();

            using var command = connection.CreateCommand();
            command.CommandText = "DELETE FROM Users WHERE Id = @Id";

            command.Parameters.AddWithValue("@Id", userId);

            command.ExecuteNonQuery();
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
                return new User
                {
                    Id = reader.GetString(0),
                    FirstName = reader.GetString(1),
                    LastName = reader.GetString(2)
                };
            }

            return null;
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
    }
}

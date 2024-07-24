using ContactBook.Domain;
using Microsoft.Data.Sqlite;

namespace ContactBook.Repositories
{
    public class ContactRepository : IContactRepository
    {
        private readonly string _connectionString;

        public ContactRepository()
        {
            _connectionString = "Data Source=AppData/contact-database.db;";
        }

        public void Create(Contact contact)
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

        public void Delete(string contactId)
        {
            using var connection = new SqliteConnection(_connectionString);
            connection.Open();

            using var command = connection.CreateCommand();
            command.CommandText = "DELETE FROM Contacts WHERE Id = @Id";

            command.Parameters.AddWithValue("@Id", contactId);

            command.ExecuteNonQuery();
        }

        public Contact Get(string contactId)
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

        public void Update(Contact contact)
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
    }
}

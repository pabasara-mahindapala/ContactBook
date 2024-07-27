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

        public void CreateAddressByState(AddressByState addressByState)
        {
            using var connection = new SqliteConnection(_connectionString);
            connection.Open();

            using var command = connection.CreateCommand();
            command.CommandText = "INSERT INTO AddressByState (Id, State, City, Postcode) VALUES (@Id, @State, @City, @Postcode)";

            command.Parameters.AddWithValue("@Id", addressByState.Id);
            command.Parameters.AddWithValue("@State", addressByState.State);
            command.Parameters.AddWithValue("@City", addressByState.City);
            command.Parameters.AddWithValue("@Postcode", addressByState.Postcode);

            command.ExecuteNonQuery();
        }

        public void CreateContactByType(ContactByType contactByType)
        {
            using var connection = new SqliteConnection(_connectionString);
            connection.Open();

            using var command = connection.CreateCommand();
            command.CommandText = "INSERT INTO ContactByType (Id, Type, Detail) VALUES (@Id, @Type, @Detail)";

            command.Parameters.AddWithValue("@Id", contactByType.Id);
            command.Parameters.AddWithValue("@Type", contactByType.Type);
            command.Parameters.AddWithValue("@Detail", contactByType.Detail);

            command.ExecuteNonQuery();
        }

        public void CreateUserAddress(string userId, string addressByStateId)
        {
            using var connection = new SqliteConnection(_connectionString);
            connection.Open();

            using var command = connection.CreateCommand();
            command.CommandText = "INSERT INTO UserAddresses (UserId, AddressByStateId) VALUES (@UserId, @AddressByStateId)";

            command.Parameters.AddWithValue("@UserId", userId);
            command.Parameters.AddWithValue("@AddressByStateId", addressByStateId);

            command.ExecuteNonQuery();
        }

        public void CreateUserContact(string userId, string contactByTypeId)
        {
            using var connection = new SqliteConnection(_connectionString);
            connection.Open();

            using var command = connection.CreateCommand();
            command.CommandText = "INSERT INTO UserContacts (UserId, ContactByTypeId) VALUES (@UserId, @ContactByTypeId)";

            command.Parameters.AddWithValue("@UserId", userId);
            command.Parameters.AddWithValue("@ContactByTypeId", contactByTypeId);

            command.ExecuteNonQuery();
        }

        public UserAddress GetUserAddress(string userId)
        {
            using var connection = new SqliteConnection(_connectionString);
            connection.Open();

            using var command = connection.CreateCommand();
            command.CommandText = @"
        SELECT ua.UserId, abs.Id, abs.State, abs.City, abs.Postcode
        FROM UserAddresses ua
        JOIN AddressByState abs ON ua.AddressByStateId = abs.Id
        WHERE ua.UserId = @UserId";
            command.Parameters.AddWithValue("@UserId", userId);

            using var reader = command.ExecuteReader();

            var userAddress = new UserAddress();
            userAddress.UserId = userId;
            userAddress.AddressByStateDictionary = new Dictionary<string, AddressByState>();

            while (reader.Read())
            {
                var state = reader.GetString(2);
                var address = new AddressByState
                {
                    State = state,
                    Id = reader.GetString(1),
                    City = reader.GetString(3),
                    Postcode = reader.GetString(4)
                };

                userAddress.AddressByStateDictionary[state] = address;
            }

            return userAddress;
        }

        public UserContact GetUserContact(string userId)
        {
            using var connection = new SqliteConnection(_connectionString);
            connection.Open();

            using var command = connection.CreateCommand();
            command.CommandText = @"
        SELECT uc.UserId, cbt.Id, cbt.Type, cbt.Detail
        FROM UserContacts uc
        JOIN ContactByType cbt ON uc.ContactByTypeId = cbt.Id
        WHERE uc.UserId = @UserId";
            command.Parameters.AddWithValue("@UserId", userId);

            using var reader = command.ExecuteReader();

            var userContact = new UserContact();
            userContact.UserId = userId;
            userContact.ContactByTypeDictionary = new Dictionary<string, ContactByType>();

            while (reader.Read())
            {
                var type = reader.GetString(2);
                var contact = new ContactByType
                {
                    Type = type,
                    Id = reader.GetString(1),
                    Detail = reader.GetString(3)
                };

                userContact.ContactByTypeDictionary[type] = contact;
            }

            return userContact;
        }

        public void UpdateAddressByState(AddressByState addressByState)
        {
            using var connection = new SqliteConnection(_connectionString);
            connection.Open();

            using var command = connection.CreateCommand();
            command.CommandText = "UPDATE AddressByState SET State = @State, City = @City, Postcode = @Postcode WHERE Id = @Id";

            command.Parameters.AddWithValue("@Id", addressByState.Id);
            command.Parameters.AddWithValue("@State", addressByState.State);
            command.Parameters.AddWithValue("@City", addressByState.City);
            command.Parameters.AddWithValue("@Postcode", addressByState.Postcode);

            command.ExecuteNonQuery();
        }

        public void UpdateContactByType(ContactByType contactByType)
        {
            using var connection = new SqliteConnection(_connectionString);
            connection.Open();

            using var command = connection.CreateCommand();
            command.CommandText = "UPDATE ContactByType SET Type = @Type, Detail = @Detail WHERE Id = @Id";

            command.Parameters.AddWithValue("@Id", contactByType.Id);
            command.Parameters.AddWithValue("@Type", contactByType.Type);
            command.Parameters.AddWithValue("@Detail", contactByType.Detail);

            command.ExecuteNonQuery();
        }
    }
}

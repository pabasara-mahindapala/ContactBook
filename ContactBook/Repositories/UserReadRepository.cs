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

        public async Task CreateAddressByStateAsync(AddressByState addressByState)
        {
            using var connection = new SqliteConnection(_connectionString);
            await connection.OpenAsync();

            using var command = connection.CreateCommand();
            command.CommandText = "INSERT INTO AddressByState (Id, State, City, Postcode) VALUES (@Id, @State, @City, @Postcode)";

            command.Parameters.AddWithValue("@Id", addressByState.Id);
            command.Parameters.AddWithValue("@State", addressByState.State);
            command.Parameters.AddWithValue("@City", addressByState.City);
            command.Parameters.AddWithValue("@Postcode", addressByState.Postcode);

            await command.ExecuteNonQueryAsync();
        }

        public async Task CreateContactByTypeAsync(ContactByType contactByType)
        {
            using var connection = new SqliteConnection(_connectionString);
            await connection.OpenAsync();

            using var command = connection.CreateCommand();
            command.CommandText = "INSERT INTO ContactByType (Id, Type, Detail) VALUES (@Id, @Type, @Detail)";

            command.Parameters.AddWithValue("@Id", contactByType.Id);
            command.Parameters.AddWithValue("@Type", contactByType.Type);
            command.Parameters.AddWithValue("@Detail", contactByType.Detail);

            await command.ExecuteNonQueryAsync();
        }

        public async Task CreateUserAddressAsync(string userId, string addressByStateId)
        {
            using var connection = new SqliteConnection(_connectionString);
            await connection.OpenAsync();

            using var command = connection.CreateCommand();
            command.CommandText = "INSERT INTO UserAddresses (UserId, AddressByStateId) VALUES (@UserId, @AddressByStateId)";

            command.Parameters.AddWithValue("@UserId", userId);
            command.Parameters.AddWithValue("@AddressByStateId", addressByStateId);

            await command.ExecuteNonQueryAsync();
        }

        public async Task CreateUserContactAsync(string userId, string contactByTypeId)
        {
            using var connection = new SqliteConnection(_connectionString);
            await connection.OpenAsync();

            using var command = connection.CreateCommand();
            command.CommandText = "INSERT INTO UserContacts (UserId, ContactByTypeId) VALUES (@UserId, @ContactByTypeId)";

            command.Parameters.AddWithValue("@UserId", userId);
            command.Parameters.AddWithValue("@ContactByTypeId", contactByTypeId);

            await command.ExecuteNonQueryAsync();
        }

        public async Task<UserAddress> GetUserAddressAsync(string userId)
        {
            using var connection = new SqliteConnection(_connectionString);
            await connection.OpenAsync();

            using var command = connection.CreateCommand();
            command.CommandText = @"
        SELECT ua.UserId, abs.Id, abs.State, abs.City, abs.Postcode
        FROM UserAddresses ua
        JOIN AddressByState abs ON ua.AddressByStateId = abs.Id
        WHERE ua.UserId = @UserId";
            command.Parameters.AddWithValue("@UserId", userId);

            using var reader = await command.ExecuteReaderAsync();

            var userAddress = new UserAddress();
            userAddress.UserId = userId;
            userAddress.AddressByStateDictionary = new Dictionary<string, AddressByState>();

            while (await reader.ReadAsync())
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

        public async Task<UserContact> GetUserContactAsync(string userId)
        {
            using var connection = new SqliteConnection(_connectionString);
            await connection.OpenAsync();

            using var command = connection.CreateCommand();
            command.CommandText = @"
        SELECT uc.UserId, cbt.Id, cbt.Type, cbt.Detail
        FROM UserContacts uc
        JOIN ContactByType cbt ON uc.ContactByTypeId = cbt.Id
        WHERE uc.UserId = @UserId";
            command.Parameters.AddWithValue("@UserId", userId);

            using var reader = await command.ExecuteReaderAsync();

            var userContact = new UserContact();
            userContact.UserId = userId;
            userContact.ContactByTypeDictionary = new Dictionary<string, ContactByType>();

            while (await reader.ReadAsync())
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

        public async Task UpdateAddressByStateAsync(AddressByState addressByState)
        {
            using var connection = new SqliteConnection(_connectionString);
            await connection.OpenAsync();

            using var command = connection.CreateCommand();
            command.CommandText = "UPDATE AddressByState SET State = @State, City = @City, Postcode = @Postcode WHERE Id = @Id";

            command.Parameters.AddWithValue("@Id", addressByState.Id);
            command.Parameters.AddWithValue("@State", addressByState.State);
            command.Parameters.AddWithValue("@City", addressByState.City);
            command.Parameters.AddWithValue("@Postcode", addressByState.Postcode);

            await command.ExecuteNonQueryAsync();
        }

        public async Task UpdateContactByTypeAsync(ContactByType contactByType)
        {
            using var connection = new SqliteConnection(_connectionString);
            await connection.OpenAsync();

            using var command = connection.CreateCommand();
            command.CommandText = "UPDATE ContactByType SET Type = @Type, Detail = @Detail WHERE Id = @Id";

            command.Parameters.AddWithValue("@Id", contactByType.Id);
            command.Parameters.AddWithValue("@Type", contactByType.Type);
            command.Parameters.AddWithValue("@Detail", contactByType.Detail);

            await command.ExecuteNonQueryAsync();
        }
    }
}

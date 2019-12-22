using BasicLogin.Domain;
using BasicLogin.Exceptions;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace BasicLogin.Storage
{
    public class SQLServerUsersRepository : IUsersRepository
    {
        private readonly string _connectionString;

        public SQLServerUsersRepository(string connectionString)
        {
            if (string.IsNullOrWhiteSpace(connectionString))
                throw new ArgumentNullException(nameof(connectionString));

            _connectionString = connectionString;
        }

        public async Task SetPassword(int userId, byte[] newPasswordHash, byte[] newSalt)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                await connection.ExecuteAsync(
                    "UPDATE Users.Users SET PasswordHash = @newPasswordHash, PasswordSalt = @newSalt WHERE UserId = @userId",
                    new { userId, newPasswordHash, newSalt });
            }
        }

        public async Task Create(User user)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                try
                {
                    await connection.ExecuteAsync(
                        "INSERT INTO Users.Users (UserTypeId, UserStateId, Username, PasswordHash, PasswordSalt, FirstName, LastName, RegistrationDate)" +
                        "VALUES (@UserTypeId, @UserStateId, @Username, @PasswordHash, @PasswordSalt, @FirstName, @LastName, @RegistrationDate)",
                        user);
                }
                catch (SqlException ex) when (ex.Message.Contains("UK_Users_Users_Username"))
                {
                    throw new StorageConstraintException($"A user with the username: '{user.Username}' already exists");
                }
                catch (SqlException ex) when (ex.Message.Contains("UK_Users_Users_FirstLastName"))
                {
                    throw new StorageConstraintException($"A user with the name: '{user.FirstName}' and surname: '{user.LastName}' already exists");
                }
            }
        }

        public async Task SetState(int userId, UserStates state)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                await connection.ExecuteAsync(
                    "UPDATE Users.Users SET UserStateId = @stateId WHERE UserId = @userId",
                    new { userId, stateId = (byte)state });
            }
        }

        public async Task<User> Load(string username)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                return await connection.QueryFirstOrDefaultAsync<User>(
                    "SELECT * FROM Users.Users WHERE username = @username",
                    new { username });
            }
        }

        public async Task<IEnumerable<User>> GetAll()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                return await connection.QueryAsync<User>(
                    "SELECT * FROM Users.Users");
            }
        }
    }
}

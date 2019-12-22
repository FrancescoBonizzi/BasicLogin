using BasicLogin.Domain;
using BasicLogin.Exceptions;
using BasicLogin.Security;
using BasicLogin.Storage;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BasicLogin
{
    public class Users
    {
        private readonly IUsersRepository _usersRepository;
        private readonly IPasswordHasher _hasher;

        public Users(
            IUsersRepository usersRepository,
            IPasswordHasher hasher)
        {
            _usersRepository = usersRepository;
            _hasher = hasher;
        }

        public async Task CreateUser(
            string username,
            string plainTextPassword,
            string firstName,
            string lastName,
            UserTypes userType)
        {
            if (string.IsNullOrWhiteSpace(username))
                throw new EmptyFieldException(nameof(username));
            if (string.IsNullOrWhiteSpace(plainTextPassword))
                throw new EmptyFieldException(nameof(plainTextPassword));
            if (string.IsNullOrWhiteSpace(firstName))
                throw new EmptyFieldException(nameof(firstName));
            if (string.IsNullOrWhiteSpace(lastName))
                throw new EmptyFieldException(nameof(lastName));

            var salt = _hasher.GenerateSalt();
            var hashedPassword = _hasher.Hash(plainTextPassword, salt);

            await _usersRepository.Create(new User()
            {
                FirstName = firstName,
                LastName = lastName,
                PasswordHash = hashedPassword,
                PasswordSalt = salt,
                Username = username,
                UserStateId = UserStates.Active,
                UserTypeId = userType,
                RegistrationDate = DateTimeOffset.Now
            });
        }

        public async Task<User> Load(string username)
        {
            if (string.IsNullOrWhiteSpace(username))
                throw new EmptyFieldException(nameof(username));

            return await _usersRepository.Load(username);
        }

        public async Task SetPassword(int userId, string newPlainTextPassword)
        {
            if (string.IsNullOrWhiteSpace(newPlainTextPassword))
                throw new EmptyFieldException(nameof(newPlainTextPassword));

            var salt = _hasher.GenerateSalt();
            var hashedPassword = _hasher.Hash(newPlainTextPassword, salt);

            await _usersRepository.SetPassword(userId, hashedPassword, salt);
        }

        public async Task<User> Login(string username, string password)
        {
            var user = await Load(username);
            if (user == null)
                throw new AuthenticationFailedException();

            if (user.UserStateId == UserStates.Disabled)
                throw new AuthenticationFailedException();

            var inputPasswordHash = _hasher.Hash(password, user.PasswordSalt);
            if (!_hasher.AreEquals(user.PasswordHash, inputPasswordHash))
                throw new AuthenticationFailedException();

            return user;
        }

        public async Task SetState(int userId, UserStates state)
            => await _usersRepository.SetState(userId, state);

        public async Task<IEnumerable<User>> GetAll()
            => await _usersRepository.GetAll();
    }
}

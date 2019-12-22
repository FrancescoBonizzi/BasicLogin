using System;

namespace BasicLogin.Domain
{
    public class User
    {
        public int UserId { get; set; }
        public UserTypes UserTypeId { get; set; }
        public UserStates UserStateId { get; set; }
        public string Username { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTimeOffset RegistrationDate { get; set; }
    }
}

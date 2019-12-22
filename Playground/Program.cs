using BasicLogin;
using BasicLogin.Domain;
using BasicLogin.Security;
using BasicLogin.Storage;
using System;
using System.Threading.Tasks;

namespace Playground
{
    internal class Program
    {
        private static async Task Main(string[] args)
        {
            var cs = "Data Source=localhost\\SQLExpress; Integrated Security=SSPI; Initial Catalog=PLAYGROUND;";
            var usersRepository = new SQLServerUsersRepository(cs);
            var hasher = new Pbkdf2PasswordHasher();
            var users = new Users(usersRepository, hasher);

            try
            {
                await users.CreateUser(
                    username: "admin", 
                    plainTextPassword: "someVeryNiceRandomPassword", 
                    firstName: "admin's name", 
                    lastName: "admin's surname", 
                    userType: UserTypes.Administrator);

                await users.CreateUser(
                    username: "user", 
                    plainTextPassword: "someVeryNiceRandomPasswordDifferentFromTheOthers", 
                    firstName: "user's name", 
                    lastName: "user's surname", 
                    userType: UserTypes.SimpleUser);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            Console.WriteLine("done");
            Console.Read();
        }
    }
}

using BasicLogin.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BasicLogin.Storage
{
    public interface IUsersRepository
    {
        /// <summary>
        /// Creates a user
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        Task Create(User user);

        /// <summary>
        /// Loads a user informations
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        Task<User> Load(string username);

        /// <summary>
        /// Sets the password of a user and its salt
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="newPasswordHash"></param>
        /// <param name="newSalt"></param>
        /// <returns></returns>
        Task SetPassword(int userId, byte[] newPasswordHash, byte[] newSalt);

        /// <summary>
        /// Changes the user state
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="state"></param>
        /// <returns></returns>
        Task SetState(int userId, UserStates state);

        /// <summary>
        /// Downloads every user in the repository
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<User>> GetAll();
    }
}

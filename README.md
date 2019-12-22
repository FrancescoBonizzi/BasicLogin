# BasicLogin
**BasicLogin** is a very simple example of how to implement authentication for example a WebSite with MS SQL Server and a PBKDF2 password hasher.

`BasicLogin` exposes a `Users` class which permits to:
- Create a user
- Load a user
- Set the user's password
- "Login" a user which means get its stored password, compute the hash with the given plain text password and compare them
- Get all users

`BasicLogin` has these depencies:
- `IUsersRepository`: the service that communicates with the users storage. It could be a file, another type of database. Here I coded the `SQLServerUsersRepository` for you based on `Microsoft SQL Server` and `Dapper`.
- `IPasswordHasher`: an abstraction to hash a plain text string with a salt. Here I implemented for you `Pbkdf2PasswordHasher`.

You should intend this library as a draft that can be modified to suit your needs. Of course you won't use these simple user states or user types, this is just a very basic example.

# Usage example:

```c#
var cs = "Data Source=localhost\\SQLExpress; Integrated Security=SSPI; Initial Catalog=PLAYGROUND;";
var usersRepository = new SQLServerUsersRepository(cs);
var hasher = new Pbkdf2PasswordHasher();
var users = new Users(usersRepository, hasher);

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

// If the login fails, an AuthenticationFailedException is thrown
var loggedUser = users.Login("user", "wrongPassword");
```

# Building
Simply clone this repository and build the BasicLogin.sln solution. Then, create a `SQL Server` database and run the `TSQL` script in the `Database` folder.

# How to contribute
- Report any issues
- Implement some new `IUsersRepository`
- Improve the current `IPasswordHasher`
- Propose new features / improvements
- Just telling your opinion :-)
- [Offer me an espresso!](https://www.paypal.com/cgi-bin/webscr?cmd=_donations&business=DTT7P8N3TV7N6&currency_code=EUR&source=url) ;-)
using System;

namespace BasicLogin.Exceptions
{
    public class AuthenticationFailedException : Exception
    {
        public AuthenticationFailedException()
            : base("Authentication failed") { }
    }
}

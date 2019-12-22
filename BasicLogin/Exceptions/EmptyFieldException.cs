using System;

namespace BasicLogin.Exceptions
{
    public class EmptyFieldException : Exception
    {
        public EmptyFieldException(string field)
            : base($"The field {field} cannot be empty") { }
    }
}

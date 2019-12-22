using System;

namespace BasicLogin.Exceptions
{
    public class StorageConstraintException : Exception
    {
        public StorageConstraintException(string message)
            : base(message) { }
    }
}

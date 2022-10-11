using System;

namespace Services.Exceptions
{
    public class NoUserFoundException : Exception
    {
        public NoUserFoundException() : base("No user found for the given parameters") { }
    }
}

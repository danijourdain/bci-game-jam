using System;

public class InvalidArraySizeException : Exception
{
    public InvalidArraySizeException(string message) : base(message)
    {
    }
}

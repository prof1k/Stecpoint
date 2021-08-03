namespace SIS.First.Infrastructure.Exceptions
{
    using System;

    /// <summary>
    /// Исключения связанные с бизнес-логикой.
    /// </summary>
    public class BusinessException : Exception
    {
        public BusinessException()
        {
        }

        public BusinessException(string? message) : base(message)
        {
        }

        public BusinessException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}
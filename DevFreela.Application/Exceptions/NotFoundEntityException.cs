using System;

namespace DevFreela.Application.Exceptions
{
    [Serializable]
    public class NotFoundEntityException : Exception
    {
        public NotFoundEntityException(string message) : base(message)
        {
        }
    }
}

using FluentValidation.Results;
using System;
using System.Collections.Generic;

namespace DevFreela.Application.Exceptions
{
    [Serializable]
    public class BadRequestException : Exception
    {
        public List<ValidationFailure> Errors { get; set; }
        public BadRequestException(string message) : base(message)
        {
            Errors = new List<ValidationFailure>();
        }

        public BadRequestException(string message, List<ValidationFailure> validationsFailures) : base(message)
        {
            Errors = validationsFailures ?? new List<ValidationFailure>();
        }
    }
}

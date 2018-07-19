using System;
using System.Collections.Generic;

namespace Negocios.Utilities.Validation.Error
{
    public class InvalidStateException : Exception
    {
        private readonly List<string> _errors;

        public InvalidStateException(List<string> errors)
        {
            _errors = errors;
        }

        public List<string> GetErrors()
        {
            return _errors;
        }

        public override string Message => string.Join("\n -", _errors);
    }
}

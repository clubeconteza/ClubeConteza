using System;
using System.Text.RegularExpressions;

namespace Negocios.Utilities.Format
{
    public class BaseFormatter : IFormatter
    {
        protected readonly string formatted;
        protected readonly string formattedReplacement;
        protected readonly string unformatted;
        protected readonly string unformattedReplacement;

        public BaseFormatter(string formatted, string formattedReplacement, string unformatted, string unformattedReplacement)
        {
            this.formatted = formatted;
            this.formattedReplacement = formattedReplacement;
            this.unformatted = unformatted;
            this.unformattedReplacement = unformattedReplacement;
        }

        public string Format(string value)
        {
            if (value == null)
            {
                throw new ArgumentNullException("Valor não pode ser nulo.");
            }
            return new Regex(unformatted).Replace(value, formattedReplacement);
        }

        public string Unformat(string value)
        {
            if (value == null)
            {
                throw new ArgumentNullException("Valor não pode ser nulo.");
            }

            if (new Regex(unformatted).IsMatch(value))
            {
                return value;
            }
            return new Regex(formatted).Replace(value, unformattedReplacement);
        }

        public bool IsFormatted(string value)
        {
            return new Regex(formatted).IsMatch(value);
        }

        public bool CanBeFormatted(string value)
        {
            return new Regex(unformatted).IsMatch(value);
        }
    }
}

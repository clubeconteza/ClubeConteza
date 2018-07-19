using System;

namespace Negocios.Utilities.Format
{
    public interface IFormatter
    {
        string Format(String value);
        string Unformat(String value);
        bool IsFormatted(String value);
        bool CanBeFormatted(String value);
    }
}

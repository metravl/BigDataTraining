using System;

namespace CsvProcessor.CsvParsing
{
    /// <summary>
    /// An exception indicating that there is an error while parsing a CSV file.
    /// </summary>
    internal class CsvParsingException : Exception
    {
        public CsvParsingException(string message)
            : base(message)
        {
        }

        public CsvParsingException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}

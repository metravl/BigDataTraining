using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Microsoft.VisualBasic.FileIO;

namespace CsvProcessor.CsvParsing
{
    /// <summary>
    /// A generic CSV file iterator.
    /// Specifics to the parsing behavior is added via specifying the output model and the CSV line parser.
    /// </summary>
    /// <typeparam name="TContentLineModel">
    /// The type of the model that is a result of CSV content line parsing.
    /// </typeparam>
    internal class CsvFile<TContentLineModel> : IEnumerable<TContentLineModel>, IDisposable
    {
        /// <summary>
        /// A built-in type for parsing CSV lines. It hides complexity of parsing ","-delimited cells and quotes for cells containing ",".
        /// </summary>
        private readonly TextFieldParser _textFieldParser;

        private readonly StreamReader _streamReader;
        private readonly ICsvLineParser<TContentLineModel> _csvLineParser;
        private bool _isDisposed;
        
        public CsvFile(string csvFilePath, ICsvLineParser<TContentLineModel> csvLineParser)
        {
            if (csvFilePath == null)
            {
                throw new ArgumentNullException(nameof(csvFilePath));
            }
            if (string.IsNullOrWhiteSpace(csvFilePath))
            {
                throw new ArgumentException($"{nameof(csvFilePath)} cannot be empty", nameof(csvFilePath));
            }
            if (csvLineParser == null)
            {
                throw new ArgumentNullException(nameof(csvLineParser));
            }

            _streamReader = new StreamReader(csvFilePath);
            _csvLineParser = csvLineParser;

            _textFieldParser = new TextFieldParser(_streamReader)
            {
                HasFieldsEnclosedInQuotes = true
            };
            _textFieldParser.SetDelimiters(",");
        }

        /// <inheritdoc />
        public IEnumerator<TContentLineModel> GetEnumerator()
        {
            if (_textFieldParser.EndOfData)
            {
                yield break;
            }

            string[] lineParts = _textFieldParser.ReadFields();
            _csvLineParser.LoadHeader(lineParts);

            while (!_textFieldParser.EndOfData)
            {
                lineParts = _textFieldParser.ReadFields();
                TContentLineModel csvLineObject = _csvLineParser.Parse(lineParts);

                yield return csvLineObject;
            }
        }

        /// <inheritdoc />
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        /// <inheritdoc />
        public void Dispose()
        {
            if (_isDisposed)
            {
                return;
            }

            _textFieldParser.Close();
            _streamReader.Dispose();
            _isDisposed = true;
        }
    }
}

namespace CsvProcessor.CsvParsing
{
    /// <summary>
    /// Parser of CSV lines.
    /// </summary>
    /// <typeparam name="TOutputModel">
    /// The type of the model that is a result of CSV content line parsing.
    /// </typeparam>
    internal interface ICsvLineParser<out TOutputModel>
    {
        /// <summary>
        /// Loads CSV header columns. The parser associates column names with their indexes to use them further for content parsing.
        /// </summary>
        /// <param name="headerLineParts">CSV header line parts (names of columns read from the file).</param>
        void LoadHeader(string[] headerLineParts);

        /// <summary>
        /// Parses the CSV content line.
        /// </summary>
        /// <param name="contentLineParts">CSV content line parts.</param>
        /// <returns>The model object as a result of content line parsing.</returns>
        TOutputModel Parse(string[] contentLineParts);
    }
}

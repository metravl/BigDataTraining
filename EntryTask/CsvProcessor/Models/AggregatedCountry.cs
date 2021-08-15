namespace CsvProcessor.Models
{
    /// <summary>
    /// A result of product country aggregation.
    /// </summary>
    internal class AggregatedCountry
    {
        public string OriginCountry { get; set; }

        public decimal AveragePrice { get; set; }

        public decimal RatingFivePercentage { get; set; }
    }
}

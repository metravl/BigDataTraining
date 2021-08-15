namespace CsvProcessor.Models
{
    /// <summary>
    /// The model type representing the CSV line content object.
    /// </summary>
    internal class Product
    {
        public string OriginCountry { get; set; }

        public decimal Price { get; set; }

        public int RatingCount { get; set; }

        public int RatingFiveCount { get; set; }
    }
}

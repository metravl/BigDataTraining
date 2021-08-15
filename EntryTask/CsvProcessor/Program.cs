using System;
using System.IO;
using System.Collections.Generic;
using CsvProcessor.Models;
using CsvProcessor.CsvParsing;
using CsvProcessor.Aggregation;

namespace CsvProcessor
{
    /// <summary>
    /// Program's entry point.
    /// </summary>
    internal class Program
    {
        public static void Main(string[] args)
        {
            try
            {
                string currentDirectoryPath = Path.GetDirectoryName(Environment.GetCommandLineArgs()[0]);

                string csvFilePath = Path.Combine(
                    currentDirectoryPath!,
                    // ReSharper disable once StringLiteralTypo
                    "InputData\\test-task_dataset_summer_products.csv");
                ProcessCsv(csvFilePath);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            Console.WriteLine("Press Enter to exit...");
            Console.ReadLine();
        }

        private static void ProcessCsv(string csvFilePath)
        {
            var productCountryAggregator = new ProductCountryAggregator();
            using (var csvFile = new CsvFile<Product>(csvFilePath, new ProductCsvLineParser()))
            {
                foreach (Product product in csvFile)
                {
                    productCountryAggregator.Aggregate(product);
                }
            }

            IReadOnlyCollection<AggregatedCountry> aggregatedCountries = productCountryAggregator.Finish();
            Console.WriteLine("Origin Country\tAverage Price\tRating Five Percentage");
            foreach (AggregatedCountry country in aggregatedCountries)
            {
                Console.WriteLine($"{country.OriginCountry}\t{country.AveragePrice}\t{country.RatingFivePercentage}");
            }
        }
    }
}

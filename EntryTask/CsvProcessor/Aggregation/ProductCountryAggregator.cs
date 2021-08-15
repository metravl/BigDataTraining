using System;
using System.Collections.Generic;
using System.Linq;
using CsvProcessor.Models;

namespace CsvProcessor.Aggregation
{
    /// <summary>
    /// Performs aggregation of product countries by calculating the average price of products and percentage of five star ratings among all ratings.
    /// </summary>
    internal class ProductCountryAggregator : IAggregator<Product, IReadOnlyCollection<AggregatedCountry>>
    {
        /// <summary>
        /// Interim data structure that is used to store country's cumulative properties.
        /// </summary>
        private class CountryInfo
        {
            public decimal SumOfPrices { get; set; }

            public long SumOfRatingCount { get; set; }

            public long SumOfRatingFiveCount { get; set; }

            public long NumberOfProducts { get; set; }
        }

        private readonly Dictionary<string, CountryInfo> _countriesMap = new Dictionary<string, CountryInfo>();
        
        /// <inheritdoc />
        public void Aggregate(Product product)
        {
            if (product == null)
            {
                throw new ArgumentNullException(nameof(product));
            }

            if (string.IsNullOrWhiteSpace(product.OriginCountry))
            {
                return;
            }

            CountryInfo currentCountry;
            if (_countriesMap.ContainsKey(product.OriginCountry))
            {
                currentCountry = _countriesMap[product.OriginCountry];
            }
            else
            {
                currentCountry = new CountryInfo();
                _countriesMap.Add(product.OriginCountry, currentCountry);
            }

            currentCountry.SumOfPrices += product.Price;
            currentCountry.SumOfRatingCount += product.RatingCount;
            currentCountry.SumOfRatingFiveCount += product.RatingFiveCount;
            currentCountry.NumberOfProducts++;
        }

        /// <inheritdoc />
        public IReadOnlyCollection<AggregatedCountry> Finish()
        {
            var aggregatedCountries = new List<AggregatedCountry>();

            foreach (string orderedCountryName in _countriesMap.Keys.OrderBy(k => k))
            {
                CountryInfo currentCountry = _countriesMap[orderedCountryName];
                aggregatedCountries.Add(new AggregatedCountry
                {
                    OriginCountry = orderedCountryName,
                    AveragePrice = currentCountry.SumOfPrices / currentCountry.NumberOfProducts,
                    RatingFivePercentage = currentCountry.SumOfRatingCount != 0
                        ? ((decimal)currentCountry.SumOfRatingFiveCount / currentCountry.SumOfRatingCount) * 100
                        : 0
                });
            }

            return aggregatedCountries;
        }
    }
}

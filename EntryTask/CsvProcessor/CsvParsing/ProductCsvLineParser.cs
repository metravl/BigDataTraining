using System;
using System.Globalization;
using CsvProcessor.Models;

namespace CsvProcessor.CsvParsing
{
    /// <summary>
    /// Parser of CSV lines containing product properties.
    /// </summary>
    internal class ProductCsvLineParser : ICsvLineParser<Product>
    {
        private int? _originCountryIndex;
        private int? _priceIndex;
        private int? _ratingCountIndex;
        private int? _ratingFiveCountIndex;

        /// <inheritdoc />
        public void LoadHeader(string[] headerLineParts)
        {
            if (headerLineParts == null)
            {
                throw new ArgumentNullException(nameof(headerLineParts));
            }
            
            for (var i = 0; i < headerLineParts.Length; i++)
            {           
                switch (headerLineParts[i])
                {
                    case "origin_country":
                        _originCountryIndex = i;
                        break;

                    case "price":
                        _priceIndex = i;
                        break;

                    case "rating_count":
                        _ratingCountIndex = i;
                        break;

                    case "rating_five_count":
                        _ratingFiveCountIndex = i;
                        break;
                }
            }

            if (_originCountryIndex == null
                || _priceIndex == null
                || _ratingCountIndex == null
                || _ratingFiveCountIndex == null)
            {
                throw new CsvParsingException("One or more columns are not found in CSV");
            }
        }

        /// <inheritdoc />
        public Product Parse(string[] contentLineParts)
        {
            if (contentLineParts == null)
            {
                throw new ArgumentNullException(nameof(contentLineParts));
            }

            if (_originCountryIndex == null
                || _priceIndex == null
                || _ratingCountIndex == null
                || _ratingFiveCountIndex == null)
            {
                throw new InvalidOperationException($"{nameof(LoadHeader)} should be called before {nameof(Parse)}");
            }

            try
            {
                var product = new Product
                {
                    OriginCountry = contentLineParts[_originCountryIndex.Value],
                    Price = !string.IsNullOrWhiteSpace(contentLineParts[_priceIndex.Value])
                        ? Convert.ToDecimal(contentLineParts[_priceIndex.Value], CultureInfo.InvariantCulture)
                        : 0,
                    RatingCount = !string.IsNullOrWhiteSpace(contentLineParts[_ratingCountIndex.Value])
                        ? Convert.ToInt32(contentLineParts[_ratingCountIndex.Value])
                        : 0,
                    RatingFiveCount = !string.IsNullOrWhiteSpace(contentLineParts[_ratingFiveCountIndex.Value])
                        ? Convert.ToInt32(contentLineParts[_ratingFiveCountIndex.Value])
                        : 0
                };

                return product;
            }
            catch (Exception ex)
            {
                throw new CsvParsingException($"Error while parsing line: {string.Join(",", contentLineParts)}", ex);
            }
        }
    }
}

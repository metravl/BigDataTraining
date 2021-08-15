namespace CsvProcessor.Aggregation
{
    /// <summary>
    /// Provides operations to perform data aggregation.
    /// </summary>
    /// <typeparam name="TInputModel">The type of the input model that is used for aggregation.</typeparam>
    /// <typeparam name="TOutputModel">The type of the output model that is a result of aggregation.</typeparam>
    internal interface IAggregator<in TInputModel, out TOutputModel>
    {
        /// <summary>
        /// Aggregates the input model object.
        /// It is expected that this method is called as many times as we have objects for aggregation.
        /// </summary>
        /// <param name="input">The input object.</param>
        void Aggregate(TInputModel input);

        /// <summary>
        /// Finishes aggregation and returns the result.
        /// </summary>
        /// <returns>The result of aggregation.</returns>
        TOutputModel Finish();
    }
}

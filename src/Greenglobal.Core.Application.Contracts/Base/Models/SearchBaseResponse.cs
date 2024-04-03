namespace Greenglobal.Core.Models
{
    /// <summary>
    /// Search Model Request
    /// </summary>
    public class SearchBaseResponse
    {
        /// <summary>
        /// Keyword
        /// Example: code, name, ...
        /// </summary>
        public string? Keyword { get; set; }
    }
}

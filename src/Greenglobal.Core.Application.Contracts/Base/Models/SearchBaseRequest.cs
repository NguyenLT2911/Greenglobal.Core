namespace Greenglobal.Core.Models
{
    /// <summary>
    /// Search Model Request
    /// </summary>
    public class SearchBaseRequest
    {
        /// <summary>
        /// Keyword
        /// Example: code, name, ...
        /// </summary>
        public string? Keyword { get; set; }

        /// <summary>
        /// Status
        /// 0: Locked, 1 Using, -1 Deleted
        /// </summary>
        public int? Status { get; set; }
    }
}

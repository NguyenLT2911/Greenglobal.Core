using System.Text.Json.Serialization;

namespace Greenglobal.Core.Models
{
    /// <summary>
    /// Paging Model Request
    /// </summary>
    public class PageBaseRequest
    {
        /// <summary>
        /// Number of page
        /// </summary>
        [JsonPropertyName("page_number")]
        public int PageNumber { get; set; }

        /// <summary>
        /// Size of page
        /// </summary>
        [JsonPropertyName("page_size")]
        public int PageSize { get; set; } = int.MaxValue;
    }
}

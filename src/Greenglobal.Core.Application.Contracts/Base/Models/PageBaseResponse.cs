using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Greenglobal.Core.Models
{
    /// <summary>
    /// Paging Model Response
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class PageBaseResponse<T>
    {

        /// <summary>
        /// Status code, default StatusCode200
        /// </summary>
        public int Status { get; set; } = 200;

        /// <summary>
        /// Message
        /// </summary>
        public string? Message { get; set; }

        /// <summary>
        /// Total row
        /// </summary>
        [JsonPropertyName("toral_row")]
        public int TotalRow { get; set; }

        /// <summary>
        /// Number of page
        /// </summary>
        [JsonPropertyName("page_number")]
        public int PageNumber { get; set; }

        /// <summary>
        /// List Items
        /// </summary>
        public IEnumerable<T>? Data { get; set; }
    }
}

using System;
using System.Text.Json.Serialization;

namespace Greenglobal.Core.Models
{
    public class FunctionRequest
    {
        [JsonPropertyName("parent_id")]
        public Guid? ParentId { get; set; }

        [JsonPropertyName("path_image")]
        public string? PathImage { get; set; }

        public string Code { get; set; }

        public string Name { get; set; }

        [JsonPropertyName("is_module")]
        public bool IsModule { get; set; } = true;

        [JsonPropertyName("sort_order")]
        public int SortOrder { get; set; }

        public string? Description { get; set; }

        public int Status { get; set; }

        [JsonPropertyName("application_id")]
        public Guid ApplicationId { get; set; }
    }
}

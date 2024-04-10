using System;
using System.Text.Json.Serialization;

namespace Greenglobal.Core.Models
{
    public class RoleRequest
    {
        public string? Code { get; set; }

        public string? Name { get; set; }

        [JsonPropertyName("sort_order")]
        public int SortOrder { get; set; }

        public string? Description { get; set; }

        public int Status { get; set; } = 1;

        [JsonPropertyName("apllication_id")]
        public Guid ApplicationId { get; set; }
    }
}

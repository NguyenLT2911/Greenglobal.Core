using System;
using System.Text.Json.Serialization;
using Volo.Abp.Application.Dtos;

namespace Greenglobal.Core.Models
{
    public class RoleResponse : EntityDto<Guid>
    {
        public string Name { get; set; }

        [JsonPropertyName("sort_order")]
        public int SortOrder { get; set; }

        public string? Description { get; set; }

        public int Status { get; set; } = 1;

        [JsonPropertyName("created_at")]
        public DateTime CreatedAt { get; set; }

        [JsonPropertyName("updated_at")]
        public DateTime? UpdatedAt { get; set; }

        [JsonPropertyName("updated_name")]
        public string? UpdatedName { get; set; }
    }
}

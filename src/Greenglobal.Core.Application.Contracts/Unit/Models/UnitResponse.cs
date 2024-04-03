using System;
using System.Text.Json.Serialization;
using Volo.Abp.Application.Dtos;

namespace Greenglobal.Core.Models
{
    public class UnitResponse : EntityDto<Guid>
    {
        [JsonPropertyName("parent_id")]
        public Guid? ParentId { get; set; }

        public string Name { get; set; }

        [JsonPropertyName("sort_order")]
        public int SortOrder { get; set; }

        public string? Description { get; set; }
        
        public int Status { get; set; }

        [JsonPropertyName("created_at")]
        public DateTime CreatedAt { get; set; }

        [JsonPropertyName("updated_at")]
        public DateTime? UpdatedAt { get; set; }

        [JsonPropertyName("updated_name")]
        public string? UpdatedName { get; set; }
    }
}

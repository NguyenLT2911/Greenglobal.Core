using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Volo.Abp.Application.Dtos;

namespace Greenglobal.Core.Models
{
    public class DepartmentResponse : EntityDto<Guid>
    {
        [JsonPropertyName("unit_id")]
        public Guid UnitId { get; set; }

        [JsonPropertyName("parent_id")]
        public Guid? ParentId { get; set; }

        public string Name { get; set; }

        [JsonPropertyName("sort_order")]
        public int SortOrder { get; set; }

        public string? Description { get; set; }

        public int Status { get; set; }

        [JsonPropertyName("created_at")]
        public DateTime CreatedAt { get; set; }

        [JsonPropertyName("update_at")]
        public DateTime? UpdatedAt { get; set; }

        [JsonPropertyName("update_name")]
        public string? UpdatedName { get; set; }

        public UnitResponse Unit { get; set; }

        public List<DepartmentResponse>? Children { get; set; }

        public DepartmentResponse? Parent { get; set; }
    }
}

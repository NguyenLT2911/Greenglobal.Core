using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Volo.Abp.Application.Dtos;

namespace Greenglobal.Core.Models
{
    public class ApplicationResponse : EntityDto<Guid>
    {
        public string Name { get; set; }

        public string Code { get; set; }

        [JsonPropertyName("icon_path")]
        public string? IconPath { get; set; }

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

        public List<FunctionResponse>? Functions { get; set; }
    }
}

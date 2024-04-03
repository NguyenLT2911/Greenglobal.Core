using System;
using System.Text.Json.Serialization;
using Volo.Abp.Application.Dtos;

namespace Greenglobal.Core.Models
{
    public class ModuleResponse : EntityDto<Guid>
    {
        [JsonPropertyName("parent_id")]
        public Guid? ParentId { get; set; }

        public string Name { get; set; }

        [JsonPropertyName("is_function")]
        public bool IsFunction { get; set; }

        [JsonPropertyName("sort_order")]
        public int SortOrder { get; set; }

        public string? Description { get; set; }

        public int Status { get; set; }
    }
}

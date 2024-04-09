using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Volo.Abp.Application.Dtos;

namespace Greenglobal.Core.Models
{
    public class FunctionResponse : EntityDto<Guid>
    {
        [JsonPropertyName("parent_id")]
        public Guid? ParentId { get; set; }

        [JsonPropertyName("path_image")]
        public string? PathImage { get; set; }

        public string Code { get; set; }

        public string Name { get; set; }

        [JsonPropertyName("is_module")]
        public bool IsModule { get; set; }

        [JsonPropertyName("sort_order")]
        public int SortOrder { get; set; }

        public string? Description { get; set; }

        public int Status { get; set; }

        public List<FunctionResponse> Children { get; set; }
    }
}

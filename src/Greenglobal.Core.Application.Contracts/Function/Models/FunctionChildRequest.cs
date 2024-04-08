using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Greenglobal.Core.Models
{
    public class FunctionChildRequest
    {
        public string Code { get; set; }

        public string Name { get; set; }

        [JsonPropertyName("is_module")]
        public bool IsModule { get; set; } = false;

        [JsonPropertyName("sort_order")]
        public int SortOrder { get; set; }

        public string? Description { get; set; }

        public int Status { get; set; } = 1;

        [JsonPropertyName("function_children")]
        public List<FunctionChildRequest>? FunctionChildren { get; set; }
    }
}

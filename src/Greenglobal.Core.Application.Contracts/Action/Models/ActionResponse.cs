using System;
using System.Text.Json.Serialization;
using Volo.Abp.Application.Dtos;

namespace Greenglobal.Core.Models
{
    public class ActionResponse : EntityDto<Guid>
    {
        [JsonPropertyName("module_id")]
        public Guid ModuleId { get; set; }

        [JsonPropertyName("action_code")]
        public string ActionCode { get; set; }

        public string Name { get; set; }

        [JsonPropertyName("sort_order")]
        public int SortOrder { get; set; }

        public string? Description { get; set; }

        public int Status { get; set; }

        public ModuleResponse Module { get; set; }
    }
}

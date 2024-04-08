using System;
using System.Text.Json.Serialization;

namespace Greenglobal.Core.Models
{
    public class PermissionRequest
    {
        [JsonPropertyName("role_id")]
        public Guid RoleId { get; set; }

        [JsonPropertyName("function_id")]
        public Guid FunctionId { get; set; }

        [JsonPropertyName("is_allowed")]
        public bool IsAllowed { get; set; }
    }
}

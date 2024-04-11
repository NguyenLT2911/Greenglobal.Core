using System;
using System.Text.Json.Serialization;

namespace Greenglobal.Core.Models
{
    public class UserRoleAppRequest
    {
        [JsonPropertyName("role_id")]
        public Guid RoleId { get; set; }

        [JsonPropertyName("application_id")]
        public Guid ApplicationId { get; set; }

        [JsonPropertyName("is_main")]
        public bool IsMain { get; set; } = true;
    }
}

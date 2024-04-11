using System;
using System.Text.Json.Serialization;
using Volo.Abp.Application.Dtos;

namespace Greenglobal.Core.Models
{
    public class UserRoleAppResponse : EntityDto<Guid>
    {
        [JsonPropertyName("user_id")]
        public Guid UserId { get; set; }

        [JsonPropertyName("role_id")]
        public Guid RoleId { get; set; }

        [JsonPropertyName("application_id")]
        public Guid ApplicationId { get; set; }

        [JsonPropertyName("is_main")]
        public bool IsMain { get; set; }

        public RoleResponse? Role { get; set; }

        public ApplicationResponse? Application { get; set; }
    }
}

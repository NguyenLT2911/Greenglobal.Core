using System;
using System.Text.Json.Serialization;
using Volo.Abp.Application.Dtos;

namespace Greenglobal.Core.Models
{
    public class UserRoleDeptResponse : EntityDto<Guid>
    {
        [JsonPropertyName("user_id")]
        public Guid UserId { get; set; }

        [JsonPropertyName("role_id")]
        public Guid RoleId { get; set; }

        [JsonPropertyName("department_id")]
        public Guid DepartmentId { get; set; }

        [JsonPropertyName("is_main")]
        public bool IsMain { get; set; }

        public UserResponse User { get; set; }

        public RoleResponse Role { get; set; }

        public DepartmentResponse Department { get; set; }
    }
}

using System;
using System.Text.Json.Serialization;
using Volo.Abp.Application.Dtos;

namespace Greenglobal.Core.Models
{
    public class UserTitleDeptResponse : EntityDto<Guid>
    {
        [JsonPropertyName("user_id")]
        public Guid UserId { get; set; }

        [JsonPropertyName("title_id")]
        public Guid TitleId { get; set; }

        [JsonPropertyName("department_id")]
        public Guid DepartmentId { get; set; }

        [JsonPropertyName("is_main")]
        public bool IsMain { get; set; }

        public UserResponse User { get; set; }

        public TitleResponse Title { get; set; }

        public DepartmentResponse Department { get; set; }
    }
}

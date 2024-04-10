using System;
using System.Text.Json.Serialization;

namespace Greenglobal.Core.Models
{
    public class UserTitleDeptRequest
    {
        [JsonPropertyName("title_id")]
        public Guid TitleId { get; set; }

        [JsonPropertyName("department_id")]
        public Guid DepartmentId { get; set; }

        [JsonPropertyName("is_main")]
        public bool IsMain { get; set; } = false;
    }
}

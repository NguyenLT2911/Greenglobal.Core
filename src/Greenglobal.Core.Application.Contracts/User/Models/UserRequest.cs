using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Greenglobal.Core.Models
{
    public class UserRequest
    {
        [JsonPropertyName("full_name")]
        public string FullName { get; set; }

        [JsonPropertyName("user_name")]
        public string UserName { get; set; }

        public string Password { get; set; }

        public string? Email { get; set; }

        [JsonPropertyName("allow_login")]
        public bool AllowLogin { get; set; }

        [JsonPropertyName("phone_number")]
        public string? PhoneNumber { get; set; }

        [JsonPropertyName("avatar_path")]
        public string? AvatarPath { get; set; }

        [JsonPropertyName("sort_order")]
        public int SortOrder { get; set; }

        public string? Description { get; set; }

        public int Status { get; set; } = 1;

        [JsonPropertyName("department_id")]
        public Guid DepartmentId { get; set; }
        
        [JsonPropertyName("title_id")]
        public Guid TitleId { get; set; }

        public List<UserTitleDeptRequest>? Concurrent { get; set; }

        public List<UserRoleAppRequest>? UserRoleApps { get; set; }
    }
}

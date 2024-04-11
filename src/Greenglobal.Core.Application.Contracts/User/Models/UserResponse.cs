using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Volo.Abp.Application.Dtos;

namespace Greenglobal.Core.Models
{
    public class UserResponse : EntityDto<Guid>
    {
        [JsonPropertyName("full_name")]
        public string FullName { get; set; }

        [JsonPropertyName("user_name")]
        public string UserName { get; set; }

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

        public int Status { get; set; }

        [JsonPropertyName("created_at")]
        public DateTime CreatedAt { get; set; }

        [JsonPropertyName("updated_at")]
        public DateTime? UpdatedAt { get; set; }

        [JsonPropertyName("updated_name")]
        public string? UpdatedName { get; set; }

        public TitleResponse? Title { get; set; }

        public DepartmentResponse? Department { get; set; }

        public List<UserTitleDeptResponse>? Concurrent { get; set; }

        public List<UserRoleAppResponse>? UserRoleApps { get; set; }
    }
}

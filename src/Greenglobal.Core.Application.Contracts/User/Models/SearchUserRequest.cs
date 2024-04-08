using System;
using System.Text.Json.Serialization;

namespace Greenglobal.Core.Models
{
    public class SearchUserRequest : SearchBaseRequest
    {
        [JsonPropertyName("full_name")]
        public string? FullName { get; set; }

        [JsonPropertyName("department_id")]
        public Guid? DepartmentId { get; set; }
    }
}

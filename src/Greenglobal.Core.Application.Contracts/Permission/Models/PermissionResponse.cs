﻿using System;
using System.Text.Json.Serialization;
using Volo.Abp.Application.Dtos;

namespace Greenglobal.Core.Models
{
    public class PermissionResponse : EntityDto<Guid>
    {
        [JsonPropertyName("role_id")]
        public Guid RoleId { get; set; }

        [JsonPropertyName("function_id")]
        public Guid FunctionId { get; set; }

        [JsonPropertyName("is_allowed")]
        public bool IsAllowed { get; set; }

        public RoleResponse Role { get; set; }
    }
}

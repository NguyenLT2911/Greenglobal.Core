using System;

namespace Greenglobal.Core.Models
{
    public class UserRequest
    {
        public string FullName { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        public string? Email { get; set; }

        public string? PhoneNumber { get; set; }

        public string? AvatarPath { get; set; }

        public int SortOrder { get; set; }

        public string? Description { get; set; }

        public int Status { get; set; } = 1;

        public Guid UnitId { get; set; }

        public Guid DepartmentId { get; set; }

        public Guid RoleId { get; set; }
    }
}

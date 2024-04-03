using System;

namespace Greenglobal.Core.Models
{
    public class UserRoleDeptRequest
    {
        public Guid UserId { get; set; }

        public Guid RoleId { get; set; }

        public Guid DepartmentId { get; set; }

        public bool IsMain { get; set; }
    }
}

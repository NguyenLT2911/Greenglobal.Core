using System;

namespace Greenglobal.Core.Models
{
    public class PermissionRequest
    {
        public Guid RoleId { get; set; }

        public Guid ActionId { get; set; }

        public bool IsAllowed { get; set; }

        public Action Action { get; set; }
    }
}

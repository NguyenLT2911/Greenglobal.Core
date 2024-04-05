using System;

namespace Greenglobal.Core.Models
{
    public class PermissionRequest
    {
        public Guid RoleId { get; set; }

        public Guid FunctionId { get; set; }

        public bool IsAllowed { get; set; }
    }
}

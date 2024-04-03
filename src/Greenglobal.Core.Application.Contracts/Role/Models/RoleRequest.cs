using System;

namespace Greenglobal.Core.Models
{
    public class RoleRequest
    {
        public string Name { get; set; }

        public int SortOrder { get; set; }

        public string? Description { get; set; }

        public int Status { get; set; } = 1;

        public DateTime CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public string? UpdatedName { get; set; }
    }
}

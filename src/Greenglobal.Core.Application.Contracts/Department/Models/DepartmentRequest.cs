using System;

namespace Greenglobal.Core.Models
{
    public class DepartmentRequest
    {
        public Guid UnitId { get; set; }

        public Guid? ParentId { get; set; }

        public string Name { get; set; }

        public int SortOrder { get; set; }

        public string? Description { get; set; }

        public int Status { get; set; } = 1;
    }
}

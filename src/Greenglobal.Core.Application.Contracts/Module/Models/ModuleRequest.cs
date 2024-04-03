using System;

namespace Greenglobal.Core.Models
{
    public class ModuleRequest
    {
        public Guid? ParentId { get; set; }

        public string Name { get; set; }

        public bool IsFunction { get; set; }

        public int SortOrder { get; set; }

        public string? Description { get; set; }

        public int Status { get; set; }
    }
}

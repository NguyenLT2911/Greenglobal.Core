using System;

namespace Greenglobal.Core.Models
{
    public class FunctionChildRequest
    {
        public Guid? ParentId { get; set; }

        public string Code { get; set; }

        public string Name { get; set; }

        public bool IsModule { get; set; } = false;

        public int SortOrder { get; set; }

        public string? Description { get; set; }

        public int Status { get; set; } = 1;
    }
}

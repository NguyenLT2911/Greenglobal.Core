using System;

namespace Greenglobal.Core.Models
{
    public class ActionRequest
    {
        public Guid ModuleId { get; set; }

        public string ActionCode { get; set; }

        public string Name { get; set; }

        public int SortOrder { get; set; }

        public string? Description { get; set; }

        public int Status { get; set; }
    }
}

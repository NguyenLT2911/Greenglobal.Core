using System.Collections.Generic;

namespace Greenglobal.Core.Models
{
    public class FunctionRequest
    {
        public string Code { get; set; }

        public string Name { get; set; }

        public string Url { get; set; }

        public bool IsModule { get; set; } = true;

        public int SortOrder { get; set; }

        public string? Description { get; set; }

        public int Status { get; set; }

        public List<FunctionChildRequest> FunctionChildren { get; set; }
    }
}

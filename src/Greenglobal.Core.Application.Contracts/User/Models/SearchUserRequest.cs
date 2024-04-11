using System;

namespace Greenglobal.Core.Models
{
    public class SearchUserRequest : SearchBaseRequest
    {
        public string? FullName { get; set; }

        public Guid? DepartmentId { get; set; }
    }
}

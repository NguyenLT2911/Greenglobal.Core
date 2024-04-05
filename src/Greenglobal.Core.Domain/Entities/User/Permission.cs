using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Volo.Abp.Domain.Entities;


namespace Greenglobal.Core.Entities
{
    public class Permission : Entity<Guid>
    {
        [Required]
        public Guid RoleId { get; set; }

        [Required]
        public Guid FunctionId { get; set; }

        [Required]
        public bool IsAllowed { get; set; } = true;

        [ForeignKey("RoleId")]
        public Role Role { get; set; }

        [ForeignKey("FunctionId")]
        public Function Function { get; set; }

    }
}

using System;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Domain.Entities;

namespace Greenglobal.Core.Entities
{
    public class Module : Entity<Guid>
    {
        public Guid? ParentId { get; set; }

        [Required, MaxLength(100)]
        public string? Name { get; set; }

        [Required]
        public bool IsFunction { get; set; }

        [Required]
        public int SortOrder { get; set; }

        [MaxLength(300)]
        public string? Description { get; set; }

        [Required]
        public int Status { get; set; } = 1;
    }
}

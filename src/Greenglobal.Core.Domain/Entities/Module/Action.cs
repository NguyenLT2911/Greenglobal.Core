using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Volo.Abp.Domain.Entities;

namespace Greenglobal.Core.Entities
{
    public class Action : Entity<Guid>
    {
        [Required]
        public Guid ModuleId { get; set; }

        [Required, MaxLength(20)]
        public string ActionCode { get; set; }

        [Required, MaxLength(100)]
        public string Name { get; set; }

        [Required]
        public int SortOrder { get; set; }

        [MaxLength(300)]
        public string? Description { get; set; }

        [Required]
        public int Status { get; set; } = 1;

        [ForeignKey("ModuleId")]
        public Module Module { get; set; }
    }
}

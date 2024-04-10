using System;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Domain.Entities;

namespace Greenglobal.Core.Entities
{
    public class Unit : Entity<Guid>
    {
        public Guid? ParentId { get; set; }

        [Required, MaxLength(100)]
        public string Name { get; set; }

        [Required, MaxLength(30)]
        public string ShortName { get; set; }

        [Required]
        public int SortOrder { get; set; }

        [MaxLength(300)]
        public string? Description { get; set; }

        [Required]
        public int Status { get; set; } = 1;

        [Required]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime? UpdatedAt { get; set; }

        [MaxLength(100)]
        public string? UpdatedName { get; set; }
    }
}

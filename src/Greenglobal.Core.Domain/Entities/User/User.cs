using System;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Domain.Entities;


namespace Greenglobal.Core.Entities
{
    public class User : Entity<Guid>
    {
        [Required, MaxLength(100)]
        public string FullName { get; set; }

        [Required, MaxLength(50)]
        public string UserName { get; set; }

        [Required, MaxLength(100)]
        public string Password { get; set; }

        [MaxLength(50)]
        public string? Email { get; set; }

        [MaxLength(100)]
        public string? PhoneNumber { get; set; }

        [MaxLength(300)]
        public string? AvatarPath { get; set; }

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

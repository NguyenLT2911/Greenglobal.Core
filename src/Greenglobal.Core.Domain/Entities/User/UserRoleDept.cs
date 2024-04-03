using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Volo.Abp.Domain.Entities;


namespace Greenglobal.Core.Entities
{
    public class UserRoleDept : Entity<Guid>
    {
        [Required]
        public Guid UserId { get; set; }

        [Required]
        public Guid RoleId { get; set; }

        [Required]
        public Guid DepartmentId { get; set; }

        [Required]
        public bool IsMain { get; set; }

        [ForeignKey("UserId")]
        public User User { get; set; }

        [ForeignKey("RoleId")]
        public Role Role { get; set; }

        [ForeignKey("DepartmentId")]
        public Department Department { get; set; }
    }
}

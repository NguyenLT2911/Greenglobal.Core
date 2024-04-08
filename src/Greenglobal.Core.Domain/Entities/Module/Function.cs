﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Domain.Entities;

namespace Greenglobal.Core.Entities
{
    public class Function : Entity<Guid>
    {
        public Guid? ParentId { get; set; }

        [Required, MaxLength(100)]
        public string? Name { get; set; }

        [Required]
        public bool IsModule { get; set; } = true;

        [Required]
        public int SortOrder { get; set; }

        [MaxLength(300)]
        public string? Description { get; set; }

        [Required]
        public int Status { get; set; } = 1;

        public string PathImage { get; set; }

        [Required, MaxLength(50)]
        public string Code { get; set; }

        //public List<Function>? Children { get; set; }
    }
}

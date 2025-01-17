﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OrderManagementApp.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }

        // Navigation property
        public virtual ICollection<Order> Orders { get; set; }
    }
}
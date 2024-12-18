using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OrderManagementApp.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }

        // Navigation property
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
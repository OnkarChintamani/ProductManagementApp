﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProductManagementApp.Models
{
    [Table("Products")]
    public class Product
    {
        [Key]
            public int Id { get; set; }
            public string? Name { get; set; }
            public string? Description { get; set; }
            public float Price { get; set; }
        

    }
}

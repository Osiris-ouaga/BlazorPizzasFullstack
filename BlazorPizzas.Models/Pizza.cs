using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorPizzas.Models
{
    public class Pizza
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string [] Ingredients { get; set; }
        [Range(1, 99)]

        [Required]
        public int Price { get; set; }

        [Required]
        public string ImageName { get; set; }
    }
}

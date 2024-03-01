using BlazorPizzas.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace BlazorPizzas.API.Data
{
    public class PizzasDbContext : DbContext
    {
        public PizzasDbContext(
            DbContextOptions<PizzasDbContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Pizza>(pizza =>
            {
                pizza
                .Property(p => p.Ingredients)
                .HasConversion(
                     v => string.Join(",", v),
                     v => v.Split(",", StringSplitOptions.RemoveEmptyEntries));

                pizza.HasData(new List<Pizza>
                {
                    new Pizza{ Id =1, Name ="Bacon", Price = 12, Ingredients = new[] { "bacon", "mozzarella", "champignon", "emmental" }, ImageName = "bacon.jpg"  },
                    new Pizza{ Id =2, Name ="4 fromages", Price= 11, Ingredients = new[] { "cantal", "mozzarella", "fromage de chèvre", "gruyère" }, ImageName = "cheese.jpg"  },
                    new Pizza{ Id =3, Name ="Margherita", Price = 10, Ingredients = new[] { "sauce tomate", "mozzarella", "basilic" }, ImageName = "margherita.jpg"  },
                    new Pizza{ Id =4, Name ="Mexicaine", Price=12, Ingredients = new[] { "boeuf", "mozzarella", "maïs", "tomates", "oignon", "coriandre" }, ImageName = "meaty.jpg"  },
                    new Pizza{ Id =5, Name ="Reine", Price=11, Ingredients = new[] { "jambon", "champignons", "mozzarella" }, ImageName = "mushroom.jpg"  },
                    new Pizza{ Id =6, Name ="Pepperoni", Price=11, Ingredients = new[] { "mozzarella", "pepperoni", "tomates" }, ImageName = "pepperoni.jpg"  },
                    new Pizza{ Id =7, Name ="Végétarienne",Price = 10, Ingredients = new[] { "champignons", "roquette", "artichauts", "aubergine" }, ImageName = "veggie.jpg"  }
                });
            });
        }

        public DbSet<Pizza> Pizza { get; set; }
    }
}

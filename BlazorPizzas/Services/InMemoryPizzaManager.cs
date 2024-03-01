using BlazorPizzas.Models;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorPizzas.Services
{
    public class InMemoryPizzaManager : IPizzaManager
    {
        private ConcurrentBag<Pizza> pizzas;

        public InMemoryPizzaManager()
        {
           
        }
        public Task AddOrUpdate(Pizza p)
        {
            var pizza = pizzas.FirstOrDefault(piz => piz.Id == p.Id);
            if (pizza != null)
            {
                pizzas = new ConcurrentBag<Pizza>(pizzas.Where(piz => piz.Id != p.Id));
            }
            pizzas.Add(p);
            return Task.CompletedTask;
        }

        public Task<IEnumerable<Pizza>> GetPizzas()
        {
            return Task.FromResult(pizzas.AsEnumerable());
        }
    }
}

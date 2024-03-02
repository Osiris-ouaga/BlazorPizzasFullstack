using BlazorPizzas.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace BlazorPizzas.Services 
{
    public class HttpPizzaManager : IPizzaManager
    {
        private readonly HttpClient client;


        public HttpPizzaManager(HttpClient client)
        {
            this.client = client;
        }

        public Task AddOrUpdate(Pizza p) =>client.PostAsJsonAsync(requestUri: "api/pizzas", p);

        public Task<IEnumerable<Pizza>> GetPizzas() => client.GetFromJsonAsync<IEnumerable<Pizza>>(requestUri: "api/pizzas");
    }
}

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PokeGraf.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace PokeGraf.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index(string searchString)
        {
            if (String.IsNullOrEmpty(searchString))
            {
                return View();
            }
            else
            {
                List<Pokemon> pokemons = new List<Pokemon>(3);

                RestSharp.RestClient pokeApiCliente = new RestSharp.RestClient($"https://pokeapi.co/api/v2/pokemon/ditto");
                RestSharp.RestRequest pokeApiRequest = new RestSharp.RestRequest(RestSharp.Method.GET);
                var resultado = pokeApiCliente.Execute(pokeApiRequest);
                Models.Pokemon pokemon = Newtonsoft.Json.JsonConvert.DeserializeObject<Pokemon>(resultado.Content);
                pokemons.Add(pokemon);

                pokeApiCliente = new RestSharp.RestClient($"https://pokeapi.co/api/v2/pokemon/bulbasaur");
                pokeApiRequest = new RestSharp.RestRequest(RestSharp.Method.GET);
                resultado = pokeApiCliente.Execute(pokeApiRequest);
                pokemon = Newtonsoft.Json.JsonConvert.DeserializeObject<Pokemon>(resultado.Content);
                pokemons.Add(pokemon);

                pokeApiCliente = new RestSharp.RestClient($"https://pokeapi.co/api/v2/pokemon/pikachu");
                pokeApiRequest = new RestSharp.RestRequest(RestSharp.Method.GET);
                resultado = pokeApiCliente.Execute(pokeApiRequest);
                pokemon = Newtonsoft.Json.JsonConvert.DeserializeObject<Pokemon>(resultado.Content);
                pokemons.Add(pokemon);

                ViewBag.Pokemons = pokemons;

                return View();
            }
        }

        //public IActionResult Index()
        //{
        //    return View();
        //}

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        

        //[HttpPost]
        //public void Testando()
        //{
        

        //    Response.Redirect("/resultado");
        //}


    }
}

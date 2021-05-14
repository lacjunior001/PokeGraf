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

        public IActionResult Index(string busca)
        {
            if (String.IsNullOrEmpty(busca))
            {
                return View();
            }
            else
            {
                /**
                if (achou no banco?)
                {

                }
                else
                {
                  busca na api
                }
                */

                RestSharp.RestClient pokeApiCliente;
                RestSharp.RestRequest pokeApiRequest;

                for (int i = 0; i < 19; i++)
                {
                    pokeApiCliente = new RestSharp.RestClient($"https://pokeapi.co/api/v2/type/{i}/");
                    pokeApiRequest = new RestSharp.RestRequest(RestSharp.Method.GET);
                    var resultado = pokeApiCliente.Execute(pokeApiRequest);
                    if (resultado.IsSuccessful)
                    {
                        Models.Type tipo = Newtonsoft.Json.JsonConvert.DeserializeObject<Models.Type>(resultado.Content);

                        foreach (var item in tipo.Pokemon)
                        {
                            pokeApiCliente = new RestSharp.RestClient(item.Pokemon.Url);
                            pokeApiRequest = new RestSharp.RestRequest(RestSharp.Method.GET);
                            resultado = pokeApiCliente.Execute(pokeApiRequest);
                            Pokemon poke = Newtonsoft.Json.JsonConvert.DeserializeObject<Models.Pokemon>(resultado.Content);

                            //Inserir no banco as infos do pokemon
                            string nome = poke.Name;
                            int peso = poke.Weight;
                            int altura = poke.Height;
                            string pokeElemento;
                            
                            foreach (var tipoo in poke.Types)
                            {
                                pokeElemento = tipoo.Type.Name;
                            }
                            string sprite = poke.Sprites.FrontDefault;


                            pokeApiCliente = new RestSharp.RestClient(poke.Species.Url);
                            pokeApiRequest = new RestSharp.RestRequest(RestSharp.Method.GET);
                            resultado = pokeApiCliente.Execute(pokeApiRequest);
                            PokemonSpecies pokeS = Newtonsoft.Json.JsonConvert.DeserializeObject<Models.PokemonSpecies>(resultado.Content);
                            
                            //resto das infos
                            int idPoke = pokeS.Order;
                            string descricao = pokeS.FlavorTextEntries[0].FlavorText;
                            string raridade;
                            if (pokeS.IsBaby)
                            {
                                raridade = "Baby";
                            }
                            else if (pokeS.IsLegendary)
                            {
                                raridade = "Legendary";
                            }
                            else if (pokeS.IsMythical)
                            {
                                raridade = "Mythical";
                            }
                            else
                            {
                                raridade = "Commom";
                            }
                        }
                    }
                }

                //RestSharp.RestClient pokeApiCliente = new RestSharp.RestClient($"https://pokeapi.co/api/v2/pokemon/{busca}/");
                //RestSharp.RestRequest pokeApiRequest = new RestSharp.RestRequest(RestSharp.Method.GET);
                //var resultado = pokeApiCliente.Execute(pokeApiRequest);
                //Pokemon pokemon;
                /**
                if (resultado.IsSuccessful)
                {
                    pokemon = Newtonsoft.Json.JsonConvert.DeserializeObject<Pokemon>(resultado.Content);
                    ViewBag.Pokemon = pokemon;

                    pokeApiCliente = new RestSharp.RestClient($"https://pokeapi.co/api/v2/pokemon-species/{pokemon.Id}/");
                    pokeApiRequest = new RestSharp.RestRequest(RestSharp.Method.GET);
                    resultado = pokeApiCliente.Execute(pokeApiRequest);
                    PokemonSpecies pokemonS = Newtonsoft.Json.JsonConvert.DeserializeObject<PokemonSpecies>(resultado.Content);
                    ViewBag.PokemonS = pokemonS;

                    pokeApiCliente = new RestSharp.RestClient(pokemonS.EvolutionChain.Url);
                    pokeApiRequest = new RestSharp.RestRequest(RestSharp.Method.GET);
                    resultado = pokeApiCliente.Execute(pokeApiRequest);
                    EvolutionChain pokemonChain = Newtonsoft.Json.JsonConvert.DeserializeObject<EvolutionChain>(resultado.Content);
                    ViewBag.PokemonChain = pokemonChain;

                    /**
                     * chain list = 0 quando não tem evolução
                     * */
                    Console.WriteLine("");/**
                }
                else
                {
                    pokemon = new Pokemon();
                    pokemon.Name = "NãoExiste";
                    ViewBag.Pokemon = pokemon;
                }
                */
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

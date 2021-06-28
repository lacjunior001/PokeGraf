using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PokeGraf.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Text;

namespace PokeGraf.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index(string[] geracoes)
        {
            if (geracoes == null)
            {
                //parametros para os gaficos zerados

            }
            else
            {
                List<RegiaoDB> regioes = new List<RegiaoDB>();

                foreach (var item in geracoes)
                {
                    RegiaoDB reg = RegiaoDB.Construir(item);
                    regioes.Add(reg);
                }

                ViewBag.Regioes = regioes;
            }

            //ViewBag.Tipos = tipos;

            return View();
        }

        public IActionResult StatusPokemon()
        {
            return View();
        }

        public IActionResult MedStatus()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

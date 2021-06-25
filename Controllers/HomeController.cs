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

        public IActionResult Index()
        {
            List<string> tipos = new List<string>(18);
            List<int> quantidade = new List<int>(18);

            //Banco.BancoSelect banco = new Banco.BancoSelect();
            StringBuilder str = new StringBuilder();
            //str.Append("google.charts.load(\"current\", { packages:['corechart'] });" +
            //                "google.charts.setOnLoadCallback(drawChart);" +
            //                "function drawChart()" +
            //                 "{" +
            //                    "var data = google.visualization.arrayToDataTable([" +
            //                    "[\"Element\", \"oi\", { role: \"style\" }],\");");

            for (int i = 0; i < 18; i++)
            {
                //quantidade.Add(banco.RetornarQuantidade(i));
                //tipos.Add(banco.RetornarNomeTipo(i));
                //str.Append($"[\"{banco.RetornarNomeTipo(i)}\", {banco.RetornarQuantidade(i)}, \"#e32636\"],");

                str.Append($"[\"Voador\", 30, \"#e32636\"],");

            }

            //    str.Append("]);" +

            //    "var view = new google.visualization.DataView(data);" +
            //    "view.setColumns([0, 1," +
            //        "{" +
            //    "calc: \"stringify\"," +
            //            "sourceColumn: 1," +
            //            "type: \"string\"," +
            //            "role: \"annotation" +
            //        "}," +
            //        "2]);" +
            //    "var options = {" +
            //        "title: \"Número de Pokémon por Geração\"," +
            //        "width: 300," +
            //        "height: 500," +
            //        "bar: { groupWidth: \"95%\" }," +
            //        "legend: { position: \"none\" }," +
            //    "};" +
            //"var chart = new google.visualization.ColumnChart(document.getElementById(\"columnchart_values\"));" +
            //"chart.draw(view, options);" +
            //"}");

            Microsoft.AspNetCore.Html.HtmlString htmlContent = new Microsoft.AspNetCore.Html.HtmlString("<h1>TESTE</h1>");

            //ViewBag.Quantidade = str.ToString();
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

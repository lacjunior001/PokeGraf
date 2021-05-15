using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PokeGraf.Models;
using RestSharp;

namespace PokeGraf.Controllers
{
    public class Outros
    {
        /// <summary>
        /// Recebe URL e retorna a resposta.
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static IRestResponse BaixarInfoAPI(string url)
        {
            RestClient cliente = new RestClient(url);
            RestRequest request = new RestRequest(Method.GET);

            IRestResponse resposta = cliente.Execute(request);

            if (resposta.IsSuccessful)
            {
                return resposta;
            }
            else
            {
                throw new Exception("Erro ao Buscar na API");
            }
        }



    }
}

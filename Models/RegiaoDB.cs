using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;

namespace PokeGraf.Models
{
    /// <summary>
    /// Regiões dos Pokemons
    /// Kanto, Johto , etc..
    /// </summary>
    public class RegiaoDB
    {
        public int IdRegião { get; private set; }
        public string Nome { get; private set; }

        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// Construtor
        /// </summary>
        /// <param name="idRegião"></param>
        /// <param name="nome"></param>
        public RegiaoDB(int idRegião, string nome)
        {
            IdRegião = idRegião;
            Nome = nome;
        }

        /// <summary>
        /// Retorna a classe Tipo/Elemento.
        /// Buca no banco se não achar busca na API.
        /// </summary>
        /// <returns></returns>
        public static RegiaoDB Construir(string nome)
        {
            try
            {
                FileStream fs = new FileStream(@"C:\log4net\log4net.config", FileMode.Open);
                log4net.Config.XmlConfigurator.Configure(fs);
                fs.Close();
            }
            catch (Exception e)
            {
                throw new Exception("log4net." + e.Message);
            }

            try
            {
                //Gabriel Providenciar selec tipo/elemento pelo nome do tipo - retorna objeto TipoDB
                Banco.BancoSelect.BuscaNoBanco(nome);

                return new RegiaoDB(1, "qqqq");
            }
            catch (Exception e)
            {
                log.Error("Tipo.Erro ao Consultar Banco", e);

                RestSharp.IRestResponse resposta = Controllers.Outros.BaixarInfoAPI($"https://pokeapi.co/api/v2/type/{i}/");

                if (resposta.IsSuccessful)
                {
                    Type type = Newtonsoft.Json.JsonConvert.DeserializeObject<Type>(resposta.Content);

                    TipoDB tipo = new TipoDB(type.Id, type.Name);

                    try
                    {
                        //Gabriel providenciar o insert no banco
                    }
                    catch (Exception ex)
                    {
                        log.Error("Tipo.Erro ao inserir no banco", e);
                    }
                    return tipo;
                }
                else
                {
                    log.Error("Tipo.Erro ao Consultar na API", e);
                    throw new Exception("Elemento não encontrado");
                }
            }
        }




    }
}

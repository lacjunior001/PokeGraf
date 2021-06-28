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
        /// Retorna a Região/Geração dos Pokemon.
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
                //Gabriel Providenciar selec que procura a região pelo nome recebido aqui e retorna essa região
                Banco.BancoSelect.BuscaNoBanco(nome);

                return new RegiaoDB(1, "qqqq");
            }
            catch (Exception e)
            {
                log.Error("Tipo.Erro ao Consultar Banco", e);

                RestSharp.IRestResponse resposta = Controllers.Outros.BaixarInfoAPI($"https://pokeapi.co/api/v2/generation/{nome}/");
                if (resposta.IsSuccessful)
                {
                    Generation geracao = Newtonsoft.Json.JsonConvert.DeserializeObject<Generation>(resposta.Content);
                    RegiaoDB regiao = new RegiaoDB(geracao.Id, geracao.Name);

                    try
                    {
                        //Gabriel providenciar insert da região no banco
                    }
                    catch (Exception ex)
                    {
                        log.Error("Região.Erro ao inserir no banco", e);
                    }
                    return regiao;
                }
                else
                {
                    log.Error("Região.Erro ao Consultar na API.", e);
                    throw new Exception("Região não encontrada.");
                }
            }
        }

        /// <summary>
        /// Retorna os Pokemons apareceram pela primeira vez na região Instanciada.
        /// </summary>
        /// <returns></returns>
        public List<PokemonDB> PokemonsReg()
        {
            List<PokemonDB> lista = new List<PokemonDB>();
            try
            {
                //Gabriel providenciar o select que vai listar todos pokemons de uma regição.
                return new List<PokemonDB>();
            }
            catch (Exception e)
            {
                Generation geracao = null;

                RestSharp.IRestResponse resposta = Controllers.Outros.BaixarInfoAPI($"https://pokeapi.co/api/v2/generation/{Nome}/");
                if (resposta.IsSuccessful)
                {
                    geracao = Newtonsoft.Json.JsonConvert.DeserializeObject<Generation>(resposta.Content);

                    foreach (var item in geracao.PokemonSpecies)
                    {
                        PokemonDB poke = PokemonDB.Construir(item.Name);
                        lista.Add(poke);
                    }

                    return lista;
                }
                else
                {
                    log.Error("Região.Erro Ao recuperar Lista de Pokemons");
                    throw new Exception("Região.Erro Ao recuperar Lista de Pokemons");
                }
            }
        }


    }
}

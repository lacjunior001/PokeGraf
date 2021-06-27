using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace PokeGraf.Models
{
    /// <summary>
    /// Classe com principais informações sobre Pokemon
    /// </summary>
    public class PokemonDB
    {
        public int IDPoke { get; private set; }
        public string Nome { get; private set; }
        public double Altura { get; private set; }
        public double Peso { get; private set; }
        public string Descricao { get; private set; }
        public string Raridade { get; private set; }
        public string Sprite { get; private set; }
        public RegiaoDB Regiao { get; private set; }

        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// Construtor
        /// </summary>
        /// <param name="iDPoke">ID na National Dex</param>
        /// <param name="nome">Nome POkemon</param>
        /// <param name="altura">Altura do Pokemon</param>
        /// <param name="peso">Peso do Pokemon</param>
        /// <param name="descricao">Descrição do Pokemon</param>
        /// <param name="raridade">Se o Pokemon é Baby, Normal, Lendário</param>
        /// <param name="sprite">URL Com Img</param>
        /// <param name="regiao">Primeira Região onde Apareceu</param>
        public PokemonDB(int iDPoke, string nome, double altura, double peso, string descricao, string raridade, string sprite, RegiaoDB regiao)
        {
            IDPoke = iDPoke;
            Nome = nome;
            Altura = altura;
            Peso = peso;
            Descricao = descricao;
            Raridade = raridade;
            Sprite = sprite;
            Regiao = regiao;
        }

        /// <summary>
        /// Retorna a classe Tipo/Elemento.
        /// Buca no banco se não achar busca na API.
        /// </summary>
        /// <returns></returns>
        public static PokemonDB Construir(string nome)
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

            PokemonDB poke = null;

            try
            {
                //Gabriel Providenciar selec tipo/elemento pelo nome do tipo - retorna objeto TipoDB
                Banco.BancoSelect.BuscaNoBanco(nome);

                RegiaoDB reg = new RegiaoDB(1, "reg");

                return new PokemonDB(1, "nomeee", 1.66, 55.5, "desc", "rar", "sprite", reg);
            }
            catch (Exception e)
            {
                log.Error("Tipo.Erro ao Consultar Banco", e);

                RestSharp.IRestResponse resposta = Controllers.Outros.BaixarInfoAPI($"https://pokeapi.co/api/v2/pokemon/{nome}/");
                if (resposta.IsSuccessful)
                {
                    Pokemon pokeAPI = Newtonsoft.Json.JsonConvert.DeserializeObject<Pokemon>(resposta.Content);
                    PokemonSpecies pokeSAPI = null;
                    resposta = Controllers.Outros.BaixarInfoAPI($"https://pokeapi.co/api/v2/pokemon-species/{nome}/");
                    if (resposta.IsSuccessful)
                    {
                        pokeSAPI = Newtonsoft.Json.JsonConvert.DeserializeObject<PokemonSpecies>(resposta.Content);
                    }
                    else
                    {
                        log.Error("Erro ao recuperar Pokemon na API");
                        throw new Exception("Erro ao recuperar Pokemon na API");
                    }



                    int id = pokeAPI.Id;
                    string nomePokk = pokeAPI.Name;
                    double altura = Convert.ToDouble(pokeAPI.Height);
                    double peso = Convert.ToDouble(pokeAPI.Weight);
                    string descricao = pokeSAPI.FlavorTextEntries != null ? pokeSAPI.FlavorTextEntries[0].FlavorText : null;
                    string raridade = null;

                    if (pokeSAPI.IsBaby)
                    {
                        raridade = "Baby";
                    }
                    else if (pokeSAPI.IsLegendary)
                    {
                        raridade = "Legendary";
                    }
                    else if (pokeSAPI.IsMythical)
                    {
                        raridade = "Mythical";
                    }
                    else
                    {
                        raridade = "Commom";
                    }

                    string sprite = pokeAPI.Sprites != null ? pokeAPI.Sprites.FrontDefault : null;

                    RegiaoDB regiao = RegiaoDB.Construir();

                    poke = new PokemonDB(id, nomePokk, altura, peso, descricao, raridade, sprite,);

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
                    log.Error("Erro ao recuperar Pokemon na API");
                    throw new Exception("Erro ao recuperar Pokemon na API");
                }
            }
        }



    }
}

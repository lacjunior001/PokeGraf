using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace PokeGraf.Models
{
    /// <summary>
    /// Classe dos Tipos/Elementos dos Pokemons
    /// </summary>
    public class TipoDB
    {
        public int IdTipo { get; private set; }
        public string Nome { get; private set; }

        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// Construtor do Tipo/Elemento
        /// </summary>
        /// <param name="idTipo">ID do Tipo/Elemento no Banco</param>
        /// <param name="nomeTipo">Nome do Tipo/Elemento</param>
        public TipoDB(int idTipo, string nomeTipo)
        {
            this.IdTipo = idTipo;
            this.Nome = nomeTipo;

        }

        /// <summary>
        /// Retorna a classe Tipo/Elemento.
        /// Buca no banco se não achar busca na API.
        /// </summary>
        /// <returns></returns>
        public static TipoDB Construir(string nome)
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

                return new TipoDB(1, "qqqq");
            }
            catch (Exception e)
            {
                log.Error("Tipo.Erro ao Consultar Banco", e);

                int i = IdAPITipo(nome);

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

        /// <summary>
        /// Retorna a Ids Utilizadas na PokeAPI pra cada tipo
        /// </summary>
        /// <param name="nome">Nome do Elemento/Tipo</param>
        /// <returns></returns>
        public static int IdAPITipo(string nome)
        {
            return nome switch
            {
                "normal" => 1,
                "fire" => 10,
                "water" => 11,
                "grass" => 12,
                "electric" => 13,
                "ice" => 15,
                "fighting" => 2,
                "poison" => 4,
                "ground" => 5,
                "flying" => 3,
                "psychic" => 14,
                "bug" => 7,
                "rock" => 6,
                "ghost" => 8,
                "dark" => 17,
                "dragon" => 16,
                "steel" => 9,
                "fairy" => 18,
                _ => throw new Exception("Elemento Inválido"),
            };
        }

        /// <summary>
        /// Retorna o Código da Cor do Tipo/Elemento Instanciado.
        /// Caso a cor não esteja Listada retona NL
        /// </summary>
        /// <returns>#A8A878 ou NL</returns>
        public string PegarCor()
        {
            return this.Nome switch
            {
                "normal" => "#A8A878",
                "fire" => "#F08030",
                "water" => "#6890F0",
                "grass" => "#78C850",
                "electric" => "#F8D030",
                "ice" => "#78C850",
                "fighting" => "#C03028",
                "poison" => "#A040A0",
                "ground" => "#E0C068",
                "flying" => "#A890F0",
                "psychic" => "#F85888",
                "bug" => "#A8B820",
                "rock" => "#B8A038",
                "ghost" => "#705898",
                "dark" => "#705848",
                "dragon" => "#7038F8",
                "steel" => "#B8B8D0",
                "fairy" => "#F0B6BC",
                _ => throw new Exception("Elemento Inválido"),
            };
        }

        #region Causando Dano

        /// <summary>
        /// Lista de elementos que sofrem 2x quando atacados pelo elemento instanciado.
        /// </summary>
        /// <returns>Lista de Elementos formato String</returns>
        public List<TipoDB> SofremSuperEfetivo()
        {
            List<TipoDB> super = new List<TipoDB>(6);
            switch (this.Nome)
            {
                case "fire":
                    try
                    {
                        super.Add(Construir("grass"));
                        super.Add(Construir("ice"));
                        super.Add(Construir("bug"));
                        super.Add(Construir("steel"));
                    }
                    catch (Exception e)
                    {
                        log.Error("Tipo.SofremSuPerEfetivo.", e);
                        throw new Exception(e.Message);
                    }
                    return super;

                case "water":
                    try
                    {
                        super.Add(Construir("fire"));
                        super.Add(Construir("ground"));
                        super.Add(Construir("rock"));
                    }
                    catch (Exception e)
                    {
                        log.Error("Tipo.SofremSuPerEfetivo.", e);
                        throw new Exception(e.Message);
                    }
                    return super;

                case "grass":
                    try
                    {
                        super.Add(Construir("water"));
                        super.Add(Construir("ground"));
                        super.Add(Construir("rock"));
                    }
                    catch (Exception e)
                    {
                        log.Error("Tipo.SofremSuPerEfetivo.", e);
                        throw new Exception(e.Message);
                    }
                    return super;

                case "electric":
                    try
                    {
                        super.Add(Construir("water"));
                        super.Add(Construir("flying"));
                    }
                    catch (Exception e)
                    {
                        log.Error("Tipo.SofremSuPerEfetivo.", e);
                        throw new Exception(e.Message);
                    }
                    return super;

                case "ice":
                    try
                    {
                        super.Add(Construir("grass"));
                        super.Add(Construir("ground"));
                        super.Add(Construir("flying"));
                        super.Add(Construir("dragon"));
                    }
                    catch (Exception e)
                    {
                        log.Error("Tipo.SofremSuPerEfetivo.", e);
                        throw new Exception(e.Message);
                    }
                    return super;

                case "fighting":
                    try
                    {
                        super.Add(Construir("normal"));
                        super.Add(Construir("ice"));
                        super.Add(Construir("rock"));
                        super.Add(Construir("dark"));
                        super.Add(Construir("steel"));
                    }
                    catch (Exception e)
                    {
                        log.Error("Tipo.SofremSuPerEfetivo.", e);
                        throw new Exception(e.Message);
                    }
                    return super;

                case "poison":
                    try
                    {
                        super.Add(Construir("grass"));
                        super.Add(Construir("fairy"));
                    }
                    catch (Exception e)
                    {
                        log.Error("Tipo.SofremSuPerEfetivo.", e);
                        throw new Exception(e.Message);
                    }
                    return super;

                case "ground":
                    try
                    {
                        super.Add(Construir("fire"));
                        super.Add(Construir("electric"));
                        super.Add(Construir("poison"));
                        super.Add(Construir("rock"));
                        super.Add(Construir("steel"));
                    }
                    catch (Exception e)
                    {
                        log.Error("Tipo.SofremSuPerEfetivo.", e);
                        throw new Exception(e.Message);
                    }
                    return super;

                case "flying":
                    try
                    {
                        super.Add(Construir("grass"));
                        super.Add(Construir("fighting"));
                        super.Add(Construir("bug"));
                    }
                    catch (Exception e)
                    {
                        log.Error("Tipo.SofremSuPerEfetivo.", e);
                        throw new Exception(e.Message);
                    }
                    return super;

                case "psychic":
                    try
                    {
                        super.Add(Construir("fighting"));
                        super.Add(Construir("poison"));
                    }
                    catch (Exception e)
                    {
                        log.Error("Tipo.SofremSuPerEfetivo.", e);
                        throw new Exception(e.Message);
                    }
                    return super;

                case "bug":
                    try
                    {
                        super.Add(Construir("grass"));
                        super.Add(Construir("psychic"));
                        super.Add(Construir("dark"));
                    }
                    catch (Exception e)
                    {
                        log.Error("Tipo.SofremSuPerEfetivo.", e);
                        throw new Exception(e.Message);
                    }
                    return super;

                case "rock":
                    try
                    {
                        super.Add(Construir("fire"));
                        super.Add(Construir("ice"));
                        super.Add(Construir("flying"));
                        super.Add(Construir("bug"));
                    }
                    catch (Exception e)
                    {
                        log.Error("Tipo.SofremSuPerEfetivo.", e);
                        throw new Exception(e.Message);
                    }
                    return super;

                case "ghost":
                    try
                    {
                        super.Add(Construir("psychic"));
                        super.Add(Construir("ghost"));
                    }
                    catch (Exception e)
                    {
                        log.Error("Tipo.SofremSuPerEfetivo.", e);
                        throw new Exception(e.Message);
                    }
                    return super;

                case "dark":
                    try
                    {
                        super.Add(Construir("psychic"));
                        super.Add(Construir("ghost"));
                    }
                    catch (Exception e)
                    {
                        log.Error("Tipo.SofremSuPerEfetivo.", e);
                        throw new Exception(e.Message);
                    }
                    return super;

                case "dragon":
                    try
                    {
                        super.Add(Construir("dragon"));
                    }
                    catch (Exception e)
                    {
                        log.Error("Tipo.SofremSuPerEfetivo.", e);
                        throw new Exception(e.Message);
                    }
                    return super;

                case "steel":
                    try
                    {
                        super.Add(Construir("ice"));
                        super.Add(Construir("rock"));
                        super.Add(Construir("fairy"));
                    }
                    catch (Exception e)
                    {
                        log.Error("Tipo.SofremSuPerEfetivo.", e);
                        throw new Exception(e.Message);
                    }
                    return super;

                case "fairy":
                    try
                    {
                        super.Add(Construir("fighting"));
                        super.Add(Construir("dragon"));
                        super.Add(Construir("dark"));
                    }
                    catch (Exception e)
                    {
                        log.Error("Tipo.SofremSuPerEfetivo.", e);
                        throw new Exception(e.Message);
                    }
                    return super;

                default:
                    throw new Exception("Valor Não Listado");
            }

        }

        /// <summary>
        /// Lista de elementos que sofrem 0.5 quando atacados pelo elemento instanciado.
        /// </summary>
        /// <returns></returns>
        public List<TipoDB> SofremMetadeDoDano()
        {
            List<TipoDB> super = new List<TipoDB>(6);
            switch (this.Nome)
            {
                case "normal":
                    try
                    {
                        super.Add(Construir("rock"));
                        super.Add(Construir("steel"));
                    }
                    catch (Exception e)
                    {
                        log.Error("Tipo.SofremMetadeDoDano.", e);
                        throw new Exception(e.Message);
                    }
                    return super;

                case "fire":
                    try
                    {
                        super.Add(Construir("fire"));
                        super.Add(Construir("water"));
                        super.Add(Construir("rock"));
                        super.Add(Construir("dragon"));
                    }
                    catch (Exception e)
                    {
                        log.Error("Tipo.SofremMetadeDoDano.", e);
                        throw new Exception(e.Message);
                    }
                    return super;

                case "water":
                    try
                    {
                        super.Add(Construir("water"));
                        super.Add(Construir("grass"));
                        super.Add(Construir("dragon"));
                    }
                    catch (Exception e)
                    {
                        log.Error("Tipo.SofremMetadeDoDano.", e);
                        throw new Exception(e.Message);
                    }
                    return super;

                case "grass":
                    try
                    {
                        super.Add(Construir("fire"));
                        super.Add(Construir("grass"));
                        super.Add(Construir("poison"));
                        super.Add(Construir("flying"));
                        super.Add(Construir("bug"));
                        super.Add(Construir("dragon"));
                        super.Add(Construir("steel"));
                    }
                    catch (Exception e)
                    {
                        log.Error("Tipo.SofremMetadeDoDano.", e);
                        throw new Exception(e.Message);
                    }
                    return super;

                case "electric":
                    try
                    {
                        super.Add(Construir("electric"));
                        super.Add(Construir("grass"));
                        super.Add(Construir("dragon"));
                    }
                    catch (Exception e)
                    {
                        log.Error("Tipo.SofremMetadeDoDano.", e);
                        throw new Exception(e.Message);
                    }
                    return super;

                case "ice":
                    try
                    {
                        super.Add(Construir("fire"));
                        super.Add(Construir("water"));
                        super.Add(Construir("ice"));
                        super.Add(Construir("steel"));
                    }
                    catch (Exception e)
                    {
                        log.Error("Tipo.SofremMetadeDoDano.", e);
                        throw new Exception(e.Message);
                    }
                    return super;

                case "fighting":
                    try
                    {
                        super.Add(Construir("poison"));
                        super.Add(Construir("flying"));
                        super.Add(Construir("psychic"));
                        super.Add(Construir("bug"));
                        super.Add(Construir("fairy"));
                    }
                    catch (Exception e)
                    {
                        log.Error("Tipo.SofremMetadeDoDano.", e);
                        throw new Exception(e.Message);
                    }
                    return super;

                case "poison":
                    try
                    {
                        super.Add(Construir("poison"));
                        super.Add(Construir("ground"));
                        super.Add(Construir("rock"));
                        super.Add(Construir("ghost"));
                    }
                    catch (Exception e)
                    {
                        log.Error("Tipo.SofremMetadeDoDano.", e);
                        throw new Exception(e.Message);
                    }
                    return super;

                case "ground":
                    try
                    {
                        super.Add(Construir("grass"));
                        super.Add(Construir("bug"));
                        super.Add(Construir("poison"));
                        super.Add(Construir("rock"));
                        super.Add(Construir("steel"));
                    }
                    catch (Exception e)
                    {
                        log.Error("Tipo.SofremMetadeDoDano.", e);
                        throw new Exception(e.Message);
                    }
                    return super;

                case "flying":
                    try
                    {
                        super.Add(Construir("electric"));
                        super.Add(Construir("rock"));
                        super.Add(Construir("steel"));
                    }
                    catch (Exception e)
                    {
                        log.Error("Tipo.SofremMetadeDoDano.", e);
                        throw new Exception(e.Message);
                    }
                    return super;

                case "psychic":
                    try
                    {
                        super.Add(Construir("psychic"));
                        super.Add(Construir("steel"));
                    }
                    catch (Exception e)
                    {
                        log.Error("Tipo.SofremMetadeDoDano.", e);
                        throw new Exception(e.Message);
                    }
                    return super;

                case "bug":
                    try
                    {
                        super.Add(Construir("fire"));
                        super.Add(Construir("fighting"));
                        super.Add(Construir("poison"));
                        super.Add(Construir("flying"));
                        super.Add(Construir("ghost"));
                        super.Add(Construir("steel"));
                        super.Add(Construir("fairy"));
                    }
                    catch (Exception e)
                    {
                        log.Error("Tipo.SofremMetadeDoDano.", e);
                        throw new Exception(e.Message);
                    }
                    return super;

                case "rock":
                    try
                    {
                        super.Add(Construir("fighting"));
                        super.Add(Construir("ground"));
                        super.Add(Construir("steel"));
                    }
                    catch (Exception e)
                    {
                        log.Error("Tipo.SofremMetadeDoDano.", e);
                        throw new Exception(e.Message);
                    }
                    return super;

                case "ghost":
                    try
                    {
                        super.Add(Construir("dark"));
                    }
                    catch (Exception e)
                    {
                        log.Error("Tipo.SofremMetadeDoDano.", e);
                        throw new Exception(e.Message);
                    }
                    return super;

                case "dark":
                    try
                    {
                        super.Add(Construir("fighting"));
                        super.Add(Construir("dark"));
                        super.Add(Construir("fairy"));
                    }
                    catch (Exception e)
                    {
                        log.Error("Tipo.SofremMetadeDoDano.", e);
                        throw new Exception(e.Message);
                    }
                    return super;

                case "dragon":
                    try
                    {
                        super.Add(Construir("steel"));
                    }
                    catch (Exception e)
                    {
                        log.Error("Tipo.SofremMetadeDoDano.", e);
                        throw new Exception(e.Message);
                    }
                    return super;

                case "steel":
                    try
                    {
                        super.Add(Construir("fire"));
                        super.Add(Construir("water"));
                        super.Add(Construir("electric"));
                        super.Add(Construir("steel"));
                    }
                    catch (Exception e)
                    {
                        log.Error("Tipo.SofremMetadeDoDano.", e);
                        throw new Exception(e.Message);
                    }
                    return super;

                case "fairy":
                    try
                    {
                        super.Add(Construir("fire"));
                        super.Add(Construir("poison"));
                        super.Add(Construir("steel"));
                    }
                    catch (Exception e)
                    {
                        log.Error("Tipo.SofremMetadeDoDano.", e);
                        throw new Exception(e.Message);
                    }
                    return super;

                default:
                    throw new Exception("Valor Não Listado");
            }
        }

        /// <summary>
        /// Lista de elementos que não sofrem dano quando atacados pelo elemento instanciado.
        /// </summary>
        /// <returns></returns>
        public List<TipoDB> NaoSofremDano()
        {
            List<TipoDB> super = new List<TipoDB>(6);
            switch (this.Nome)
            {
                case "normal":
                    try
                    {
                        super.Add(Construir("ghost"));
                    }
                    catch (Exception e)
                    {
                        log.Error("Tipo.NaoSofremDano.", e);
                        throw new Exception(e.Message);
                    }
                    return super;

                case "electric":
                    try
                    {
                        super.Add(Construir("ground"));
                    }
                    catch (Exception e)
                    {
                        log.Error("Tipo.NaoSofremDano.", e);
                        throw new Exception(e.Message);
                    }
                    return super;

                case "fighting":
                    try
                    {
                        super.Add(Construir("ghost"));
                    }
                    catch (Exception e)
                    {
                        log.Error("Tipo.NaoSofremDano.", e);
                        throw new Exception(e.Message);
                    }
                    return super;

                case "poison":
                    try
                    {
                        super.Add(Construir("steel"));
                    }
                    catch (Exception e)
                    {
                        log.Error("Tipo.NaoSofremDano.", e);
                        throw new Exception(e.Message);
                    }
                    return super;

                case "ground":
                    try
                    {
                        super.Add(Construir("flying"));
                    }
                    catch (Exception e)
                    {
                        log.Error("Tipo.NaoSofremDano.", e);
                        throw new Exception(e.Message);
                    }
                    return super;

                case "psychic":
                    try
                    {
                        super.Add(Construir("dark"));
                    }
                    catch (Exception e)
                    {
                        log.Error("Tipo.NaoSofremDano.", e);
                        throw new Exception(e.Message);
                    }
                    return super;

                case "ghost":
                    try
                    {
                        super.Add(Construir("normal"));
                    }
                    catch (Exception e)
                    {
                        log.Error("Tipo.NaoSofremDano.", e);
                        throw new Exception(e.Message);
                    }
                    return super;

                case "dragon":
                    try
                    {
                        super.Add(Construir("fairy"));
                    }
                    catch (Exception e)
                    {
                        log.Error("Tipo.NaoSofremDano.", e);
                        throw new Exception(e.Message);
                    }
                    return super;

                default:
                    throw new Exception("Valor Não Listado");
            }

        }

        #endregion Causando Dano

        #region Recebendo Dano

        /// <summary>
        /// Lista de elementos que causam 2x quando atacam o elemento instanciado.
        /// </summary>
        /// <returns>Lista de Elementos formato String</returns>
        public List<TipoDB> RecebeSuperEfetivo()
        {
            List<TipoDB> super = new List<TipoDB>(6);
            switch (this.Nome)
            {
                case "fire":
                    try
                    {
                        super.Add(Construir("grass"));
                        super.Add(Construir("ice"));
                        super.Add(Construir("bug"));
                        super.Add(Construir("steel"));
                    }
                    catch (Exception e)
                    {
                        log.Error("Tipo.RecebeSuperEfetivo.", e);
                        throw new Exception(e.Message);
                    }
                    return super;

                case "water":
                    try
                    {
                        super.Add(Construir("fire"));
                        super.Add(Construir("ground"));
                        super.Add(Construir("rock"));
                    }
                    catch (Exception e)
                    {
                        log.Error("Tipo.RecebeSuperEfetivo.", e);
                        throw new Exception(e.Message);
                    }
                    return super;

                case "grass":
                    try
                    {
                        super.Add(Construir("water"));
                        super.Add(Construir("ground"));
                        super.Add(Construir("rock"));
                    }
                    catch (Exception e)
                    {
                        log.Error("Tipo.RecebeSuperEfetivo.", e);
                        throw new Exception(e.Message);
                    }
                    return super;

                case "electric":
                    try
                    {
                        super.Add(Construir("water"));
                        super.Add(Construir("flying"));
                    }
                    catch (Exception e)
                    {
                        log.Error("Tipo.RecebeSuperEfetivo.", e);
                        throw new Exception(e.Message);
                    }
                    return super;

                case "ice":
                    try
                    {
                        super.Add(Construir("grass"));
                        super.Add(Construir("ground"));
                        super.Add(Construir("flying"));
                        super.Add(Construir("dragon"));
                    }
                    catch (Exception e)
                    {
                        log.Error("Tipo.RecebeSuperEfetivo.", e);
                        throw new Exception(e.Message);
                    }
                    return super;

                case "fighting":
                    try
                    {
                        super.Add(Construir("normal"));
                        super.Add(Construir("ice"));
                        super.Add(Construir("rock"));
                        super.Add(Construir("dark"));
                        super.Add(Construir("steel"));
                    }
                    catch (Exception e)
                    {
                        log.Error("Tipo.RecebeSuperEfetivo.", e);
                        throw new Exception(e.Message);
                    }
                    return super;

                case "poison":
                    try
                    {
                        super.Add(Construir("grass"));
                        super.Add(Construir("fairy"));
                    }
                    catch (Exception e)
                    {
                        log.Error("Tipo.RecebeSuperEfetivo.", e);
                        throw new Exception(e.Message);
                    }
                    return super;

                case "ground":
                    try
                    {
                        super.Add(Construir("fire"));
                        super.Add(Construir("electric"));
                        super.Add(Construir("poison"));
                        super.Add(Construir("rock"));
                        super.Add(Construir("steel"));
                    }
                    catch (Exception e)
                    {
                        log.Error("Tipo.RecebeSuperEfetivo.", e);
                        throw new Exception(e.Message);
                    }
                    return super;

                case "flying":
                    try
                    {
                        super.Add(Construir("grass"));
                        super.Add(Construir("fighting"));
                        super.Add(Construir("bug"));
                    }
                    catch (Exception e)
                    {
                        log.Error("Tipo.RecebeSuperEfetivo.", e);
                        throw new Exception(e.Message);
                    }
                    return super;

                case "psychic":
                    try
                    {
                        super.Add(Construir("fighting"));
                        super.Add(Construir("poison"));
                    }
                    catch (Exception e)
                    {
                        log.Error("Tipo.RecebeSuperEfetivo.", e);
                        throw new Exception(e.Message);
                    }
                    return super;

                case "bug":
                    try
                    {
                        super.Add(Construir("grass"));
                        super.Add(Construir("psychic"));
                        super.Add(Construir("dark"));
                    }
                    catch (Exception e)
                    {
                        log.Error("Tipo.RecebeSuperEfetivo.", e);
                        throw new Exception(e.Message);
                    }
                    return super;

                case "rock":
                    try
                    {
                        super.Add(Construir("fire"));
                        super.Add(Construir("ice"));
                        super.Add(Construir("flying"));
                        super.Add(Construir("bug"));
                    }
                    catch (Exception e)
                    {
                        log.Error("Tipo.RecebeSuperEfetivo.", e);
                        throw new Exception(e.Message);
                    }
                    return super;

                case "ghost":
                    try
                    {
                        super.Add(Construir("psychic"));
                        super.Add(Construir("ghost"));
                    }
                    catch (Exception e)
                    {
                        log.Error("Tipo.RecebeSuperEfetivo.", e);
                        throw new Exception(e.Message);
                    }
                    return super;

                case "dark":
                    try
                    {
                        super.Add(Construir("psychic"));
                        super.Add(Construir("ghost"));
                    }
                    catch (Exception e)
                    {
                        log.Error("Tipo.RecebeSuperEfetivo.", e);
                        throw new Exception(e.Message);
                    }
                    return super;

                case "dragon":
                    try
                    {
                        super.Add(Construir("dragon"));
                    }
                    catch (Exception e)
                    {
                        log.Error("Tipo.RecebeSuperEfetivo.", e);
                        throw new Exception(e.Message);
                    }
                    return super;

                case "steel":
                    try
                    {
                        super.Add(Construir("ice"));
                        super.Add(Construir("rock"));
                        super.Add(Construir("fairy"));
                    }
                    catch (Exception e)
                    {
                        log.Error("Tipo.RecebeSuperEfetivo.", e);
                        throw new Exception(e.Message);
                    }
                    return super;

                case "fairy":
                    try
                    {
                        super.Add(Construir("fighting"));
                        super.Add(Construir("dragon"));
                        super.Add(Construir("dark"));
                    }
                    catch (Exception e)
                    {
                        log.Error("Tipo.RecebeSuperEfetivo.", e);
                        throw new Exception(e.Message);
                    }
                    return super;

                default:
                    throw new Exception("Valor Não Listado");
            }

        }

        /// <summary>
        /// Lista de elementos que causam 0.5 quando atacam o elemento instanciado.
        /// </summary>
        /// <returns></returns>
        public List<TipoDB> RecebeMetadeDoDano()
        {
            List<TipoDB> super = new List<TipoDB>(6);
            switch (this.Nome)
            {
                case "normal":
                    try
                    {
                        super.Add(Construir("rock"));
                        super.Add(Construir("steel"));
                    }
                    catch (Exception e)
                    {
                        log.Error("Tipo.RecebeMetadeDoDano.", e);
                        throw new Exception(e.Message);
                    }
                    return super;

                case "fire":
                    try
                    {
                        super.Add(Construir("fire"));
                        super.Add(Construir("water"));
                        super.Add(Construir("rock"));
                        super.Add(Construir("dragon"));
                    }
                    catch (Exception e)
                    {
                        log.Error("Tipo.RecebeMetadeDoDano.", e);
                        throw new Exception(e.Message);
                    }
                    return super;

                case "water":
                    try
                    {
                        super.Add(Construir("water"));
                        super.Add(Construir("grass"));
                        super.Add(Construir("dragon"));
                    }
                    catch (Exception e)
                    {
                        log.Error("Tipo.RecebeMetadeDoDano.", e);
                        throw new Exception(e.Message);
                    }
                    return super;

                case "grass":
                    try
                    {
                        super.Add(Construir("fire"));
                        super.Add(Construir("grass"));
                        super.Add(Construir("poison"));
                        super.Add(Construir("flying"));
                        super.Add(Construir("bug"));
                        super.Add(Construir("dragon"));
                        super.Add(Construir("steel"));
                    }
                    catch (Exception e)
                    {
                        log.Error("Tipo.RecebeMetadeDoDano.", e);
                        throw new Exception(e.Message);
                    }
                    return super;

                case "electric":
                    try
                    {
                        super.Add(Construir("electric"));
                        super.Add(Construir("grass"));
                        super.Add(Construir("dragon"));
                    }
                    catch (Exception e)
                    {
                        log.Error("Tipo.RecebeMetadeDoDano.", e);
                        throw new Exception(e.Message);
                    }
                    return super;

                case "ice":
                    try
                    {
                        super.Add(Construir("fire"));
                        super.Add(Construir("water"));
                        super.Add(Construir("ice"));
                        super.Add(Construir("steel"));
                    }
                    catch (Exception e)
                    {
                        log.Error("Tipo.RecebeMetadeDoDano.", e);
                        throw new Exception(e.Message);
                    }
                    return super;

                case "fighting":
                    try
                    {
                        super.Add(Construir("poison"));
                        super.Add(Construir("flying"));
                        super.Add(Construir("psychic"));
                        super.Add(Construir("bug"));
                        super.Add(Construir("fairy"));
                    }
                    catch (Exception e)
                    {
                        log.Error("Tipo.RecebeMetadeDoDano.", e);
                        throw new Exception(e.Message);
                    }
                    return super;

                case "poison":
                    try
                    {
                        super.Add(Construir("poison"));
                        super.Add(Construir("ground"));
                        super.Add(Construir("rock"));
                        super.Add(Construir("ghost"));
                    }
                    catch (Exception e)
                    {
                        log.Error("Tipo.RecebeMetadeDoDano.", e);
                        throw new Exception(e.Message);
                    }
                    return super;

                case "ground":
                    try
                    {
                        super.Add(Construir("grass"));
                        super.Add(Construir("bug"));
                        super.Add(Construir("poison"));
                        super.Add(Construir("rock"));
                        super.Add(Construir("steel"));
                    }
                    catch (Exception e)
                    {
                        log.Error("Tipo.RecebeMetadeDoDano.", e);
                        throw new Exception(e.Message);
                    }
                    return super;

                case "flying":
                    try
                    {
                        super.Add(Construir("electric"));
                        super.Add(Construir("rock"));
                        super.Add(Construir("steel"));
                    }
                    catch (Exception e)
                    {
                        log.Error("Tipo.RecebeMetadeDoDano.", e);
                        throw new Exception(e.Message);
                    }
                    return super;

                case "psychic":
                    try
                    {
                        super.Add(Construir("psychic"));
                        super.Add(Construir("steel"));
                    }
                    catch (Exception e)
                    {
                        log.Error("Tipo.RecebeMetadeDoDano.", e);
                        throw new Exception(e.Message);
                    }
                    return super;

                case "bug":
                    try
                    {
                        super.Add(Construir("fire"));
                        super.Add(Construir("fighting"));
                        super.Add(Construir("poison"));
                        super.Add(Construir("flying"));
                        super.Add(Construir("ghost"));
                        super.Add(Construir("steel"));
                        super.Add(Construir("fairy"));
                    }
                    catch (Exception e)
                    {
                        log.Error("Tipo.RecebeMetadeDoDano.", e);
                        throw new Exception(e.Message);
                    }
                    return super;

                case "rock":
                    try
                    {
                        super.Add(Construir("fighting"));
                        super.Add(Construir("ground"));
                        super.Add(Construir("steel"));
                    }
                    catch (Exception e)
                    {
                        log.Error("Tipo.RecebeMetadeDoDano.", e);
                        throw new Exception(e.Message);
                    }
                    return super;

                case "ghost":
                    try
                    {
                        super.Add(Construir("dark"));
                    }
                    catch (Exception e)
                    {
                        log.Error("Tipo.RecebeMetadeDoDano.", e);
                        throw new Exception(e.Message);
                    }
                    return super;

                case "dark":
                    try
                    {
                        super.Add(Construir("fighting"));
                        super.Add(Construir("dark"));
                        super.Add(Construir("fairy"));
                    }
                    catch (Exception e)
                    {
                        log.Error("Tipo.RecebeMetadeDoDano.", e);
                        throw new Exception(e.Message);
                    }
                    return super;

                case "dragon":
                    try
                    {
                        super.Add(Construir("steel"));
                    }
                    catch (Exception e)
                    {
                        log.Error("Tipo.RecebeMetadeDoDano.", e);
                        throw new Exception(e.Message);
                    }
                    return super;

                case "steel":
                    try
                    {
                        super.Add(Construir("fire"));
                        super.Add(Construir("water"));
                        super.Add(Construir("electric"));
                        super.Add(Construir("steel"));
                    }
                    catch (Exception e)
                    {
                        log.Error("Tipo.RecebeMetadeDoDano.", e);
                        throw new Exception(e.Message);
                    }
                    return super;

                case "fairy":
                    try
                    {
                        super.Add(Construir("fire"));
                        super.Add(Construir("poison"));
                        super.Add(Construir("steel"));
                    }
                    catch (Exception e)
                    {
                        log.Error("Tipo.RecebeMetadeDoDano.", e);
                        throw new Exception(e.Message);
                    }
                    return super;

                default:
                    throw new Exception("Valor Não Listado");
            }
        }

        /// <summary>
        /// Lista de elementos que não recebem dano quando atacados pelo elemento instanciado.
        /// </summary>
        /// <returns></returns>
        public List<TipoDB> NaoRecebemDano()
        {
            List<TipoDB> super = new List<TipoDB>(6);
            switch (this.Nome)
            {
                case "normal":
                    try
                    {
                        super.Add(Construir("ghost"));
                    }
                    catch (Exception e)
                    {
                        log.Error("Tipo.NaoRecebemDano.", e);
                        throw new Exception(e.Message);
                    }
                    return super;

                case "electric":
                    try
                    {
                        super.Add(Construir("ground"));
                    }
                    catch (Exception e)
                    {
                        log.Error("Tipo.NaoRecebemDano.", e);
                        throw new Exception(e.Message);
                    }
                    return super;

                case "fighting":
                    try
                    {
                        super.Add(Construir("ghost"));
                    }
                    catch (Exception e)
                    {
                        log.Error("Tipo.NaoRecebemDano.", e);
                        throw new Exception(e.Message);
                    }
                    return super;

                case "poison":
                    try
                    {
                        super.Add(Construir("steel"));
                    }
                    catch (Exception e)
                    {
                        log.Error("Tipo.NaoRecebemDano.", e);
                        throw new Exception(e.Message);
                    }
                    return super;

                case "ground":
                    try
                    {
                        super.Add(Construir("flying"));
                    }
                    catch (Exception e)
                    {
                        log.Error("Tipo.NaoRecebemDano.", e);
                        throw new Exception(e.Message);
                    }
                    return super;

                case "psychic":
                    try
                    {
                        super.Add(Construir("dark"));
                    }
                    catch (Exception e)
                    {
                        log.Error("Tipo.NaoRecebemDano.", e);
                        throw new Exception(e.Message);
                    }
                    return super;

                case "ghost":
                    try
                    {
                        super.Add(Construir("normal"));
                    }
                    catch (Exception e)
                    {
                        log.Error("Tipo.NaoRecebemDano.", e);
                        throw new Exception(e.Message);
                    }
                    return super;

                case "dragon":
                    try
                    {
                        super.Add(Construir("fairy"));
                    }
                    catch (Exception e)
                    {
                        log.Error("Tipo.NaoRecebemDano.", e);
                        throw new Exception(e.Message);
                    }
                    return super;

                default:
                    throw new Exception("Valor Não Listado");
            }

        }

        #endregion Recebendo Dano


        /// <summary>
        /// Lista Pokemons com o elemento Instanciado
        /// </summary>
        /// <returns></returns>
        public List<PokemonDB> GetPokemons()
        {
            List<PokemonDB> listaPoke = new List<PokemonDB>();

            try
            {
                //gabriel providenciar select no banco que retorna lista de pokemons de determinado elemento
                Banco.BancoSelect.BuscaNoBanco(Nome);

                return listaPoke;
            }
            catch (Exception e)
            {
                int i = IdAPITipo(Nome);
                RestSharp.IRestResponse resposta = Controllers.Outros.BaixarInfoAPI($"https://pokeapi.co/api/v2/type/{i}/");

                if (resposta.IsSuccessful)
                {
                    Type type = Newtonsoft.Json.JsonConvert.DeserializeObject<Type>(resposta.Content);

                    foreach (var item in type.Pokemon)
                    {
                        //PokemonDB poke = PokemonDB.


                    }



                }
                else
                {
                    log.Error($"erro ao pegar pokemons na api.<<{resposta.Content}>>");
                    throw new Exception($"erro ao pegar pokemons na api.<<{resposta.Content}>>");
                }
                return listaPoke;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<PokemonDB> GetMoves()
        {
            return new List<PokemonDB>();
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}

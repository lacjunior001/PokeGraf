using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
    }
}

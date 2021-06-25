using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PokeGraf.Models
{
    /// <summary>
    /// Status do pokemon.
    /// </summary>
    public class StatusDB
    {
        public int IdStatus { get; private set; }
        public string Nome { get; private set; }
        public int ValorBase { get; private set; }
        public int ValorMinimo { get; private set; }
        public int ValorMax { get; private set; }

        /// <summary>
        /// Construtor.
        /// </summary>
        /// <param name="idStatus"></param>
        /// <param name="nome"></param>
        /// <param name="valorBase"></param>
        /// <param name="valorMinimo"></param>
        /// <param name="valorMax"></param>
        public StatusDB(int idStatus, string nome, int valorBase, int valorMinimo, int valorMax)
        {
            IdStatus = idStatus;
            Nome = nome;
            ValorBase = valorBase;
            ValorMinimo = valorMinimo;
            ValorMax = valorMax;
        }




    }
}

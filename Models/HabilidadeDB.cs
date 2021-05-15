using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PokeGraf.Models
{
    /// <summary>
    /// Habilidade e level que o pokemon aprende a habilidade.
    /// </summary>
    public class HabilidadeDB
    {
        public int IdHabilidade { get; private set; }
        public string Nome { get; private set; }
        public int Level { get; private set; }
        public TipoDB Tipo { get; private set; }

        /// <summary>
        /// Construtor.
        /// </summary>
        /// <param name="idHabilidade"></param>
        /// <param name="nome"></param>
        /// <param name="level"></param>
        /// <param name="tipo"></param>
        public HabilidadeDB(int idHabilidade, string nome, int level, TipoDB tipo)
        {
            IdHabilidade = idHabilidade;
            Nome = nome;
            Level = level;
            Tipo = tipo;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PokeGraf.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class GeneroDB
    {
        public int IdGenero { get; private set; }
        public string Nome { get; private set; }
        public double Probabilidade { get; private set; }

        public GeneroDB(int idGenero, string nome, double probabilidade)
        {
            IdGenero = idGenero;
            Nome = nome;
            Probabilidade = probabilidade;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PokeGraf.Models
{
    public class NaturezaDB
    {
        public int IdNatureza { get; private set; }
        public string Nome { get; private set; }

        public NaturezaDB(int idNatureza, string nome)
        {
            IdNatureza = idNatureza;
            Nome = nome;
        }
    }
}

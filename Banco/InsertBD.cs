using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.Data;

namespace PokeGraf.Banco
{
    class InsertBD
    {
        static IniciaBanco databasestart = new IniciaBanco();
        public void InsertDataPokemon(string NOME, double ALTURA, double PESO, string DESCRICAO, string RARIDADE, string SPRITE)
        {
            SQLiteConnection connect = databasestart.GetConnection();
            connect.Open();

            SQLiteCommand command = new SQLiteCommand(@"INSERT INTO Pokemon (nomePokemon, alturaPokemon, pesoPokemon, descricaoPokemon, raridadePokemon, spritePokemon) VALUES ( @nome, @altura, @peso, @descricao, @raridade, @sprite )", connect);
            command.Parameters.AddWithValue(@"nome", NOME);                    
            command.Parameters.AddWithValue(@"altura", ALTURA);
            command.Parameters.AddWithValue(@"peso", PESO);
            command.Parameters.AddWithValue(@"descricao", DESCRICAO);
            command.Parameters.AddWithValue(@"raridade", RARIDADE);
            command.Parameters.AddWithValue(@"sprite", SPRITE);

            command.ExecuteNonQuery();

            connect.Close();
        }

        public void InsertDataTipo(string NOME)
        {
            SQLiteConnection connect = databasestart.GetConnection();
            connect.Open();

            SQLiteCommand command = new SQLiteCommand(@"INSERT INTO Tipo (nomeTipo) VALUES (@nome)", connect);
            command.Parameters.AddWithValue(@"nome", NOME);

            command.ExecuteNonQuery();

            connect.Close();
        }

        public void InsertDataTipoPokemon(string POKEMON, string TIPO)
        {
            BancoSelect select = new BancoSelect();
            int pkTipo = select.RetornarPKTipo(TIPO);
            int pkPokemon = select.RetornarPKPokemon(POKEMON);
            SQLiteConnection connect = databasestart.GetConnection();
            connect.Open();
            
            SQLiteCommand command = new SQLiteCommand(@"INSERT INTO RelacaoTipoPokemon (IDTipo, IDPokemon) VALUES (@TIPO, @POKEMON)", connect);
            command.Parameters.AddWithValue(@"TIPO", pkTipo);
            command.Parameters.AddWithValue(@"POKEMON", pkPokemon);

            command.ExecuteNonQuery();

            connect.Close();
        }
    }
}

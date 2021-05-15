using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.Data;

namespace PokeGraf.Banco
{
    public class BancoSelect
    {
        static IniciaBanco databasestart = new IniciaBanco();

        public static void BuscaNoBanco(string item)
        {
            throw new Exception("Não Existe Banco");
        }

        public int RetornarQuantidade(int TIPO)
        {
            Models.PokemonDB pokemon = new Models.PokemonDB();
            //int pkTipo = RetornarPKTipo(TIPO);
            int i = 0;
            int[] id = new int[i];
            SQLiteConnection connect = databasestart.GetConnection();
            connect.Open();

            SQLiteCommand command = new SQLiteCommand(@"SELECT IDPokemon FROM RelacaoTipoPokemon WHERE IDTipo = @IDTipo", connect);
            command.Parameters.AddWithValue(@"IDTipo", TIPO);
            SQLiteDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                id[i] = reader.GetInt32(0);
                i++;
            }
            connect.Close();
            int quantidade = id.Length;
            return quantidade;
        }

        public string RetornarNomeTipo(int TIPO)
        {
            Models.TipoDB tipo = new Models.TipoDB();

            string nomeTipo = "";
            SQLiteConnection connect = databasestart.GetConnection();
            connect.Open();

            SQLiteCommand command = new SQLiteCommand(@"SELECT nomeTipo FROM Tipo WHERE ID=@IDTipo", connect);
            command.Parameters.AddWithValue(@"IDTipo", TIPO);
            SQLiteDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                nomeTipo = reader.GetString(0);
            }
            connect.Close();

            return nomeTipo;
        }

        public int RetornarPKPokemon(string NOME)
        {
            Models.PokemonDB pokemon = new Models.PokemonDB();

            SQLiteConnection connect = databasestart.GetConnection();
            connect.Open();

            SQLiteCommand command = new SQLiteCommand(@"SELECT ID FROM Pokemon WHERE nomePOKEMON = @nome", connect);
            command.Parameters.AddWithValue(@"nome", NOME);
            SQLiteDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                pokemon.IDPokemon = reader.GetInt32(0);
            }
            connect.Close();
            return pokemon.IDPokemon;
        }

        public void RetornarPokemon(string NOME)
        {
            Models.PokemonDB pokemon = new Models.PokemonDB();

            SQLiteConnection connect = databasestart.GetConnection();
            connect.Open();

            SQLiteCommand command = new SQLiteCommand(@"SELECT * FROM Pokemon", connect);
            SQLiteDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                pokemon.IDPokemon = reader.GetInt32(0);
                pokemon.NomePokemon = reader.GetString(1);
                pokemon.AlturaPokemon = reader.GetDouble(2);
                pokemon.PesoPokemon = reader.GetDouble(3);
                pokemon.DescricaoPokemon = reader.GetString(4);
                pokemon.RaridadePokemon = reader.GetString(5);
                pokemon.SpritePokemon = reader.GetString(6);
            }
            connect.Close();
        }

        public int RetornarPKTipo(string NOME)
        {
            Models.TipoDB tipo = new Models.TipoDB();

            SQLiteConnection connect = databasestart.GetConnection();
            connect.Open();

            SQLiteCommand command = new SQLiteCommand(@"SELECT ID FROM Tipo WHERE nomeTipo=@nome", connect);
            command.Parameters.AddWithValue(@"nome", NOME);
            SQLiteDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                tipo.idTipo = reader.GetInt32(0);
            }
            connect.Close();
            return tipo.idTipo;
        }
        public void RetornarTipo(string NOME)
        {
            Models.TipoDB tipo = new Models.TipoDB();

            SQLiteConnection connect = databasestart.GetConnection();
            connect.Open();

            SQLiteCommand command = new SQLiteCommand(@"SELECT * FROM Tipo", connect);
            SQLiteDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                tipo.idTipo = reader.GetInt32(0);
                tipo.nomeTipo = reader.GetString(1);
            }
            connect.Close();
        }

        public string RetornarElementoPokeon(string NOME)
        {
            int ID = RetornarPKPokemon(NOME);
            SQLiteConnection connect = databasestart.GetConnection();
            connect.Open();

            SQLiteCommand command = new SQLiteCommand(@"SELECT IDTipo FROM RelacaoTipoPokemon WHERE IDPokemon=@ID", connect);
            command.Parameters.AddWithValue(@"ID", ID);
            SQLiteDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                int idTipo = reader.GetInt32(0);
            }
            connect.Close();

            return "achou";
        }
    }
}

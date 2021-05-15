using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Data.SQLite;
using System.Data;


namespace PokeGraf.Banco
{
    public class BancoTabelas
    {
        static IniciaBanco databasestart = new IniciaBanco();

        public void CreateTablePokemon()
        {
            SQLiteConnection connect = databasestart.GetConnection();
            connect.Open();
            string query = @"CREATE TABLE IF NOT EXISTS Pokemon (
                            ID INTEGER,
                            nomePokemon VARCHAR(15),
                            alturaPokemon double,
                            pesoPokemon double,
                            descricaoPokemon VARCHAR(300),
                            raridadePokemon VARCHAR(15),
                            spritePokemon VARCHAR(300),
                            PRIMARY KEY (ID AUTOINCREMENT)
                        )";
            SQLiteCommand command = new SQLiteCommand(query, connect);
            command.ExecuteNonQuery();
            connect.Close();
        }

       /* public void CreateTableRegiao()
        {
            SQLiteConnection connect = databasestart.GetConnection();
            connect.Open();
            string query = @"CREATE TABLE IF NOT EXISTS Regiao (
                            ID INTEGER,
                            nomeRegiao VARCHAR(30),
                            PRIMARY KEY (ID AUTOINCREMENT)
                        )";
            SQLiteCommand command = new SQLiteCommand(query, connect);
            command.ExecuteNonQuery();
            connect.Close();
        }*/

        public void CreateTableTipo()
        {
            SQLiteConnection connect = databasestart.GetConnection();
            connect.Open();
            string query = @"CREATE TABLE IF NOT EXISTS Tipo (
                            ID INTEGER,
                            nomeTipo VARCHAR(30),
                            PRIMARY KEY (ID AUTOINCREMENT)
                        )";
            SQLiteCommand command = new SQLiteCommand(query, connect);
            command.ExecuteNonQuery();
            connect.Close();
        }

        public void CreateRelacaoTipoPokemon()
        {
            SQLiteConnection connect = databasestart.GetConnection();
            connect.Open();
            string query = @"CREATE TABLE IF NOT EXISTS RelacaoTipoPokemon (
                            ID INTEGER,
                            IDTipo INTEGER,
                            IDPokemon INTEGER,
                            PRIMARY KEY (ID AUTOINCREMENT),
                            FOREIGN KEY (IDTipo) REFERENCES Tipo (ID),
                            FOREIGN KEY (IDPokemon) REFERENCES Pokemon (ID)                            
                        )";
            SQLiteCommand command = new SQLiteCommand(query, connect);
            command.ExecuteNonQuery();
            connect.Close();
        }
        /* public void CreateTableStatus()
         {
             SQLiteConnection connect = databasestart.GetConnection();
             connect.Open();
             string query = @"CREATE TABLE IF NOT EXISTS Status (
                             ID INTEGER,
                             nomeAtributo VARCHAR(15),
                             valorAtributo INTEGER,
                             valorMax INTEGER,
                             valorMin INTEGER,
                             PRIMARY KEY (ID AUTOINCREMENT)
                         )";
             SQLiteCommand command = new SQLiteCommand(query, connect);
             command.ExecuteNonQuery();
             connect.Close();
         }
         public void CreateTableNature()
         {
             SQLiteConnection connect = databasestart.GetConnection();
             connect.Open();
             string query = @"CREATE TABLE IF NOT EXISTS Nature (
                             ID INTEGER,
                             nomeNature VARCHAR(30),
                             PRIMARY KEY (ID AUTOINCREMENT)
                         )";
             SQLiteCommand command = new SQLiteCommand(query, connect);
             command.ExecuteNonQuery();
             connect.Close();
         }
         public void CreateTableGender()
         {
             SQLiteConnection connect = databasestart.GetConnection();
             connect.Open();
             string query = @"CREATE TABLE IF NOT EXISTS Gender (
                             ID INTEGER,
                             nomeGender VARCHAR(15),
                             PRIMARY KEY (ID AUTOINCREMENT)
                         )";
             SQLiteCommand command = new SQLiteCommand(query, connect);
             command.ExecuteNonQuery();
             connect.Close();
         }

         public void CreateTableMove()
         {
             SQLiteConnection connect = databasestart.GetConnection();
             connect.Open();
             string query = @"CREATE TABLE IF NOT EXISTS Move (
                             ID INTEGER,
                             nomeMove VARCHAR(30),
                             tipoMove VARCHAR(30),
                             PRIMARY KEY (ID AUTOINCREMENT)
                         )";
             SQLiteCommand command = new SQLiteCommand(query, connect);
             command.ExecuteNonQuery();
             connect.Close();
         }*/
    }
}
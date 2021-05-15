using System;

namespace PokeGraf.Banco
{
    class Program
    {
        static void trestes()
        {
            BancoTabelas create = new BancoTabelas();
            
            create.CreateTablePokemon();
            create.CreateTableTipo();
            create.CreateRelacaoTipoPokemon();

            InsertBD insert = new InsertBD();

            /*BancoSelect select = new BancoSelect();
            insert.InsertDataTipoPokemon("Gabriel", "Água");
            select.RetornarElementoPokeon("Gabriel");*/

            RestSharp.RestClient pokeApiCliente;
            RestSharp.RestRequest pokeApiRequest;

            for (int i = 0; i < 19; i++)
            {
                pokeApiCliente = new RestSharp.RestClient($"https://pokeapi.co/api/v2/type/{i}/");
                pokeApiRequest = new RestSharp.RestRequest(RestSharp.Method.GET);
                var resultado = pokeApiCliente.Execute(pokeApiRequest);
                if (resultado.IsSuccessful)
                {
                    Models.Type tipo = Newtonsoft.Json.JsonConvert.DeserializeObject<Models.Type>(resultado.Content);
                    insert.InsertDataTipo(tipo.Name);

                    foreach (var item in tipo.Pokemon)
                    {
                        pokeApiCliente = new RestSharp.RestClient(item.Pokemon.Url);
                        pokeApiRequest = new RestSharp.RestRequest(RestSharp.Method.GET);
                        resultado = pokeApiCliente.Execute(pokeApiRequest);
                        Models.Pokemon poke = Newtonsoft.Json.JsonConvert.DeserializeObject<Models.Pokemon>(resultado.Content);

                        //Inserir no banco as infos do pokemon
                        string nome = poke.Name;
                        int peso = poke.Weight;
                        int altura = poke.Height;
                        string pokeElemento;

                        foreach (var tipoo in poke.Types)
                        {
                            pokeElemento = tipoo.Type.Name;
                        }
                        string sprite = poke.Sprites.FrontDefault;

                        pokeApiCliente = new RestSharp.RestClient(poke.Species.Url);
                        pokeApiRequest = new RestSharp.RestRequest(RestSharp.Method.GET);
                        resultado = pokeApiCliente.Execute(pokeApiRequest);
                        Models.PokemonSpecies pokeS = Newtonsoft.Json.JsonConvert.DeserializeObject<Models.PokemonSpecies>(resultado.Content);

                        //resto das infos
                        int idPoke = pokeS.Order;
                        string descricao = pokeS.FlavorTextEntries[0].FlavorText;
                        string raridade = "epic";
                        if (pokeS.IsBaby)
                        {
                            raridade = "Baby";
                        }
                        else if (pokeS.IsLegendary)
                        {
                            raridade = "Legendary";
                        }
                        else if (pokeS.IsMythical)
                        {
                            raridade = "Mythical";
                        }
                        else
                        {
                            raridade = "Commom";
                        }

                        insert.InsertDataPokemon(nome, altura, peso, descricao, raridade, sprite);

                        //pega o primeira aparição
                        //https://pokeapi.co/api/v2/generation/

                    }
                }
            }
           
        }
    }
}

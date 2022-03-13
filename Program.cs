using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Refit;

namespace PokeAPI
{
    class Program
    {
        //Gerador do arquivo
        static async Task Main(string[] args)
        {
            //Calcula o tempo de execucao
            Stopwatch swt = new Stopwatch();

            swt.Start();

            //Aguarda a lista com os dados dos pokemons
            List<Pokemon> printPokemon = await GetPokemons();
            //Cria o txt
            StreamWriter sw = new StreamWriter("../../../ListaPokemon.txt");
            //Grava dos dados no txt
            sw.WriteLine(JsonConvert.SerializeObject(printPokemon));

            sw.Close();

            swt.Stop();

            Console.WriteLine("\nA Tarefa foi finalizada {0}", swt.ElapsedMilliseconds / 1000.0);
        }

        //Gera a lista de pokemons
        static async Task<List<Pokemon>> GetPokemons()
        {
            string[] pokemons = { "charmander", "squirtle", "caterpie", "weedle", "pidgey", "pidgeotto", "rattata", "spearow", "fearow", "arbok", "pikachu", "sandshrew" };

            List<Pokemon> printPokemon = new List<Pokemon>();

            Parallel.ForEach(pokemons, PokeL =>
            {
                var response = disparaReq(PokeL);

                printPokemon.Add(response.Result);

            });
            return printPokemon;
        }
        //Realiza a consulta dos pokemons
        static async Task<Pokemon> disparaReq(string PokeL)
        {
            var PokemonList = RestService.For<IPokemonApiService>("https://pokeapi.co/api/v2");

            Console.WriteLine("Consultando Pokemon informado: " + PokeL);

            var poke = await PokemonList.GetPokemonAsync(PokeL);

            var habilidadeStr = "";

            poke.abilities.ForEach(habilidade =>
            {
                habilidadeStr += habilidade.ability.name + ", ";
            });

            var tipoStr = "";

            poke.types.ForEach(type =>
            {
                tipoStr += type.type.name + ", ";
            });

            Console.Write($"\nNome:{poke.name}\nTipo:{tipoStr}\nHabilidades:{habilidadeStr}\nPeso:{poke.weight}\nTamanho:{poke.height}\nImagem:{poke.sprites.front_default}");

            //Cria Pasta
            Directory.CreateDirectory("../../../ImagensPokemons");

            //Download da Imagem 
            WebClient webClient = new WebClient();
            webClient.DownloadFile(poke.sprites.front_default, $@"../../../ImagensPokemons/{poke.name}.jpg");

            return poke;
        }
    }
}

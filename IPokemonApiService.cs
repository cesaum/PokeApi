using Refit;
using System;
using System.Threading.Tasks;

namespace PokeAPI
{
    public interface IPokemonApiService
    {
        [Get("/pokemon/{pokem}")]
        Task<Pokemon> GetPokemonAsync(String pokem);

    }
}

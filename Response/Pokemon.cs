using System.Collections.Generic;

namespace PokeAPI
{

    public class Pokemon
    {
        public string name { get; set; }

        public List<PokemonType> types { get; set; }

        public List<PokemonAbility> abilities { get; set; }

        public int weight { get; set; }

        public int height { get; set; }

        public PokemonSprites sprites { get; set; }

    }
}

using AdapterPattern.Basic.Model;
using System.Collections.Generic;

namespace AdapterPattern.Basic
{
    public class SuperheroConverter
    {
        private readonly ISuperheroRendererAdapter _rendererAdapter;

        public SuperheroConverter(ISuperheroRendererAdapter rendererAdapter)
        {
            _rendererAdapter = rendererAdapter;
        }

        public SuperheroConverter(): this(new SuperHeroRendererAdapter()) { }

        public string ToString(List<Superhero> heroes)
        {
            return _rendererAdapter.ToString(heroes);
        }
    }
}
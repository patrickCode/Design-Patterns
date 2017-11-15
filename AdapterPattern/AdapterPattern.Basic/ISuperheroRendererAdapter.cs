using AdapterPattern.Basic.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace AdapterPattern.Basic
{
    public interface ISuperheroRendererAdapter
    {
        string ToString(List<Superhero> heroes);
    }
}

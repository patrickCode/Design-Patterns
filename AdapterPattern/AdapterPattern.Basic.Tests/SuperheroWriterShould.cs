using AdapterPattern.Basic.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace AdapterPattern.Basic.Tests
{
    [TestClass]
    public class SuperheroConverterShould
    {
        [TestMethod]
        public void ShouldConvertOneSupoerHeroToString()
        {
            var converter = new SuperheroConverter();
            var superheroes = new List<Superhero>()
            {
                new Superhero()
                {
                    Id = 1,
                    Alias = "Bruce Wayne",
                    Name = "Batman"
                }
            };

            var str = converter.ToString(superheroes);
            Console.Write(str);

            Assert.IsNotNull(str);
        }

        [TestMethod]
        public void ShouldConvertThreeSupoerHeroToString()
        {
            var converter = new SuperheroConverter();
            var superheroes = new List<Superhero>()
            {
                new Superhero()
                {
                    Id = 1,
                    Alias = "Bruce Wayne",
                    Name = "Batman"
                },
                new Superhero()
                {
                    Id = 2,
                    Alias = "Clark Kent",
                    Name = "Superman"
                },
                new Superhero()
                {
                    Id = 3,
                    Alias = "Diana Lane",
                    Name = "Wonder Woman"
                }
            };

            var str = converter.ToString(superheroes);
            Console.Write(str);

            Assert.IsNotNull(str);
        }
    }
}

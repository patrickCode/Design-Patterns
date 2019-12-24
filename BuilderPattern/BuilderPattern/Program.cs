using System;

namespace BuilderPattern
{
    class Program
    {
        static void Main(string[] args)
        {
            var clubsandwichBuilder = new ClubSandwichBuilder();
            var sandwichMaker = new SandwichMaker(clubsandwichBuilder);

            var sandwich = sandwichMaker.BuildSandwich();
            sandwich.Display();
        }
    }
}

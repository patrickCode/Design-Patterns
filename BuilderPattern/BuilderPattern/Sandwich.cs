using System;
using System.Collections.Generic;

namespace BuilderPattern
{
    public class Sandwich
    {
        public BreadType Bread { get; set; }
        public CheeseType Cheese { get; set; }
        public MeatType Meat { get; set; }
        public bool HasMayo { get; set; }
        public bool HasMustard { get; set; }
        public List<string> Vegetables { get; set; }

        public void Display()
        {
            Console.WriteLine("SANDWICH");
            Console.WriteLine($"\tCBread: {Bread.ToString()}");
            Console.WriteLine($"\tCheese: {Cheese.ToString()}");
            Console.WriteLine($"\tMeat: {Meat.ToString()}");
            Console.WriteLine($"\tVegetables: {string.Join(',', Vegetables)}");
        }
    }

    public enum BreadType
    {
        Oregano,
        Parmessan,
        White
    }

    public enum MeatType
    {
        Chicken,
        Lamb,
        Ham
    }

    public enum CheeseType
    {
        American,
        Swiss,
        Cheddar
    }
}

using System.Collections.Generic;

namespace BuilderPattern
{
    /*
     * This class knows that the specific ingridients for making a Club Sandwich
     * Data for making a Club Sandwich is present here
     */
    public class ClubSandwichBuilder : SandwichBuilder
    {
        public override void AddCondiments()
        {
            sandwich.HasMayo = true;
            sandwich.HasMustard = false;
        }

        public override void ApplyMeatAndCheese()
        {
            sandwich.Meat = MeatType.Chicken;
            sandwich.Cheese = CheeseType.Cheddar;
        }

        public override void ApplyVegetables()
        {
            sandwich.Vegetables = new List<string>() { "Tomato", "Onion" };
        }

        public override void PrepareBread()
        {
            sandwich.Bread = BreadType.White;
        }
    }
}

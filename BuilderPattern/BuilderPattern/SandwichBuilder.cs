using System;
using System.Collections.Generic;
using System.Text;

namespace BuilderPattern
{
    /*
     * This class knows what the different ingridients needed for making a specific type of sandwich.
     * This will enforce the structure of the object
     */
    public abstract class SandwichBuilder
    {
        protected Sandwich sandwich;

        public Sandwich GetSandwich() => sandwich;

        public void CreateSandwich()
        {
            sandwich = new Sandwich();
        }

        public abstract void PrepareBread();
        public abstract void ApplyMeatAndCheese();
        public abstract void ApplyVegetables();
        public abstract void AddCondiments();
    }
}

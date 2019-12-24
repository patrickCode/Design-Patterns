namespace BuilderPattern
{
    /*
     * This class knows how to make a sandwich, i.e. which steps to be followed after which.
     * For e.g. first the bread needs to be chosen, then the meat and cheese has to be applied, etc.
     * So this class will use one the builders (which knows what the exact ingridients of the sandwich), and use it make the final sandwich.
     * This will contain the logic for actually making a sandwich
     */
    public class SandwichMaker
    {
        private readonly SandwichBuilder _builder;

        public SandwichMaker(SandwichBuilder builder)
        {
            _builder = builder;
        }

        public Sandwich BuildSandwich()
        {
            _builder.CreateSandwich();
            _builder.PrepareBread();
            _builder.ApplyMeatAndCheese();
            _builder.ApplyVegetables();
            _builder.AddCondiments();
            return _builder.GetSandwich();
        }
    }
}

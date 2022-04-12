namespace DesignPatterns.CreationalPatterns.AbstractFactory.RealWorld
{
    // The Abstract Factory design pattern provides an interface for creating families of related or
    // dependent objects without specifying their concrete classes.

    class Example
    {
        public static void Run()
        {
            AnimalWorld world;

            world = new(new AfricaFactory());
            world.RunFoodChain();

            world = new(new AmericaFactory());
            world.RunFoodChain();

            Console.WriteLine();
        }
    }

    // AbstractFactory (ContinentFactory)

    abstract class ContinentFactory
    {
        public abstract Herbivore CreateHerbivore();
        public abstract Carnivore CreateCarnivore();
    }

    // ConcreteFactory (AfricaFactory, AmericaFactory)

    class AfricaFactory : ContinentFactory
    {
        public override Herbivore CreateHerbivore() => new Wildebeest();
        public override Carnivore CreateCarnivore() => new Lion();
    }

    class AmericaFactory : ContinentFactory
    {
        public override Herbivore CreateHerbivore() => new Bison();
        public override Carnivore CreateCarnivore() => new Wolf();
    }

    // AbstractProduct (Herbivore, Carnivore)

    abstract class Herbivore
    {
        public override string ToString() => GetType().Name;
    }

    abstract class Carnivore
    {
        public void Eat(Herbivore herbivore) => Console.WriteLine($"{this} eats {herbivore}");

        public override string ToString() => GetType().Name;
    }

    // Product (Wildebeest, Lion, Bison, Wolf)

    class Wildebeest : Herbivore
    {
    }

    class Bison : Herbivore
    {
    }

    class Lion : Carnivore
    {
    }

    class Wolf : Carnivore
    {
    }

    // Client (AnimalWorld)

    class AnimalWorld
    {
        private readonly Herbivore _herbivore;
        private readonly Carnivore _carnivore;

        public AnimalWorld(ContinentFactory factory)
        {
            _herbivore = factory.CreateHerbivore();
            _carnivore = factory.CreateCarnivore();
        }

        public void RunFoodChain() => _carnivore.Eat(_herbivore);
    }
}

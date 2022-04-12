namespace DesignPatterns.CreationalPatterns.AbstractFactory.Structural
{
    // The Abstract Factory design pattern provides an interface for creating families of related or
    // dependent objects without specifying their concrete classes.

    class Example
    {
        public static void Run()
        {
            Client client;

            client = new(new ConcreteFactory1());
            client.Run();

            client = new(new ConcreteFactory2());
            client.Run();

            Console.WriteLine();
        }
    }

    // Abstract Factory

    abstract class AbstractFactory
    {
        public abstract AbstractProductA CreateProductA();
        public abstract AbstractProductB CreateProductB();
    }

    // Concrete Factory

    class ConcreteFactory1 : AbstractFactory
    {
        public override AbstractProductA CreateProductA() => new ProductA1();
        public override AbstractProductB CreateProductB() => new ProductB1();
    }

    class ConcreteFactory2 : AbstractFactory
    {
        public override AbstractProductA CreateProductA() => new ProductA2();
        public override AbstractProductB CreateProductB() => new ProductB2();
    }

    // Abstract Product

    abstract class AbstractProductA
    {
        public override string ToString() => GetType().Name;
    }

    abstract class AbstractProductB
    {
        public void Interact(AbstractProductA a) => Console.WriteLine($"{this} interacts with {a}");

        public override string ToString() => GetType().Name;
    }

    // Product

    class ProductA1 : AbstractProductA
    {
    }

    class ProductA2 : AbstractProductA
    {
    }

    class ProductB1 : AbstractProductB
    {
    }

    class ProductB2 : AbstractProductB
    {
    }

    // Client

    class Client
    {
        private readonly AbstractProductA _productA;
        private readonly AbstractProductB _productB;

        public Client(AbstractFactory factory)
        {
            _productA = factory.CreateProductA();
            _productB = factory.CreateProductB();
        }

        public void Run() => _productB.Interact(_productA);
    }
}

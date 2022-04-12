namespace DesignPatterns.CreationalPatterns.FactoryMethod.Structural
{
    // The Factory Method design pattern defines an interface for creating an
    // object, but let subclasses decide which class to instantiate. This pattern lets a
    // class defer instantiation to subclasses.

    class Example
    {
        public static void Run()
        {
            List<Creator> creators = new()
            {
                new ConcreteCreatorA(),
                new ConcreteCreatorB()
            };

            foreach (Creator creator in creators)
            {
                Product product = creator.FactoryMethod();
                Console.WriteLine($"Created {product}");
            }

            Console.WriteLine();
        }
    }

    // Product

    abstract class Product
    {
        public override string ToString() => GetType().Name;
    }

    // ConcreteProduct

    class ConcreteProductA : Product
    {
    }

    class ConcreteProductB : Product
    {
    }

    // Creator
    abstract class Creator
    {
        public abstract Product FactoryMethod();
    }

    // ConcreteCreator

    class ConcreteCreatorA : Creator
    {
        public override Product FactoryMethod() => new ConcreteProductA();
    }

    class ConcreteCreatorB : Creator
    {
        public override Product FactoryMethod() => new ConcreteProductB();
    }
}

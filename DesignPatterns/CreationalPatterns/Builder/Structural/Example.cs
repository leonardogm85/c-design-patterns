namespace DesignPatterns.CreationalPatterns.Builder.Structural
{
    // The Builder design pattern separates the construction of a complex object from its
    // representation so that the same construction process can create different representations.

    class Example
    {
        public static void Run()
        {
            Builder b1 = new ConcreteBuilder1();
            Director.Construct(b1);
            b1.GetResult().Show();

            Console.WriteLine();

            Builder b2 = new ConcreteBuilder2();
            Director.Construct(b2);
            b2.GetResult().Show();
        }
    }

    // Builder

    abstract class Builder
    {
        public abstract void BuildPartA();
        public abstract void BuildPartB();
        public abstract Product GetResult();
    }

    // ConcreteBuilder

    class ConcreteBuilder1 : Builder
    {
        private readonly Product _product = new();

        public override void BuildPartA() => _product.Add("PartA");
        public override void BuildPartB() => _product.Add("PartB");

        public override Product GetResult() => _product;
    }

    class ConcreteBuilder2 : Builder
    {
        private readonly Product _product = new();

        public override void BuildPartA() => _product.Add("PartX");
        public override void BuildPartB() => _product.Add("PartY");

        public override Product GetResult() => _product;
    }

    // Director

    static class Director
    {
        public static void Construct(Builder builder)
        {
            builder.BuildPartA();
            builder.BuildPartB();
        }
    }

    // Product

    class Product
    {
        private readonly List<string> _parts = new();

        public void Add(string part) => _parts.Add(part);

        public void Show()
        {
            Console.WriteLine("Product Parts:");

            foreach (string part in _parts)
            {
                Console.WriteLine($"  {part}");
            }
        }
    }
}

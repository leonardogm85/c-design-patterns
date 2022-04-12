namespace DesignPatterns.CreationalPatterns.Prototype.Structural
{
    // The Prototype design pattern specifies the kind of objects to create using a prototypical
    // instance, and create new objects by copying this prototype.

    class Example
    {
        public static void Run()
        {
            Prototype prototype;
            Prototype clone;

            prototype = new ConcretePrototype1("1");
            clone = prototype.Clone();
            Console.WriteLine($"Cloned: {clone}");

            prototype = new ConcretePrototype2("2");
            clone = prototype.Clone();
            Console.WriteLine($"Cloned: {clone}");

            Console.WriteLine();
        }
    }

    // Prototype

    abstract class Prototype
    {
        protected Prototype(string id) => Id = id;

        public string Id { get; private set; }

        public abstract Prototype Clone();

        public override string ToString() => Id;
    }

    // ConcretePrototype

    class ConcretePrototype1 : Prototype
    {
        public ConcretePrototype1(string id) : base(id)
        {
        }

        public override Prototype Clone() => (Prototype)MemberwiseClone();
    }

    class ConcretePrototype2 : Prototype
    {
        public ConcretePrototype2(string id) : base(id)
        {
        }

        public override Prototype Clone() => (Prototype)MemberwiseClone();
    }

    // Client

    // -
}

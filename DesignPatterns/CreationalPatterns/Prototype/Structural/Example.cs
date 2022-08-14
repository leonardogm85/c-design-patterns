namespace DesignPatterns.CreationalPatterns.Prototype.Structural
{
    // The Prototype design pattern specifies the kind of objects to create using a prototypical
    // instance, and create new objects by copying this prototype.

    class Example
    {
        public static void Run()
        {
            Prototype p1 = new ConcretePrototype1("1");
            Prototype c1 = p1.Clone();
            Console.WriteLine($"Cloned: {c1}");

            Prototype p2 = new ConcretePrototype2("2");
            Prototype c2 = p2.Clone();
            Console.WriteLine($"Cloned: {c2}");
        }
    }

    // Prototype

    abstract class Prototype
    {
        protected Prototype(string id) => Id = id;

        public string Id { get; }

        public abstract Prototype Clone();

        public override string ToString() => Id;
    }

    // ConcretePrototype

    class ConcretePrototype1 : Prototype
    {
        public ConcretePrototype1(string id) : base(id) { }

        public override Prototype Clone() => (Prototype)MemberwiseClone();
    }

    class ConcretePrototype2 : Prototype
    {
        public ConcretePrototype2(string id) : base(id) { }

        public override Prototype Clone() => (Prototype)MemberwiseClone();
    }

    // Client

    // -
}

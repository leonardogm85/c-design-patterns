namespace DesignPatterns.StructuralPatterns.Adapter.Structural
{
    // The Adapter design pattern converts the interface of a class into another interface clients
    // expect. This design pattern lets classes work together that couldn‘t otherwise because of
    // incompatible interfaces.

    class Example
    {
        public static void Run()
        {
            Target target;

            target = new Target();
            target.Request();

            target = new Adapter();
            target.Request();

            Console.WriteLine();
        }
    }

    // Target

    class Target
    {
        public virtual void Request() => Console.WriteLine("Called Target Request()");
    }

    // Adapter

    class Adapter : Target
    {
        private readonly Adaptee _adaptee = new();

        public override void Request() => _adaptee.SpecificRequest();
    }

    // Adaptee

    class Adaptee
    {
        public virtual void SpecificRequest() => Console.WriteLine("Called Adaptee SpecificRequest()");
    }

    // Client

    // -
}

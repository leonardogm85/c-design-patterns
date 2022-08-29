namespace DesignPatterns.StructuralPatterns.Bridge.Structural
{
    // The Bridge design pattern decouples an abstraction from its implementation so that the
    // two can vary independently.

    class Example
    {
        public static void Run()
        {
            Implementor implementorA = new ConcreteImplementorA();
            Implementor implementorB = new ConcreteImplementorB();

            Abstraction abstraction = new RefinedAbstraction(implementorA);
            abstraction.Operation();

            abstraction.SetImplementor(implementorB);
            abstraction.Operation();
        }
    }

    // Abstraction

    class Abstraction
    {
        public Implementor Implementor { get; private set; }

        public Abstraction(Implementor implementor) => Implementor = implementor;

        public void SetImplementor(Implementor implementor) => Implementor = implementor;

        public virtual void Operation() => Implementor.Operation();
    }

    // RefinedAbstraction

    class RefinedAbstraction : Abstraction
    {
        public RefinedAbstraction(Implementor implementor) : base(implementor) { }
    }

    // Implementor

    abstract class Implementor
    {
        public abstract void Operation();
    }

    // ConcreteImplementor

    class ConcreteImplementorA : Implementor
    {
        public override void Operation() => Console.WriteLine("ConcreteImplementorA Operation");
    }

    class ConcreteImplementorB : Implementor
    {
        public override void Operation() => Console.WriteLine("ConcreteImplementorB Operation");
    }
}

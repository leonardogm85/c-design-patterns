namespace DesignPatterns.CreationalPatterns.Singleton.Structural
{
    // The Singleton design pattern ensures a class has only one instance and
    // provide a global point of access to it.

    class Example
    {
        public static void Run()
        {
            Singleton s1 = Singleton.Instance();
            Singleton s2 = Singleton.Instance();

            if (s1 == s2)
            {
                Console.WriteLine("Objects are the same instance");
            }
        }
    }

    // Singleton

    class Singleton
    {
        private static Singleton? _instance;

        private Singleton() { }

        public static Singleton Instance() => _instance ??= new();
    }
}

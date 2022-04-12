namespace DesignPatterns.CreationalPatterns.Singleton.RealWorld
{
    // The Singleton design pattern ensures a class has only one instance and
    // provide a global point of access to it.

    class Example
    {
        public static void Run()
        {
            LoadBalancer b1 = LoadBalancer.GetLoadBalancer();
            LoadBalancer b2 = LoadBalancer.GetLoadBalancer();
            LoadBalancer b3 = LoadBalancer.GetLoadBalancer();
            LoadBalancer b4 = LoadBalancer.GetLoadBalancer();

            if (b1 == b2 && b2 == b3 && b3 == b4)
            {
                Console.WriteLine("Same instance");
                Console.WriteLine();
            }

            LoadBalancer balancer = LoadBalancer.GetLoadBalancer();

            for (int index = 0; index < 15; index++)
            {
                Console.WriteLine($"Dispatch request to: {balancer.NextServer}");
            }

            Console.WriteLine();
        }
    }

    // Singleton (LoadBalancer)

    sealed class LoadBalancer
    {
        private static readonly LoadBalancer _instance = new();

        private readonly List<Server> _servers = new();
        private readonly Random _random = new();

        private LoadBalancer()
        {
            _servers.Add(new("Server1", "120.14.220.11"));
            _servers.Add(new("Server2", "120.14.220.12"));
            _servers.Add(new("Server3", "120.14.220.13"));
            _servers.Add(new("Server4", "120.14.220.14"));
            _servers.Add(new("Server5", "120.14.220.15"));
        }

        public static LoadBalancer GetLoadBalancer() => _instance;

        public Server NextServer => _servers[_random.Next(_servers.Count)];
    }

    class Server
    {
        public Server(string name, string ip)
        {
            Name = name;
            Ip = ip;
        }

        public string Name { get; private set; }
        public string Ip { get; private set; }

        public override string ToString() => $"{Name} [{Ip}]";
    }
}

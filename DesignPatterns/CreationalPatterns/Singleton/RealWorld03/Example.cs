namespace DesignPatterns.CreationalPatterns.Singleton.RealWorld03
{
    class Example
    {
        public static void Run()
        {
            Logger.GetInstance().Write("Started!");

            var l1 = Logger.GetInstance();
            var l2 = Logger.GetInstance();

            if (l1 == l2)
            {
                Logger.GetInstance().Write("Logger has a single instance.");
            }
            else
            {
                Logger.GetInstance().Write("Loggers are different.");
            }

            var c1 = Config.GetInstance();

            c1.SetValue("login", "test_login");
            c1.SetValue("password", "test_password");

            var c2 = Config.GetInstance();

            if ("test_login" == c2.GetValue("login") && "test_password" == c2.GetValue("password"))
            {
                Logger.GetInstance().Write("Config singleton also works fine.");
            }

            Logger.GetInstance().Write("Finished!");
        }
    }

    class Logger
    {
        private static readonly Logger _instance = new();

        static Logger() { }

        private Logger() { }

        public static Logger GetInstance() => _instance;

        public void Write(string message) => Console.WriteLine($"{DateTime.Now}: {message}");
    }

    class Config
    {
        private Dictionary<string, string> _values = new();

        public string GetValue(string key) => _values[key];

        public void SetValue(string key, string value) => _values[key] = value;

        public static Config GetInstance() => Nested.Instance;

        private class Nested
        {
            internal static readonly Config Instance = new();

            static Nested() { }
        }
    }
}

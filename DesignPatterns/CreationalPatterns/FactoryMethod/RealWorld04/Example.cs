namespace DesignPatterns.CreationalPatterns.FactoryMethod.RealWorld04
{
    class Example
    {
        public static void Run()
        {
            var app = new Application();

            var info = "Very important info of the presentation.";

            Console.WriteLine("Testing Wifi:");
            app.Present(info, new WifiFactory());

            Console.WriteLine();

            Console.WriteLine("Testing Bluetooth:");
            app.Present(info, new BluetoothFactory());
        }
    }

    class Application
    {
        private Projector? _currentProjector;

        public void Present(string info, ProjectorFactory factory)
        {
            if (_currentProjector == null)
            {
                _currentProjector = factory.CreateProjector();
            }
            else
            {
                _currentProjector = factory.SyncedProjector(_currentProjector);
            }

            _currentProjector.Present(info);
        }
    }

    abstract class ProjectorFactory
    {
        public abstract Projector CreateProjector();

        public Projector SyncedProjector(Projector projector)
        {
            var newProjector = CreateProjector();
            newProjector.Sync(projector);
            return newProjector;
        }
    }

    class WifiFactory : ProjectorFactory
    {
        public override Projector CreateProjector() => new WifiProjector();
    }

    class BluetoothFactory : ProjectorFactory
    {
        public override Projector CreateProjector() => new BluetoothProjector();
    }

    abstract class Projector
    {
        public int CurrentPage { get; protected set; }

        public abstract void Present(string info);

        public abstract void Update(int page);

        public void Sync(Projector projector) => projector.Update(CurrentPage);
    }

    class WifiProjector : Projector
    {
        public override void Present(string info) => Console.WriteLine($"Info is presented over Wifi: {info}");

        public override void Update(int page) => CurrentPage = page;
    }

    class BluetoothProjector : Projector
    {
        public override void Present(string info) => Console.WriteLine($"Info is presented over Bluetooth: {info}");

        public override void Update(int page) => CurrentPage = page;
    }
}

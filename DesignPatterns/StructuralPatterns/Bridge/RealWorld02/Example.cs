namespace DesignPatterns.StructuralPatterns.Bridge.RealWorld02
{
    class Example
    {
        public static void Run()
        {
            var app = new Application();

            app.TestDevice(new Radio());

            Console.WriteLine();

            app.TestDevice(new Tv());
        }
    }

    class Application
    {
        public void TestDevice(IDevice device)
        {
            Console.WriteLine("Tests with basic remote.");
            var basicRemote = new BasicRemote(device);
            basicRemote.Power();
            device.PrintStatus();

            Console.WriteLine();

            Console.WriteLine("Tests with advanced remote.");
            var advancedRemote = new AdvancedRemote(device);
            advancedRemote.Power();
            advancedRemote.Mute();
            device.PrintStatus();
        }
    }

    /// <summary>
    /// A interface "implementação" declara métodos comuns a todas as classes concretas de implementação. Ela não precisa coincidir
    /// com a interface de abstração. Na verdade, as duas interfaces podem ser inteiramente diferentes. Tipicamente a interface de
    /// implementação fornece apenas operações primitivas, enquanto que a abstração define operações de alto nível baseada
    /// naquelas primitivas.
    /// </summary>
    interface IDevice
    {
        bool IsEnable();
        void Enable();
        void Disable();
        int GetVolume();
        void SetVolume(int volume);
        int GetChannel();
        void SetChannel(int channel);
        void PrintStatus();
    }

    // Todos os dispositivos seguem a mesma interface.

    class Radio : IDevice
    {
        private bool _on = false;
        private int _volume = 30;
        private int _channel = 1;

        public bool IsEnable() => _on;

        public void Enable() => _on = true;

        public void Disable() => _on = false;

        public int GetVolume() => _volume;

        public void SetVolume(int volume)
        {
            if (volume > 100)
            {
                _volume = 100;
            }
            else if (volume < 0)
            {
                _volume = 0;
            }
            else
            {
                _volume = volume;
            }
        }

        public int GetChannel() => _channel;

        public void SetChannel(int channel) => _channel = channel;

        public void PrintStatus()
        {
            Console.WriteLine("--------------------------------------------------");
            Console.WriteLine("| I'm Radio.");
            Console.WriteLine($"| I'm {(IsEnable() ? "enabled" : "disabled")}.");
            Console.WriteLine($"| Current volume is {GetVolume()}%.");
            Console.WriteLine($"| Current channel is {GetChannel()}.");
            Console.WriteLine("--------------------------------------------------");
        }
    }

    class Tv : IDevice
    {
        private bool _on = false;
        private int _volume = 30;
        private int _channel = 1;

        public bool IsEnable() => _on;

        public void Enable() => _on = true;

        public void Disable() => _on = false;

        public int GetVolume() => _volume;

        public void SetVolume(int volume)
        {
            if (volume > 100)
            {
                _volume = 100;
            }
            else if (volume < 0)
            {
                _volume = 0;
            }
            else
            {
                _volume = volume;
            }
        }

        public int GetChannel() => _channel;

        public void SetChannel(int channel) => _channel = channel;

        public void PrintStatus()
        {
            Console.WriteLine("--------------------------------------------------");
            Console.WriteLine("| I'm TV set.");
            Console.WriteLine($"| I'm {(IsEnable() ? "enabled" : "disabled")}.");
            Console.WriteLine($"| Current volume is {GetVolume()}%.");
            Console.WriteLine($"| Current channel is {GetChannel()}.");
            Console.WriteLine("--------------------------------------------------");
        }
    }

    /// <summary>
    /// A "abstração" define a interface para a parte "controle" das duas hierarquias de classe. Ela mantém uma referência a um
    /// objeto da hierarquia de "implementação" e delega todo o trabalho real para esse objeto.
    /// </summary>
    interface IRemote
    {
        void Power();
        void VolumeDown();
        void VolumeUp();
        void ChannelDown();
        void ChannelUp();
    }

    class BasicRemote : IRemote
    {
        protected readonly IDevice _device;

        public BasicRemote(IDevice device) => _device = device;

        public void Power()
        {
            Console.WriteLine("Remote: power toggle.");

            if (_device.IsEnable())
            {
                _device.Disable();
            }
            else
            {
                _device.Enable();
            }
        }

        public void VolumeDown()
        {
            Console.WriteLine("Remote: volume down.");
            _device.SetVolume(_device.GetVolume() - 10);
        }

        public void VolumeUp()
        {
            Console.WriteLine("Remote: volume up.");
            _device.SetVolume(_device.GetVolume() + 10);
        }

        public void ChannelDown()
        {
            Console.WriteLine("Remote: channel down.");
            _device.SetChannel(_device.GetChannel() - 1);
        }

        public void ChannelUp()
        {
            Console.WriteLine("Remote: channel up.");
            _device.SetChannel(_device.GetChannel() + 1);
        }
    }

    /// <summary>
    /// Você pode estender classes a partir dessa hierarquia de abstração independentemente das classes de dispositivo.
    /// </summary>
    class AdvancedRemote : BasicRemote
    {
        public AdvancedRemote(IDevice device) : base(device) { }

        public void Mute()
        {
            Console.WriteLine("Remote: mute.");
            _device.SetVolume(0);
        }
    }
}

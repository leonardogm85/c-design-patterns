namespace DesignPatterns.CreationalPatterns.Prototype.RealWorld01
{
    // The Prototype design pattern specifies the kind of objects to create using a prototypical
    // instance, and create new objects by copying this prototype.

    class Example
    {
        public static void Run()
        {
            ColorManager manager = new();

            manager["red"] = new Color(255, 0, 0);
            manager["green"] = new Color(0, 255, 0);
            manager["blue"] = new Color(0, 0, 255);

            manager["angry"] = new Color(255, 54, 0);
            manager["peace"] = new Color(128, 211, 128);
            manager["flame"] = new Color(211, 34, 20);

            ColorPrototype angry = manager["angry"].Clone();
            Console.WriteLine($"Cloning - {angry}");

            ColorPrototype peace = manager["peace"].Clone();
            Console.WriteLine($"Cloning - {peace}");

            ColorPrototype flame = manager["flame"].Clone();
            Console.WriteLine($"Cloning - {flame}");
        }
    }

    // Prototype (ColorPrototype)

    abstract class ColorPrototype
    {
        public abstract ColorPrototype Clone();
    }

    // ConcretePrototype (Color)

    class Color : ColorPrototype
    {
        public Color(int red, int green, int blue)
        {
            Red = red;
            Green = green;
            Blue = blue;
        }

        public int Red { get; }
        public int Green { get; }
        public int Blue { get; }

        public override ColorPrototype Clone() => (ColorPrototype)MemberwiseClone();

        public override string ToString() => $"Color RGB: {Red,3},{Green,3},{Blue,3}";
    }

    // Client (ColorManager)

    class ColorManager
    {
        private readonly Dictionary<string, ColorPrototype> _colors = new();

        public ColorPrototype this[string key]
        {
            get => _colors[key];
            set => _colors[key] = value;
        }
    }
}

namespace DesignPatterns.CreationalPatterns.Prototype.RealWorld02
{
    class Example
    {
        public static void Run()
        {
            var app = new Application();

            var s1 = app.Get("Big green circle");
            var s2 = app.Get("Medium blue rectangle");
            var s3 = app.Get("Medium blue rectangle");

            if (s1 == s2)
            {
                Console.WriteLine("Shape 1 (Big green circle) and Shape 2 (Medium blue rectangle) are the same object. (Wrong)");
            }
            else
            {
                Console.Write("Shape 1 (Big green circle) and Shape 2 (Medium blue rectangle) are two different objects, ");

                if (s1.Equals(s2))
                {
                    Console.WriteLine("And they are identical. (Wrong)");
                }
                else
                {
                    Console.WriteLine("But they are not identical. (Correct)");
                }
            }

            if (s2 == s3)
            {
                Console.WriteLine("Shape 2 (Medium blue rectangle) and Shape 3 (Medium blue rectangle) are the same object. (Wrong)");
            }
            else
            {
                Console.Write("Shape 2 (Medium blue rectangle) and Shape 3 (Medium blue rectangle) are two different objects, ");

                if (s2.Equals(s3))
                {
                    Console.WriteLine("And they are identical. (Correct)");
                }
                else
                {
                    Console.WriteLine("But they are not identical. (Wrong)");
                }
            }
        }
    }

    class Application
    {
        private readonly Dictionary<string, Shape> _cache = new();

        public Application() => Reset();

        private void Reset()
        {
            var circle = new Circle(
                5,
                7,
                Color.Green,
                45);

            var rectangle = new Rectangle(
                6,
                9,
                Color.Blue,
                8,
                9);

            Put("Big green circle", circle);
            Put("Medium blue rectangle", rectangle);
        }

        public void Put(string key, Shape shape) => _cache.Add(key, shape);

        public Shape Get(string key) => _cache[key].Clone();
    }

    enum Color
    {
        Red,
        Green,
        Blue
    }

    /// <summary>
    /// Protótipo base.
    /// </summary>
    abstract class Shape
    {
        // Um construtor normal.
        protected Shape(int x, int y, Color color)
        {
            X = x;
            Y = y;
            Color = color;
        }

        // O construtor do protótipo. Um objeto novo é inicializado com valores do objeto existente.
        protected Shape(Shape source) : this(source.X, source.Y, source.Color) { }

        public int X { get; }
        public int Y { get; }
        public Color Color { get; }

        // A operação de clonagem retorna uma das subclasses Shape.
        public abstract Shape Clone();

        public override bool Equals(object? obj)
        {
            return obj is Shape shape
                && X == shape.X
                && Y == shape.Y
                && Color == shape.Color;
        }

        public override int GetHashCode() => HashCode.Combine(X, Y, Color);
    }

    /// <summary>
    /// Protótipo concreto. O método de clonagem cria um novo objeto e passa ele ao construtor. Até o construtor terminar, ele tem
    /// uma referência ao clone fresco. Portanto, ninguém tem acesso ao clone parcialmente construído. Isso faz com que o clone
    /// resultante seja consistente.
    /// </summary>
    class Rectangle : Shape
    {
        public Rectangle(int x, int y, Color color, int width, int height) : base(x, y, color)
        {
            Width = width;
            Height = height;
        }

        // Uma chamada para o construtor pai é necessária para copiar campos privados definidos na classe pai.
        protected Rectangle(Rectangle source) : base(source)
        {
            Width = source.Width;
            Height = source.Height;
        }

        public int Width { get; }
        public int Height { get; }

        public override Shape Clone() => new Rectangle(this);

        public override bool Equals(object? obj)
        {
            return obj is Rectangle rectangle
                && base.Equals(obj)
                && Width == rectangle.Width
                && Height == rectangle.Height;
        }

        public override int GetHashCode() => HashCode.Combine(base.GetHashCode(), Width, Height);
    }

    class Circle : Shape
    {
        public Circle(int x, int y, Color color, int radius) : base(x, y, color) => Radius = radius;

        protected Circle(Circle source) : base(source) => Radius = source.Radius;

        public int Radius { get; }

        public override Shape Clone() => new Circle(this);

        public override bool Equals(object? obj)
        {
            return obj is Circle circle
                && base.Equals(obj)
                && Radius == circle.Radius;
        }

        public override int GetHashCode() => HashCode.Combine(base.GetHashCode(), Radius);
    }
}

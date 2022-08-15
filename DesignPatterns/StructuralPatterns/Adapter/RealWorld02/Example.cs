namespace DesignPatterns.StructuralPatterns.Adapter.RealWorld02
{
    class Example
    {
        public static void Run()
        {
            var roundHole = new RoundHole(5);

            var roundPeg = new RoundPeg(5);

            if (roundHole.Fits(roundPeg))
            {
                Console.WriteLine("Round peg r5 fits round hole r5.");
            }

            var smallSquarePeg = new SquarePeg(2);
            var largeSquarePeg = new SquarePeg(9);

            var smallSquarePegAdapter = new SquarePegAdapter(smallSquarePeg);
            var largeSquarePegAdapter = new SquarePegAdapter(largeSquarePeg);

            if (roundHole.Fits(smallSquarePegAdapter))
            {
                Console.WriteLine("Square peg w2 fits round hole r5.");
            }

            if (!roundHole.Fits(largeSquarePegAdapter))
            {
                Console.WriteLine("Square peg w9 does not fit into round hole r5.");
            }
        }
    }

    /// <summary>
    /// Digamos que você tenha duas classes com interfaces compatíveis: RoundHole (Buraco Redondo) e RoundPeg (Pino
    /// Redondo).
    /// </summary>
    class RoundHole
    {
        private readonly double _radius;

        public RoundHole(double radius) => _radius = radius;

        // Retorna o raio do buraco.
        public double GetRadius() => _radius;

        public bool Fits(RoundPeg peg) => GetRadius() >= peg.GetRadius();
    }

    class RoundPeg
    {
        private readonly double _radius;

        protected RoundPeg() { }

        public RoundPeg(double radius) => _radius = radius;

        // Retorna o raio do pino.
        public virtual double GetRadius() => _radius;
    }

    /// <summary>
    /// Mas tem uma classe incompatível: SquarePeg (Pino Quadrado).
    /// </summary>
    class SquarePeg
    {
        private readonly double _width;

        public SquarePeg(double width) => _width = width;

        // Retorna a largura do pino quadrado.
        public double GetWidth() => _width;

        public double GetSquare() => Math.Pow(_width, 2);
    }

    /// <summary>
    /// Uma classe adaptadora permite que você encaixe pinos quadrados em buracos redondos. Ela estende a classe RoundPeg
    /// para permitir que objetos do adaptador ajam como pinos redondos.
    /// </summary>
    class SquarePegAdapter : RoundPeg
    {
        // Na verdade, o adaptador contém uma instância da classe SquarePeg.
        private readonly SquarePeg _peg;

        public SquarePegAdapter(SquarePeg peg) => _peg = peg;

        // O adaptador finge que é um pino redondo com um raio que encaixaria o pino quadrado que o adaptador está
        // envolvendo.
        public override double GetRadius() => Math.Sqrt(Math.Pow(_peg.GetWidth() / 2, 2) * 2);
    }
}

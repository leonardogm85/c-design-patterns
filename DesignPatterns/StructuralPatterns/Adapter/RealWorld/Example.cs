namespace DesignPatterns.StructuralPatterns.Adapter.RealWorld
{
    // The Adapter design pattern converts the interface of a class into another interface clients
    // expect. This design pattern lets classes work together that couldn‘t otherwise because of
    // incompatible interfaces.

    class Example
    {
        public static void Run()
        {
            Compound compound;

            compound = new Compound();
            compound.Display();

            compound = new RichCompound("Water");
            compound.Display();

            compound = new RichCompound("Benzene");
            compound.Display();

            compound = new RichCompound("Ethanol");
            compound.Display();
        }
    }

    // Target (Compound)

    class Compound
    {
        public float? BoilingPoint { get; protected set; }
        public float? MeltingPoint { get; protected set; }
        public double? MolecularWeight { get; protected set; }
        public string? MolecularFormula { get; protected set; }

        public virtual void Display() => Console.WriteLine($"{"Compound",10}: Unknown {Environment.NewLine}");
    }

    // Adapter (Compound)

    class RichCompound : Compound
    {
        private readonly string _chemical;

        private ChemicalDatabank? _bank;

        public RichCompound(string chemical) => _chemical = chemical;

        public override void Display()
        {
            _bank = new();

            BoilingPoint = _bank.GetCriticalPoint(_chemical, "B");
            MeltingPoint = _bank.GetCriticalPoint(_chemical, "M");
            MolecularWeight = _bank.GetMolecularWeight(_chemical);
            MolecularFormula = _bank.GetMolecularStructure(_chemical);

            Console.WriteLine($"{"Compound",10}: {_chemical}");
            Console.WriteLine($"{"Formula",10}: {MolecularFormula}");
            Console.WriteLine($"{"Weight",10}: {MolecularWeight}");
            Console.WriteLine($"{"Melting Pt",10}: {MeltingPoint}");
            Console.WriteLine($"{"Boiling Pt",10}: {BoilingPoint}");
            Console.WriteLine();
        }
    }

    // Adaptee (ChemicalDatabank)

    class ChemicalDatabank
    {
        public float GetCriticalPoint(string compound, string point)
        {
            if (point == "M")
            {
                switch (compound.ToLower())
                {
                    case "water":
                        return 0.0f;
                    case "benzene":
                        return 5.5f;
                    case "ethanol":
                        return 114.1f;
                    default:
                        return 0f;
                }
            }

            switch (compound.ToLower())
            {
                case "water":
                    return 100.0f;
                case "benzene":
                    return 80.1f;
                case "ethanol":
                    return 78.3f;
                default:
                    return 0f;
            }
        }

        public string GetMolecularStructure(string compound)
        {
            switch (compound.ToLower())
            {
                case "water":
                    return "H20";
                case "benzene":
                    return "C6H6";
                case "ethanol":
                    return "C2H5OH";
                default:
                    return "";
            }
        }

        public double GetMolecularWeight(string compound)
        {
            switch (compound.ToLower())
            {
                case "water":
                    return 18.015;
                case "benzene":
                    return 78.1134;
                case "ethanol":
                    return 46.0688;
                default:
                    return 0d;
            }
        }
    }

    // Client (AdapterApp)

    // -
}

namespace DesignPatterns.CreationalPatterns.Builder.RealWorld
{
    // The Builder design pattern separates the construction of a complex object from its
    // representation so that the same construction process can create different representations.

    class Example
    {
        public static void Run()
        {
            VehicleBuilder builder;

            builder = new MotorCycleBuilder();
            Shop.Construct(builder);
            builder.Vehicle.Show();

            builder = new CarBuilder();
            Shop.Construct(builder);
            builder.Vehicle.Show();

            builder = new ScooterBuilder();
            Shop.Construct(builder);
            builder.Vehicle.Show();
        }
    }

    // Builder (VehicleBuilder)

    abstract class VehicleBuilder
    {
        public Vehicle Vehicle { get; private set; }

        protected VehicleBuilder(string vehicleType) => Vehicle = new(vehicleType);

        public abstract void BuildFrame();
        public abstract void BuildEngine();
        public abstract void BuildWheels();
        public abstract void BuildDoors();
    }

    // ConcreteBuilder (MotorCycleBuilder, CarBuilder, ScooterBuilder)

    class MotorCycleBuilder : VehicleBuilder
    {
        public MotorCycleBuilder() : base("MotorCycle")
        {
        }

        public override void BuildFrame() => Vehicle["frame"] = "MotorCycle Frame";
        public override void BuildEngine() => Vehicle["engine"] = "500 cc";
        public override void BuildWheels() => Vehicle["wheels"] = "2";
        public override void BuildDoors() => Vehicle["doors"] = "0";
    }

    class CarBuilder : VehicleBuilder
    {
        public CarBuilder() : base("Car")
        {
        }

        public override void BuildFrame() => Vehicle["frame"] = "Car Frame";
        public override void BuildEngine() => Vehicle["engine"] = "2500 cc";
        public override void BuildWheels() => Vehicle["wheels"] = "4";
        public override void BuildDoors() => Vehicle["doors"] = "4";
    }

    class ScooterBuilder : VehicleBuilder
    {
        public ScooterBuilder() : base("Scooter")
        {
        }

        public override void BuildFrame() => Vehicle["frame"] = "Scooter Frame";
        public override void BuildEngine() => Vehicle["engine"] = "50 cc";
        public override void BuildWheels() => Vehicle["wheels"] = "2";
        public override void BuildDoors() => Vehicle["doors"] = "0";
    }

    // Director (Shop)

    static class Shop
    {
        public static void Construct(VehicleBuilder builder)
        {
            builder.BuildFrame();
            builder.BuildEngine();
            builder.BuildWheels();
            builder.BuildDoors();
        }
    }

    // Product (Vehicle)

    class Vehicle
    {
        private readonly string _vehicleType;

        private readonly Dictionary<string, string> _parts = new();

        public Vehicle(string vehicleType) => _vehicleType = vehicleType;

        public string this[string key]
        {
            get => _parts[key];
            set => _parts[key] = value;
        }

        public void Show()
        {
            Console.WriteLine($"{"Vehicle Type",12}: {_vehicleType}");

            Console.WriteLine($"{"Frame",12}: {_parts["frame"]}");
            Console.WriteLine($"{"Engine",12}: {_parts["engine"]}");
            Console.WriteLine($"{"#Wheels",12}: {_parts["wheels"]}");
            Console.WriteLine($"{"#Doors",12}: {_parts["doors"]}");

            Console.WriteLine();
        }
    }
}

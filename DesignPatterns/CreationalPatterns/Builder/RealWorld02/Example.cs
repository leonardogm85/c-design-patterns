namespace DesignPatterns.CreationalPatterns.Builder.RealWorld02
{
    class Example
    {
        public static void Run()
        {
            var app = new Application();

            Console.WriteLine("Make car:");
            app.Make(new CarBuilder());

            Console.WriteLine();

            Console.WriteLine("Make manual:");
            app.Make(new ManualBuilder());
        }
    }

    /// <summary>
    /// O código cliente cria um objeto builder, passa ele para o diretor e então inicia o processo de construção. O resultado
    /// final é recuperado do objeto builder.
    /// </summary>
    class Application
    {
        public void Make(IBuilder builder)
        {
            var director = new Director();

            director.ConstructCityCar(builder);
            var cityCar = builder.GetResult();
            Console.WriteLine(cityCar);

            director.ConstructSportCar(builder);
            var sportCar = builder.GetResult();
            Console.WriteLine(sportCar);

            director.ConstructSuvCar(builder);
            var suvCar = builder.GetResult();
            Console.WriteLine(suvCar);
        }
    }

    /// <summary>
    /// O diretor é apenas responsável por executar as etapas de construção em uma sequência em particular. Isso ajuda quando
    /// produzindo produtos de acordo com uma ordem específica ou configuração. A rigor, a classe diretor é opcional, já que o
    /// cliente pode controlar os builders diretamente.
    /// O diretor trabalha com qualquer instância builder que o código cliente passar a ele. Dessa forma, o código
    /// cliente pode alterar o tipo final do produto recém montado.
    /// O diretor pode construir diversas variações do produto usando as mesmas etapas de construção.
    /// </summary>
    class Director
    {
        public void ConstructCityCar(IBuilder builder)
        {
            builder.Reset();
            builder.SetSeats(4);
            builder.SetEngine(new CityEngine());
            builder.SetTripComputer(false);
            builder.SetGps(false);
        }

        public void ConstructSportCar(IBuilder builder)
        {
            builder.Reset();
            builder.SetSeats(2);
            builder.SetEngine(new SportEngine());
            builder.SetTripComputer(true);
            builder.SetGps(false);
        }

        public void ConstructSuvCar(IBuilder builder)
        {
            builder.Reset();
            builder.SetSeats(4);
            builder.SetEngine(new SuvEngine());
            builder.SetTripComputer(false);
            builder.SetGps(true);
        }
    }

    /// <summary>
    /// A interface builder especifica métodos para criar as diferentes partes de objetos produto.
    /// </summary>
    interface IBuilder
    {
        void Reset();
        void SetSeats(int seats);
        void SetEngine(Engine engine);
        void SetTripComputer(bool tripComputer);
        void SetGps(bool gps);
        IDomainModel GetResult();
    }

    /// <summary>
    /// As classes builder concretas seguem a interface do builder e fornecem implementações específicas das etapas
    /// de construção. Seu programa pode ter algumas variações de builders, cada uma implementada de forma diferente.
    /// </summary>
    class CarBuilder : IBuilder
    {
        private int _seats;
        private Engine _engine;
        private bool _tripComputer;
        private bool _gps;

        // Uma instância fresca do builder deve conter um objeto produto em branco na qual ela usa para montagem futura.
        public CarBuilder() => Reset();

        // O método reset limpa o objeto sendo construído.
        public void Reset()
        {
            _seats = 2;
            _engine = new CityEngine();
            _tripComputer = false;
            _gps = false;
        }

        // Todas as etapas de produção trabalham com a mesma instância de produto.

        // Define o número de assentos no carro.
        public void SetSeats(int seats) => _seats = seats;

        // Instala um tipo de motor.
        public void SetEngine(Engine engine) => _engine = engine;

        // Instala um computador de bordo.
        public void SetTripComputer(bool tripComputer) => _tripComputer = tripComputer;

        // Instala um sistema de posicionamento global.
        public void SetGps(bool gps) => _gps = gps;

        // Builders concretos devem fornecer seus próprios métodos para recuperar os resultados. Isso é porque
        // vários tipos de builders podem criar produtos inteiramente diferentes que nem sempre seguem a mesma
        // interface. Portanto, tais métodos não podem ser declarados na interface do builder (ao menos não em
        // uma linguagem de programação de tipo estático).
        // Geralmente, após retornar o resultado final para o cliente, espera-se que uma instância de builder comece
        // a produzir outro produto. É por isso que é uma prática comum chamar o método reset no final do corpo do método
        // `getProduct`. Contudo este comportamento não é obrigatório, e você pode fazer seu builder esperar por
        // uma chamada explícita do reset a partir do código cliente antes de se livrar de seu resultado anterior.
        public IDomainModel GetResult()
        {
            var result = new Car(
                _seats,
                _engine,
                _tripComputer,
                _gps);
            Reset();
            return result;
        }
    }

    /// <summary>
    /// Ao contrário dos outros padrões criacionais, o Builder permite que você construa produtos que não seguem uma
    /// interface comum.
    /// </summary>
    class ManualBuilder : IBuilder
    {
        private int _seats;
        private Engine _engine;
        private bool _tripComputer;
        private bool _gps;

        public ManualBuilder() => Reset();

        public void Reset()
        {
            _seats = 2;
            _engine = new CityEngine();
            _tripComputer = false;
            _gps = false;
        }

        // Documenta as funcionalidades do assento do carro.
        public void SetSeats(int seats) => _seats = seats;

        // Adiciona instruções do motor.
        public void SetEngine(Engine engine) => _engine = engine;

        // Adiciona instruções do computador de bordo.
        public void SetTripComputer(bool tripComputer) => _tripComputer = tripComputer;

        // Adiciona instruções do GPS.
        public void SetGps(bool gps) => _gps = gps;

        // Retorna o manual e reseta o builder.
        public IDomainModel GetResult()
        {
            var result = new Manual(
                _seats,
                _engine,
                _tripComputer,
                _gps);
            Reset();
            return result;
        }
    }

    /// <summary>
    /// Usar o padrão Builder só faz sentido quando seus produtos são bem complexos e requerem configuração extensiva. Os dois
    /// produtos a seguir são relacionados, embora eles não tenham uma interface em comum.
    /// Um carro pode ter um GPS, computador de bordo, e alguns assentos. Diferentes modelos de carros (esportivo, SUV,
    /// conversível) podem ter diferentes funcionalidades instaladas ou equipadas.
    /// </summary>
    class Car : IDomainModel
    {
        public Car(int seats, Engine engine, bool tripComputer, bool gps)
        {
            Seats = seats;
            Engine = engine;
            TripComputer = tripComputer;
            Gps = gps;
        }

        public int Seats { get; }
        public Engine Engine { get; }
        public bool TripComputer { get; }
        public bool Gps { get; }

        public override string ToString() => $"Car -> Seats: {Seats}, Engine: {Engine}; TripComputer: {TripComputer}; GPS: {Gps}.";
    }

    /// <summary>
    /// Cada carro deve ter um manual do usuário que corresponda a configuração do carro e descreva todas suas
    /// funcionalidades.
    /// </summary>
    class Manual : IDomainModel
    {
        public Manual(int seats, Engine engine, bool tripComputer, bool gps)
        {
            Seats = seats;
            Engine = engine;
            TripComputer = tripComputer;
            Gps = gps;
        }

        public int Seats { get; }
        public Engine Engine { get; }
        public bool TripComputer { get; }
        public bool Gps { get; }

        public override string ToString() => $"Manual -> Seats: {Seats}, Engine: {Engine}; TripComputer: {TripComputer}; GPS: {Gps}.";
    }

    abstract class Engine
    {
        public override string ToString() => GetType().Name;
    }

    class CityEngine : Engine { }

    class SportEngine : Engine { }

    class SuvEngine : Engine { }

    interface IDomainModel { }
}

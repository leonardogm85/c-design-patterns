namespace DesignPatterns.StructuralPatterns.Bridge.RealWorld01
{
    // The Bridge design pattern decouples an abstraction from its implementation so that the
    // two can vary independently.

    class Example
    {
        public static void Run()
        {
            DataObject data = new CustomersDataObject("Chicago");

            CustomersBase customers = new Customers(data);

            customers.Show();
            customers.Next();
            customers.Show();
            customers.Next();
            customers.Show();
            customers.Add("Henry Velasquez");

            Console.WriteLine();

            customers.ShowAll();
        }
    }

    // Abstraction (BusinessObject)

    class CustomersBase
    {
        public DataObject DataObject { get; private set; }

        public CustomersBase(DataObject dataObject) => DataObject = dataObject;

        public void SetDataObject(DataObject dataObject) => DataObject = dataObject;

        public virtual void Next() => DataObject.NextRecord();

        public virtual void Prior() => DataObject.PriorRecord();

        public virtual void Add(string customer) => DataObject.AddRecord(customer);

        public virtual void Delete(string customer) => DataObject.DeleteRecord(customer);

        public virtual void Show() => DataObject.ShowRecord();

        public virtual void ShowAll() => DataObject.ShowAllRecords();
    }

    // RefinedAbstraction (Customers)

    class Customers : CustomersBase
    {
        public Customers(DataObject dataObject) : base(dataObject) { }
    }

    // Implementor (DataObject)

    abstract class DataObject
    {
        public abstract void NextRecord();
        public abstract void PriorRecord();
        public abstract void AddRecord(string customer);
        public abstract void DeleteRecord(string customer);
        public abstract string GetCurrentRecord();
        public abstract void ShowRecord();
        public abstract void ShowAllRecords();
    }

    // ConcreteImplementor (CustomersDataObject)

    class CustomersDataObject : DataObject
    {
        private readonly List<string> _customers = new();

        private readonly string _city;

        private int _current = 0;

        public CustomersDataObject(string city)
        {
            _city = city;

            _customers.Add("Jim Jones");
            _customers.Add("Samual Jackson");
            _customers.Add("Allen Good");
            _customers.Add("Ann Stills");
            _customers.Add("Lisa Giolani");
        }

        public override void NextRecord()
        {
            if (_current <= _customers.Count - 1)
            {
                _current++;
            }
        }

        public override void PriorRecord()
        {
            if (_current > 0)
            {
                _current--;
            }
        }

        public override void AddRecord(string customer) => _customers.Add(customer);

        public override void DeleteRecord(string customer) => _customers.Add(customer);

        public override string GetCurrentRecord() => _customers[_current];

        public override void ShowRecord() => Console.WriteLine(GetCurrentRecord());

        public override void ShowAllRecords()
        {
            Console.WriteLine($"Customer City: {_city}");

            foreach (string customer in _customers)
            {
                Console.WriteLine($"  {customer}");
            }
        }
    }
}

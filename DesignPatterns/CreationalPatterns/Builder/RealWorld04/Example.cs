namespace DesignPatterns.CreationalPatterns.Builder.RealWorld04
{
    class Example
    {
        public static void Run()
        {
            Console.WriteLine("Client: Start fetching data from Realm");
            var realm = new UserApplication(new RealmQueryBuilder<User>());
            realm.Query();

            Console.WriteLine();

            Console.WriteLine("Client: Start fetching data from CoreData");
            var coreData = new UserApplication(new CoreDataQueryBuilder<User>());
            coreData.Query();
        }
    }

    class UserApplication
    {
        public IQueryBuilder<User> Builder { get; }

        public UserApplication(IQueryBuilder<User> builder) => Builder = builder;

        public void Query()
        {
            var results = Builder
                .Filter(user => user.Age < 20)
                .Limit(1)
                .Fetch();

            Console.WriteLine($"Client: I have fetched: {results.Count} records.");
        }
    }

    interface IQueryBuilder<TModel> where TModel : IDomainModel
    {
        IQueryBuilder<TModel> Limit(int limit);
        IQueryBuilder<TModel> Filter(Func<TModel, bool> predicate);
        IList<TModel> Fetch();
    }

    class RealmQueryBuilder<TModel> : IQueryBuilder<TModel> where TModel : IDomainModel
    {
        private List<Query> _operations = new();

        public IQueryBuilder<TModel> Limit(int limit)
        {
            _operations.Add(Query.Limit);
            return this;
        }

        public IQueryBuilder<TModel> Filter(Func<TModel, bool> predicate)
        {
            _operations.Add(Query.Filter);
            return this;
        }

        public IList<TModel> Fetch() => new RealmProvider().Fetch<TModel>(_operations);
    }

    class CoreDataQueryBuilder<TModel> : IQueryBuilder<TModel> where TModel : IDomainModel
    {
        private readonly List<Query> _operations = new();

        public IQueryBuilder<TModel> Limit(int limit)
        {
            _operations.Add(Query.Limit);
            return this;
        }

        public IQueryBuilder<TModel> Filter(Func<TModel, bool> predicate)
        {
            _operations.Add(Query.Filter);
            return this;
        }

        public IList<TModel> Fetch() => new CoreDataProvider().Fetch<TModel>(_operations);
    }

    class RealmProvider
    {
        public IList<TModel> Fetch<TModel>(IList<Query> operations) where TModel : IDomainModel
        {
            Console.WriteLine($"RealmQueryBuilder: Initializing RealmProvider with {operations.Count} operations.");

            Console.WriteLine("RealmProvider: Retrieving data from Realm.");

            foreach (var operation in operations)
            {
                Console.WriteLine($"RealmProvider: Executing the {operation} operation.");
            }

            return new List<TModel>();
        }
    }

    class CoreDataProvider
    {
        public IList<TModel> Fetch<TModel>(IList<Query> operations) where TModel : IDomainModel
        {
            Console.WriteLine($"CoreDataQueryBuilder: Initializing CoreDataProvider with {operations.Count} operations.");

            Console.WriteLine("CoreDataProvider: Retrieving data from CoreData.");

            foreach (var operation in operations)
            {
                Console.WriteLine($"CoreDataProvider: Executing the {operation} operation.");
            }

            return new List<TModel>();
        }
    }

    interface IDomainModel { }

    class User : IDomainModel
    {
        public User(int id, string name, string email, int age)
        {
            Id = id;
            Name = name;
            Email = email;
            Age = age;
        }

        public int Id { get; }
        public string Name { get; }
        public string Email { get; }
        public int Age { get; }
    }

    enum Query
    {
        Filter,
        Limit
    }
}

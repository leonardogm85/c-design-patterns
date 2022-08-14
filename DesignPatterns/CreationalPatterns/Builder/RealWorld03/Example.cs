namespace DesignPatterns.CreationalPatterns.Builder.RealWorld03
{
    class Example
    {
        public static void Run()
        {
            Console.WriteLine("Testing MySQL query builder:");
            var mySql = new Application(new MySqlQueryBuilder());
            Console.WriteLine(mySql.GetQuery());

            Console.WriteLine();

            Console.WriteLine("Testing PostgresSQL query builder:");
            var postgres = new Application(new PostgresQueryBuilder());
            Console.WriteLine(postgres.GetQuery());
        }
    }

    class Application
    {
        public ISqlQueryBuilder Builder { get; }

        public Application(ISqlQueryBuilder builder) => Builder = builder;

        public string GetQuery()
        {
            return Builder
                .Select("users", new List<string> { "name", "email", "password" })
                .Where("age", "18", ">")
                .Where("age", "30", "<")
                .Limit(10, 20)
                .GetSql();
        }
    }

    interface ISqlQueryBuilder
    {
        ISqlQueryBuilder Select(string table, IList<string> fields);
        ISqlQueryBuilder Where(string field, string value, string @operator = "=");
        ISqlQueryBuilder Limit(int start, int offset);
        string GetSql();
    }

    class MySqlQueryBuilder : ISqlQueryBuilder
    {
        private SqlQuery query;

        public MySqlQueryBuilder() => Reset();

        private void Reset() => query = new SqlQuery();

        public ISqlQueryBuilder Select(string table, IList<string> fields)
        {
            Reset();
            query.Type = SqlType.Select;
            query.Base = $"SELECT {string.Join(",", fields)} FROM {table}";
            return this;
        }

        public ISqlQueryBuilder Where(string field, string value, string @operator = "=")
        {
            switch (query.Type)
            {
                case SqlType.Select:
                case SqlType.Update:
                case SqlType.Delete:
                    (query.Where ?? new List<string>()).Add($"{field} {@operator} '{value}'");
                    break;
                default:
                    throw new Exception("WHERE can only be added to SELECT, UPDATE, DELETE.");
            }

            return this;
        }

        public ISqlQueryBuilder Limit(int start, int offset)
        {
            switch (query.Type)
            {
                case SqlType.Select:
                    query.Limit = $" LIMIT {start},{offset}";
                    break;
                default:
                    throw new Exception("LIMIT can only be added to SELECT.");
            }

            return this;
        }

        public string GetSql()
        {
            var sql = query.Base!;

            if (query.Where?.Any() ?? false)
            {
                sql += $" WHERE {string.Join(" AND ", query.Where)}";
            }

            if (!string.IsNullOrEmpty(query.Limit))
            {
                sql += query.Limit;
            }

            sql += ";";

            return sql;
        }
    }

    class PostgresQueryBuilder : ISqlQueryBuilder
    {
        private SqlQuery _query;

        public PostgresQueryBuilder() => Reset();

        private void Reset() => _query = new SqlQuery();

        public ISqlQueryBuilder Select(string table, IList<string> fields)
        {
            Reset();
            _query.Type = SqlType.Select;
            _query.Base = $"SELECT {string.Join(",", fields)} FROM {table}";
            return this;
        }

        public ISqlQueryBuilder Where(string field, string value, string @operator = "=")
        {
            switch (_query.Type)
            {
                case SqlType.Select:
                case SqlType.Update:
                case SqlType.Delete:
                    (_query.Where ?? new List<string>()).Add($"{field} {@operator} '{value}'");
                    break;
                default:
                    throw new Exception("WHERE can only be added to SELECT, UPDATE, DELETE.");
            }

            return this;
        }

        public ISqlQueryBuilder Limit(int start, int offset)
        {
            switch (_query.Type)
            {
                case SqlType.Select:
                    _query.Limit = $" LIMIT {start} OFFSET {offset}";
                    break;
                default:
                    throw new Exception("LIMIT can only be added to SELECT.");
            }

            return this;
        }

        public string GetSql()
        {
            var sql = _query.Base!;

            if (_query.Where?.Any() ?? false)
            {
                sql += $" WHERE {string.Join(" AND ", _query.Where)}";
            }

            if (!string.IsNullOrEmpty(_query.Limit))
            {
                sql += _query.Limit;
            }

            sql += ";";

            return sql;
        }
    }

    class SqlQuery
    {
        public SqlType? Type { get; set; }
        public string? Base { get; set; }
        public IList<string>? Where { get; set; }
        public string? Limit { get; set; }
    }

    enum SqlType
    {
        Select,
        Update,
        Delete
    }
}
